'use strict';

/* jshint node: true */
var path = require('path');
var rfcUtils = require('./Utils/rfcUtils.js');

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
        new rfcUtils.AppBuilder('shared')
                            .setHtmlDestination('./views/shared/_layout.cshtml')
                            .addJs('./wwwroot/scripts/shared/')
                            .hasThirdPartyJs()
                            .addLess('./wwwroot/css/shared/')
                            .build(),

        // DependancyGraph
        new rfcUtils.AppBuilder('DependancyGraph')
                            .setHtmlDestination('./views/Microservice/Index.cshtml')
                            .addJs('./wwwroot/scripts/dependancyGraph/')
                            .addLess('./wwwroot/css/dependancyGraph/')
                            .build(),

        // repoExplorer
        new rfcUtils.AppBuilder('repoExplorer')
                            .setHtmlDestination('./views/Home/Repo.cshtml')
                            .addAngularJs('./wwwroot/scripts/repoExplorer/')
                            .hasAngularSpecs()
                            .build()
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
                src: app.js.files,
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
            js.push(app.js.files);
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
                    app.js.files,
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

    config.specsToBuild = function () {
        return config.apps.filter(function (app) {
            return app.hasAngular;
        }).map(function (app) {
            return {
                src: app.angular.files,
                testlibraries: [
                    'node_modules/mocha/mocha.js',
                    'node_modules/chai/chai.js',
                    'node_modules/mocha-clean/index.js',
                    'node_modules/sinon-chai/lib/sinon-chai.js'
                ],
                specs: app.angular.specs,
                name: app.name
            };
        });
    }

    config.wiredep = {
        src: './views/shared/_layout.cshtml',
        dest: './views/shared/',

        options: {
            bowerJson: require('./bower.json'),
            directory: './wwwroot/lib',
            ignorePath: '../../wwwroot',
        },
    };

    config.specRunner = {
        server: {
            script: './Utils/SpecServer/app.js',
            delayTime: 1,
            env: {
                'PORT': 8001
            },
            watch: './Utils/SpecServer/app.js'
        },

        browserReloadDelay: 1000,
        
        specTemplate: './Utils/SpecServer/templates/spec.html',
        specOutput: './Utils/SpecServer/output',
        startPath: './Utils/SpecServer/output/repoExplorer.html',

        wiredep: {
            options: {
                bowerJson: require('./bower.json'),
                devDependencies: true
            }
        }
    };


    return config;
};