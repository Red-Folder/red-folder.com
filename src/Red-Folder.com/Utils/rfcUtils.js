'use strict';

module.exports = {
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