'use strict';

var path = require('path');

module.exports = function() {
    var utils = {};

    utils.AppBuilder = function (logger, name) {
        this.logger = logger;
        this.name = name;
        this.htmlDestination = null;
        
        this.angular = {
            folder: null,
            hasSpecs: false,
            hasThirdParty: false
        };

        this.js = {
            folder: null,
            hasThirdParty: false
        };

        this.less = {
            folder: null
        }

        this.setHtmlDestination = function (file) {
            this.htmlDestination = file;
            return this;
        },

        this.addAngularJs = function (folder) {
            this.angular.folder = folder;
            return this;
        },

        this.hasAngularSpecs = function () {
            this.angular.hasSpecs = true;
            return this;
        },

        this.hasAngularThirdPartyJs = function () {
            this.angular.hasThirdParty = true;
            return this;
        },

        this.addJs = function (folder) {
            this.js.folder = folder;
            return this;
        },

        this.hasThirdPartyJs = function () {
            this.js.hasThirdParty = true;
            return this;
        },

        this.addLess = function (folder) {
            this.less.folder = folder;
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
                
            if (this.angular.folder != null)
            {
                config.hasAngular = true;
                config.angular = {
                    folder: this.angular.folder,
                    files: [
                        this.angular.folder + 'app.js',
                        this.angular.folder + '*.controller.js',
                        this.angular.folder + '*.directive.js',
                        this.angular.folder + '*.service.js'
                    ],
                    development: {
                        htmlInjection: {
                            tagName: this.name + 'Development',
                        },
                    },
                    production: {
                        folder: this.angular.folder + 'production/',
                        bundleName: this.name + '.js',
                        htmlInjection: {
                            tagName: this.name + 'Production',
                        }
                    }
                };

                config.angular.hasSpecs = false;
                if (this.angular.hasSpecs) {
                    config.angular.hasSpecs = true;
                    config.angular.specs = this.angular.folder + '*.spec.js';
                }

                config.angular.hasThirdParty = false;
                if (this.angular.hasThirdParty) {
                    config.angular.hasThirdParty = true;
                }
            }

            if (this.js.folder != null)
            {
                config.hasJs = true;
                config.js = {
                    folder: this.js.folder,
                    files: this.js.folder + '*.js',
                    development: {
                        htmlInjection: {
                            tagName: this.name + 'Development',
                        },
                    },
                    production: {
                        folder: this.js.folder + 'production/',
                        bundleName: this.name + '.js',
                        htmlInjection: {
                            tagName: this.name + 'Production',
                        }
                    }
                };

                config.js.hasThirdParty = false;
                if (this.js.hasThirdParty) {
                    config.js.hasThirdParty = true;
                }
            }

            if (this.less.folder != null)
            {
                config.hasLess = true;
                config.less = {
                    folder: this.less.folder,
                    development: {
                        htmlInjection: {
                            tagName: this.name + 'Development',
                        },
                    },
                    production: {
                        folder: this.less.folder + 'production/',
                        bundleName: this.name + '.css',
                        htmlInjection: {
                            tagName: this.name + 'Production',
                        }
                    }
                };
            }

            return config;
        }
    };

    utils.SuiteBuilder = function (logger, apps) {
        this.logger = logger;
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
                if (app.hasJs || app.hasAngular) {
                    if (results.filter(function (result) { return result.src === app.htmlDestination; }).length === 0) {
                        results.push({
                            src: app.htmlDestination,
                            dest: path.dirname(app.htmlDestination),
                            tags: [],
                        });
                    }

                    var tags = results.filter(function (result) { return result.src === app.htmlDestination; })[0].tags;

                    if (app.hasJs) {
                        if (tags.filter(function (tag) { return tag.tagName == app.js.development.htmlInjection.tagName; }).length === 0) {
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
                    if (app.hasAngular) {
                        if (tags.filter(function (tag) { return tag.tagName == app.angular.development.htmlInjection.tagName; }).length === 0) {
                            tags.push({
                                ignorePath: '/wwwroot',
                                tagName: app.angular.development.htmlInjection.tagName,
                                js: [],
                            });
                        }

                        var js = tags.filter(function (tag) { return tag.tagName == app.angular.development.htmlInjection.tagName; })[0].js;
                        if (app.angular.hasThirdParty) {
                            js.push(app.angular.folder + '3rdParty/*.js');
                        }
                        Array.prototype.push.apply(js, app.angular.files);
                    }
                }
            });

            return results;
        };

        this.BuildJsToBundle = function () {
            return [].concat(
                this.apps.filter(function (app) {
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
                }),
                this.apps.filter(function (app) {
                    return app.hasAngular;
                }).map(function (app) {
                    var src = [];
                    if (app.angular.hasSpecs)
                    {
                        src.push(app.angular.folder + '3rdParty/*.js');
                    }

                    Array.prototype.push.apply(src, app.angular.files);
                    return {
                        src: src,
                        dest: app.angular.production.folder,
                        bundleName: app.angular.production.bundleName,
                    };
                })
            );
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

    return utils;
};