'use strict';

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

                if (this.hasAngularSpecs) {
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

    builder: function(name, htmlDestination, htmlInjectionTag, jsFolder, hasThirdPartyJs, lessFolder, isAngular) {
        return {
            name: name,
            htmlDestination: htmlDestination,

            hasJs: jsFolder ? true : false,
            js: {
                isAngular: isAngular,
                folder: jsFolder,
                files: isAngular ? [
                    jsFolder + 'app.js',
                    jsFolder + '*.controller.js',
                    jsFolder + '*.directive.js',
                    jsFolder + '*.service.js'
                ] : jsFolder + '*.js',
                specs: jsFolder + '*.spec.js',

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
    }
};