'use strict';

var path = require('path');

module.exports = {
    AppBuilder: function(name) {
        this.name = name;
        this.htmlDestination = null;
        
        this.angularFolder = null;
        this.hasAngularSpecs = false;
        this.hasAngularThirdPartyJs = null;

        this.jsFolder = null;
        this.hasThirdPartyJs = null;

        this.lessFolder = null;

        this.setHtmlDestination = function (file) {
            this.htmlDestination = file;
            return this;
        },

        this.addAngularJs = function (folder) {
            this.angularFolder = folder;
            return this;
        },

        this.hasAngularSpecs = function () {
            this.hasAngularSpecs = true;
            return this;
        },

        this.hasAngularThirdPartyJs = function () {
            this.hasAngularThirdPartyJs = true;
            return this;
        },

        this.addJs = function (folder) {
            this.jsFolder = folder;
            return this;
        },

        this.hasThirdPartyJs = function () {
            this.hasThirdPartyJs = true;
            return this;
        },

        this.addLess = function (folder) {
            this.lessFolder = folder;
            return this;
        },

        this.build = function () {
            var config = {
                name: this.name,
                hasAngular: false,
                hasJs: false,
                hasLess: false
            };

            if (this.htmlDestination != null)
            {
                config.htmlDestination = this.htmlDestination;
            }
                
            if (this.angularFolder != null)
            {
                config.hasAngular = true;
                config.angular = {
                    folder: this.angularFolder,
                    files: [
                        this.angularFolder + 'app.js',
                        this.angularFolder + '*.controller.js',
                        this.angularFolder + '*.directive.js',
                        this.angularFolder + '*.service.js'
                    ],
                    development: {
                        htmlInjection: {
                            tagName: this.name + 'Development',
                        },
                    },
                    production: {
                        folder: this.angularFolder + 'production/',
                        bundleName: this.name + '.js',
                        htmlInjection: {
                            tagName: this.name + 'Production',
                        }
                    }
                };

                config.angular.hasSpecs = false;
                if (this.hasAngularSpecs) {
                    config.angular.hasSpecs = true;
                    config.angular.specs = this.angularFolder + '*.spec.js';
                }

                if (this.hasAngularThirdPartyJs != null) {
                    config.angular.hasThirdParty = true;
                }
            }

            if (this.jsFolder != null)
            {
                config.hasJs = true;
                config.js = {
                    folder: this.jsFolder,
                    files: this.jsFolder + '*.js',
                    development: {
                        htmlInjection: {
                            tagName: this.name + 'Development',
                        },
                    },
                    production: {
                        folder: this.angularFolder + 'production/',
                        bundleName: this.name + '.js',
                        htmlInjection: {
                            tagName: this.name + 'Production',
                        }
                    }
                };

                if (this.hasThirdPartyJs) {
                    config.js.hasThirdParty = true;
                }
            }

            if (this.lessFolder != null)
            {
                config.hasLess = true;
                config.less = {
                    folder: this.lessFolder,
                    development: {
                        htmlInjection: {
                            tagName: this.name + 'Development',
                        },
                    },
                    production: {
                        folder: this.lessFolder + 'production/',
                        bundleName: this.name + '.css',
                        htmlInjection: {
                            tagName: this.name + 'Production',
                        }
                    }
                };
            }

            return config;
        }
    },

    SuiteBuilder: function (apps) {
        this.apps = apps;

        this.BuildLessToCompile = function () {
            return this.apps.filter(function (app) {
                return app.hasLess;
            }).map(function (app) {
                return {
                    src: app.less.folder + '*.less',
                    dest: app.less.folder,
                };
            });
        };

        this.BuildLessToWatch = function () {
            return this.BuildLessToCompile().map(function (less) {
                return less.src;
            });
        };

        this.BuildLessToValidate = function () {
            return this.apps.filter(function (app) {
                return app.hasLess;
            }).map(function (app) {
                return {
                    src: app.less.folder + '*.less',
                    dest: app.less.folder,
                };
            });
        };

        this.BuildCssToAutoPrefix = function () {
            return this.apps.filter(function (app) {
                return app.hasLess;
            }).map(function (app) {
                return {
                    src: app.less.folder + '*.css',
                    dest: app.less.folder,
                };
            });
        };

        this.BuildCssToInject = function () {
            var results = [];
            this.apps.forEach(function (app) {
                if (app.hasLess) {
                    if (results.filter(function (result) { return result.src === app.htmlDestination; }).length === 0) {
                        results.push({
                            src: app.htmlDestination,
                            dest: path.dirname(app.htmlDestination),
                            tags: [],
                        });
                    }

                    var tags = results.filter(function (result) { return result.src === app.htmlDestination; })[0].tags;

                    if (tags.filter(function (tag) { return tag.tagName == app.less.development.htmlInjection.tagName; })) {
                        tags.push({
                            ignorePath: '/wwwroot',
                            tagName: app.less.development.htmlInjection.tagName,
                            css: [],
                        });
                    }

                    var css = tags.filter(function (tag) { return tag.tagName == app.less.development.htmlInjection.tagName; })[0].css;
                    css.push(app.less.folder + '*.css');
                }
            });

            return results;
        };

        this.BuildCssToBundle = function () {
            return this.apps.filter(function (app) {
                return app.hasLess;
            }).map(function (app) {
                return {
                    src: app.less.folder + '*.css',
                    dest: app.less.production.folder,
                    bundleName: app.less.production.bundleName,
                };
            });
        };

        this.BuildCssToDeploy = function () {
            var results = [];
            this.apps.forEach(function (app) {
                if (app.hasLess) {
                    if (results.filter(function (result) { return result.src === app.htmlDestination; }).length === 0) {
                        results.push({
                            src: app.htmlDestination,
                            dest: path.dirname(app.htmlDestination),
                            tags: [],
                        });
                    }

                    var tags = results.filter(function (result) { return result.src === app.htmlDestination; })[0].tags;

                    if (tags.filter(function (tag) { return tag.tagName == app.less.production.htmlInjection.tagName; })) {
                        tags.push({
                            ignorePath: '/wwwroot',
                            tagName: app.less.production.htmlInjection.tagName,
                            css: [],
                        });
                    }

                    var css = tags.filter(function (tag) { return tag.tagName == app.less.production.htmlInjection.tagName; })[0].css;
                    css.push(app.less.production.folder + '*.css');
                }
            });

            return results;
        };

        this.BuildJsToValidate = function () {
            return [].concat(
                    this.apps.filter(function (app) {
                        return app.hasJs;
                    }).map(function (app) {
                        return {
                            src: app.js.files,
                            dest: app.js.folder,
                        };
                    }),
                    this.apps.filter(function (app) {
                        return app.hasAngular;
                    }).map(function (app) {
                        return {
                            src: app.angular.files,
                            dest: app.angular.folder,
                        };
                    }),
                    this.apps.filter(function (app) {
                        return app.hasAngular && app.angular.hasSpecs;
                    }).map(function (app) {
                        return {
                            src: app.angular.specs,
                            dest: app.angular.folder,
                        };
                    })
            );
        };

        this.BuildJsToInject = function () {
            var results = [];
            this.apps.forEach(function (app) {
                if (app.hasJs) {
                    if (results.filter(function (result) { return result.src === app.htmlDestination; }).length === 0) {
                        results.push({
                            src: app.htmlDestination,
                            dest: path.dirname(app.htmlDestination),
                            tags: [],
                        });
                    }

                    var tags = results.filter(function (result) { return result.src === app.htmlDestination; })[0].tags;

                    if (tags.filter(function (tag) { return tag.tagName == app.js.development.htmlInjection.tagName; })) {
                        tags.push({
                            ignorePath: '/wwwroot',
                            tagName: app.js.development.htmlInjection.tagName,
                            js: [],
                        });
                    }

                    var js = tags.filter(function (tag) { return tag.tagName == app.js.development.htmlInjection.tagName; })[0].js;
                    if (app.js.hasThirdParty) {
                        js.push(app.js.folder + '3rdParty/*.js');
                    }
                    js.push(app.js.files);
                }
            });

            return results;
        };

        this.BuildJsToBundle = function () {
            return this.apps.filter(function (app) {
                return app.hasJs;
            }).map(function (app) {
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

        this.BuildJsToDeploy = function () {
            var results = [];
            this.apps.forEach(function (app) {
                if (app.hasJs) {
                    if (results.filter(function (result) { return result.src === app.htmlDestination; }).length === 0) {
                        results.push({
                            src: app.htmlDestination,
                            dest: path.dirname(app.htmlDestination),
                            tags: [],
                        });
                    }

                    var tags = results.filter(function (result) { return result.src === app.htmlDestination; })[0].tags;

                    if (tags.filter(function (tag) { return tag.tagName == app.js.production.htmlInjection.tagName; })) {
                        tags.push({
                            ignorePath: '/wwwroot',
                            tagName: app.js.production.htmlInjection.tagName,
                            js: [],
                        });
                    }

                    var js = tags.filter(function (tag) { return tag.tagName == app.js.production.htmlInjection.tagName; })[0].js;
                    js.push(app.js.production.folder + '*.js');
                }
            });

            return results;
        };

        this.BuildJsToWatch = function () {
            return this.BuildJsToValidate().map(function (js) {
                return js.src;
            });
        };

        this.BuildSpecsToBuild = function () {
            return this.apps.filter(function (app) {
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

    }
};