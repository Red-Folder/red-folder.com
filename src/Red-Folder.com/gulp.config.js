﻿'use strict';

/* jshint node: true */
var path = require('path');

var RedFolder = RedFolder || {};

RedFolder.Utils = RedFolder.Utils || {};

RedFolder.Utils.WireDepOptions = function(bowserJson, directory, ignorePath, locationTag, dependencies, devDependencies) {
    var options = {
        bowerJson: bowserJson,
        directory: directory,
        ignorePath: ignorePath,
    };

    return options;
};

RedFolder.Utils.GulpAppConfig = function(htmlDestination, htmlInjectionTag, jsFolder, hasThirdPartyJs, lessFolder) {
    return {
        htmlDestination: htmlDestination,

        hasJs: jsFolder ? true : false,
        js: {
            folder: jsFolder,
            hasThirdParty: hasThirdPartyJs,
            development: {
                htmlInjection: {
                    tagName: htmlInjectionTag + 'Development',
                },
            },
            production: {
                folder: jsFolder + 'production/',
                bundleName: htmlInjectionTag + '.js',
                htmlInjection: {
                    tagName: htmlInjectionTag + 'Production',
                },
            },
        },

        hasLess: lessFolder ? true : false,
        less: {
            folder: lessFolder,

            development: {
                htmlInjection: {
                    tagName: htmlInjectionTag + 'Development',
                },
            },

            production: {
                folder: lessFolder + 'production/',
                bundleName: htmlInjectionTag + '.css',
                htmlInjection: {
                    tagName: htmlInjectionTag + 'Production',
                },
            },
        },
    };
};

