"use strict";

var path = require('path');

var RedFolder = RedFolder || {};

RedFolder.Utils = RedFolder.Utils || {};

RedFolder.Utils.WireDepOptions = function(bowserJson, directory, ignorePath, locationTag) {
    var options = {
        bowerJson: bowserJson,
        directory: directory,
        ignorePath: ignorePath,
        fileTypes: {
            cshtml: {
                block: new RegExp("(([ \\t]*)<!--\\s*" + locationTag + ":*(\\S*)\\s*-->)(\\n|\\r|.)*?(<!--\\s*end" + locationTag + "\\s*-->)", "gi"),
                detect: {
                    js: /<script.*src=['"]([^'"]+)/gi,
                    css: /<link.*href=['"]([^'"]+)/gi
                },
                replace: {
                    js: '<script type="text/javascript" src="~{{filePath}}"></script>',
                    css: '<link rel="stylesheet" href="{{filePath}}" />'
                }
            }
        }
    };

    return options;
}

RedFolder.Utils.GulpAppConfig = function (htmlDestination, htmlInjectionTag, jsFolder, lessFolder) {
    return {
        htmlDestination: htmlDestination,
        //htmlInjectionStartTag: "<!-- injection"+ htmlInjectionTag + " -->",
        //htmlInjectionStartTag: "<!-- injection" + htmlInjectionTag + " -->",

        hasJs: jsFolder ? true : false,
        js: {
            folder: jsFolder,
            custom: jsFolder + "*.js",
            thirdParty: jsFolder + "3rdParty",

            development: {
                htmlInjection: {
                    startTag: "<!-- injection" + htmlInjectionTag + "Development:js -->",
                    endTag: "<!-- endinjection" + htmlInjectionTag + "Development:js -->"
                }
            },

            production: {
                folder: jsFolder + "production/",
                htmlInjection: {
                    startTag: "<!-- injection" + htmlInjectionTag + "Production:js -->",
                    endTag: "<!-- endinjection" + htmlInjectionTag + "Production:js -->"
                }
            }
        },

        hasLess: lessFolder ? true : false,
        less: {
            folder: lessFolder,

            development: {
                htmlInjection: {
                    tagName: htmlInjectionTag + "Development"
                }
            },

            production: {
                folder: lessFolder + "production/",
                bundleName: htmlInjectionTag + ".css",
                htmlInjection: {
                    tagName: htmlInjectionTag + "Production"
                }
            }
        },

        //,
        /*
        css: {
            folder: cssFolder,
            custom: cssFolder + "*.css",
            thirdParty: cssFolder + "3rdParty",

            development: {
                htmlInjection: {
                    tagName: htmlInjectionTag + "Development"
                }
            },

            production: {
                folder: cssFolder + "production/",
                htmlInjection: {
                    startTag: "<!-- injection" + htmlInjectionTag + "Production:css -->",
                    endTag: "<!-- endinjection" + htmlInjectionTag + "Production:css -->"
                }
            }
        }
        */
    }
};

module.exports = function () {
    var config = {
        source: { 
            root: "./wwwroot-src/"
        },
        destination: {
            root: "./wwwroot/"
        },
        tools: {
            jscsConfig: "./.jsjc.json"
        }
    };

    // htmlDestination, htmlInjectionTag, jsFolder, lessFolder, cssFolder
    config.apps = [
        // Shared
        RedFolder.Utils.GulpAppConfig (
            './views/shared/_layout.cshtml',          // htmlDestination
            'shared',                                    // htmlInjectionTag
            null,                       // jsFolder
            './wwwroot/css/shared/',    // lessFolder
            null                        // ccsFolder
        ),
        
        // testApp
        RedFolder.Utils.GulpAppConfig(
            './views/shared/_layout.cshtml',          // htmlDestination
            'testApp',                                    // htmlInjectionTag
            null,                       // jsFolder
            './wwwroot/css/testApp/',   // lessFolder
            null                        // ccsFolder
        )
    ];

    config.lessToCompile = function() {
        return config.apps.filter(function (app) {
                                return app.hasLess;
                            }).map(function (app) {
                                return {
                                    src: app.less.folder + '*.less',
                                    dest: app.less.folder
                                };
                            });
    };

    config.lessToWatch = function () {
        return config.lessToCompile().map(function (less) {
            return less.src;
        });
    };

    config.cssToValidate = function () {
        return config.apps.filter(function (app) {
                                return app.hasLess;
                            }).map(function (app) {
                                return {
                                    src: app.less.folder + '*.css',
                                    dest: app.less.folder
                                };
                            });
    };

    config.cssToInject = function () {
        var results = [];
        config.apps.forEach(function (app) {
            if (results.filter(function (result) { return result.src === app.htmlDestination; }).length == 0) {
                results.push({
                    src: app.htmlDestination,
                    dest: path.dirname(app.htmlDestination),
                    tags: []
                });
            }

            var tags = results.filter(function (result) { return result.src === app.htmlDestination; })[0].tags;

            if (tags.filter(function (tag) { return tag.tagName == app.less.development.htmlInjection.tagName; })) {
                tags.push({
                    ignorePath: '/wwwroot',
                    tagName: app.less.development.htmlInjection.tagName,
                    css: []
                });
            }

            var css = tags.filter(function (tag) { return tag.tagName == app.less.development.htmlInjection.tagName; })[0].css;
            css.push(app.less.folder + '*.css');
        });

        return results;
    };

    config.cssToBundle = function () {
        return config.apps.filter(function (app) {
            return app.hasLess;
        }).map(function (app) {
            return {
                src: app.less.folder + '*.css',
                dest: app.less.production.folder,
                bundleName: app.less.production.bundleName
            };
        });
    };

    config.cssToDeploy = function () {
        var results = [];
        config.apps.forEach(function (app) {
            if (results.filter(function (result) { return result.src === app.htmlDestination; }).length == 0) {
                results.push({
                    src: app.htmlDestination,
                    dest: path.dirname(app.htmlDestination),
                    tags: []
                });
            }

            var tags = results.filter(function (result) { return result.src === app.htmlDestination; })[0].tags;

            if (tags.filter(function (tag) { return tag.tagName == app.less.production.htmlInjection.tagName; })) {
                tags.push({
                    ignorePath: '/wwwroot',
                    tagName: app.less.production.htmlInjection.tagName,
                    css: []
                });
            }

            var css = tags.filter(function (tag) { return tag.tagName == app.less.production.htmlInjection.tagName; })[0].css;
            css.push(app.less.production.folder + '*.css');
        });

        return results;
    };

    // Shared JS/ CSS
    // js.folder = "./wwwroot/scripts/shared/"
    // js.source = js.source + "**/*.js"
    // css.folder = "./wwwroot/css/shared/"
    // less.source = css.folder + "**/*.less"
    // css.source = css.folder + "**/*.css"


    config.source.less = config.source.root + "css/**/*.less";
    config.source.css = config.source.root + "css/**/*.css";
    config.source.js = config.source.root + "scripts/**/*.js";
    config.source.js3rdParty = config.source.root + "scripts/3rdParty/**/*.js";
    config.source.jsToValidate = [config.source.js, "!" + config.source.js3rdParty];

    config.destination.css = config.destination.root + "/css";
    config.destination.js = config.destination.root + "/scripts";

    config.layoutBaseFolder = "./views/shared/"
    config.layoutFolder = config.layoutBaseFolder + "output/";
    config.layoutFiles = config.layoutBaseFolder + "_Layout.cshtml";

    config.bower = {
        json: require('./bower.json'),
        directory: './wwwroot/lib',
        ignorePath: '../../wwwroot'
    };

    config.wiredep = {};

    config.wiredep.optionsList = [
        RedFolder.Utils.WireDepOptions(config.bower.json, config.bower.directory, config.bower.ignorePath, "bowerDevelopment"),
        RedFolder.Utils.WireDepOptions(config.bower.json, config.bower.directory, config.bower.ignorePath, "bowerProduction")
    ];

    return config;
};