module.exports = function() {
    var config = {
        source: {
            root: './wwwroot-src/',
        },
        destination: {
            root: './wwwroot/',
        },
        tools: {
            jscsConfig: './.jsjc.json',
        },
    };

    config.apps = [
        // Shared
        RedFolder.Utils.GulpAppConfig (
            './views/shared/_layout.cshtml',          // HtmlDestination
            'shared',                                    // HtmlInjectionTag
            './wwwroot/scripts/shared/',                       // JsFolder
            true,                       // HasThirdPartyJs
            './wwwroot/css/shared/',    // LessFolder
            null                        // CcsFolder
        ),

        // TestApp
        RedFolder.Utils.GulpAppConfig(
            './views/shared/_layout.cshtml',          // HtmlDestination
            'testApp',                                    // HtmlInjectionTag
            './wwwroot/scripts/testApp/',     // JsFolder
            false,                       // HasThirdPartyJs
            './wwwroot/css/testApp/',   // LessFolder
            null                        // CcsFolder
        ),
    ];

    config.lessToCompile = function() {
        return config.apps.filter(function(app) {
                                return app.hasLess;
                            }).map(function(app) {
                                return {
                                    src: app.less.folder + '*.less',
                                    dest: app.less.folder,
                                };
                            });
    };

    config.lessToWatch = function() {
        return config.lessToCompile().map(function(less) {
            return less.src;
        });
    };

    config.lessToValidate = function() {
        return config.apps.filter(function(app) {
                                return app.hasLess;
                            }).map(function(app) {
                                return {
                                    src: app.less.folder + '*.less',
                                    dest: app.less.folder,
                                };
                            });
    };

    config.cssToAutoPrefix = function() {
        return config.apps.filter(function(app) {
            return app.hasLess;
        }).map(function(app) {
            return {
                src: app.less.folder + '*.css',
                dest: app.less.folder,
            };
        });
    };

    config.cssToInject = function() {
        var results = [];
        config.apps.forEach(function(app) {
            if (results.filter(function(result) { return result.src === app.htmlDestination; }).length === 0) {
                results.push({
                    src: app.htmlDestination,
                    dest: path.dirname(app.htmlDestination),
                    tags: [],
                });
            }

            var tags = results.filter(function(result) { return result.src === app.htmlDestination; })[0].tags;

            if (tags.filter(function(tag) { return tag.tagName == app.less.development.htmlInjection.tagName; })) {
                tags.push({
                    ignorePath: '/wwwroot',
                    tagName: app.less.development.htmlInjection.tagName,
                    css: [],
                });
            }

            var css = tags.filter(function(tag) { return tag.tagName == app.less.development.htmlInjection.tagName; })[0].css;
            css.push(app.less.folder + '*.css');
        });

        return results;
    };

    config.cssToBundle = function() {
        return config.apps.filter(function(app) {
            return app.hasLess;
        }).map(function(app) {
            return {
                src: app.less.folder + '*.css',
                dest: app.less.production.folder,
                bundleName: app.less.production.bundleName,
            };
        });
    };

    config.cssToDeploy = function() {
        var results = [];
        config.apps.forEach(function(app) {
            if (results.filter(function(result) { return result.src === app.htmlDestination; }).length === 0) {
                results.push({
                    src: app.htmlDestination,
                    dest: path.dirname(app.htmlDestination),
                    tags: [],
                });
            }

            var tags = results.filter(function(result) { return result.src === app.htmlDestination; })[0].tags;

            if (tags.filter(function(tag) { return tag.tagName == app.less.production.htmlInjection.tagName; })) {
                tags.push({
                    ignorePath: '/wwwroot',
                    tagName: app.less.production.htmlInjection.tagName,
                    css: [],
                });
            }

            var css = tags.filter(function(tag) { return tag.tagName == app.less.production.htmlInjection.tagName; })[0].css;
            css.push(app.less.production.folder + '*.css');
        });

        return results;
    };

    config.jsToValidate = function() {
        return config.apps.filter(function(app) {
            return app.hasJs;
        }).map(function(app) {
            return {
                src: app.js.folder + '*.js',
                dest: app.js.folder,
            };
        });
    };

    config.jsToInject = function() {
        var results = [];
        config.apps.forEach(function(app) {
            if (results.filter(function(result) { return result.src === app.htmlDestination; }).length === 0) {
                results.push({
                    src: app.htmlDestination,
                    dest: path.dirname(app.htmlDestination),
                    tags: [],
                });
            }

            var tags = results.filter(function(result) { return result.src === app.htmlDestination; })[0].tags;

            if (tags.filter(function(tag) { return tag.tagName == app.js.development.htmlInjection.tagName; })) {
                tags.push({
                    ignorePath: '/wwwroot',
                    tagName: app.js.development.htmlInjection.tagName,
                    js: [],
                });
            }

            var js = tags.filter(function(tag) { return tag.tagName == app.js.development.htmlInjection.tagName; })[0].js;
            if (app.js.hasThirdParty) {
                js.push(app.js.folder + '3rdParty/*.js');
            }
            js.push(app.js.folder + '*.js');
        });

        return results;
    };

    config.jsToBundle = function() {
        return config.apps.filter(function(app) {
            return app.hasJs;
        }).map(function(app) {
            return {
                src: [
                    app.js.folder + '3rdParty/*.js',
                    app.js.folder + '*.js',
                ],
                dest: app.js.production.folder,
                bundleName: app.js.production.bundleName,
            };
        });
    };

    config.jsToDeploy = function() {
        var results = [];
        config.apps.forEach(function(app) {
            if (results.filter(function(result) { return result.src === app.htmlDestination; }).length === 0) {
                results.push({
                    src: app.htmlDestination,
                    dest: path.dirname(app.htmlDestination),
                    tags: [],
                });
            }

            var tags = results.filter(function(result) { return result.src === app.htmlDestination; })[0].tags;

            if (tags.filter(function(tag) { return tag.tagName == app.js.production.htmlInjection.tagName; })) {
                tags.push({
                    ignorePath: '/wwwroot',
                    tagName: app.js.production.htmlInjection.tagName,
                    js: [],
                });
            }

            var js = tags.filter(function(tag) { return tag.tagName == app.js.production.htmlInjection.tagName; })[0].js;
            js.push(app.js.production.folder + '*.js');
        });

        return results;
    };

    config.jsToWatch = function() {
        return config.jsToValidate().map(function(js) {
            return js.src;
        });
    };

    config.wiredep = {
        src: './views/shared/_layout.cshtml',
        dest: './views/shared/',

        options: {
            bowerJson: require('./bower.json'),
            directory: './wwwroot/lib',
            ignorePath: '../../wwwroot',
        },
    };


    return config;
};