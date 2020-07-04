'use strict';

/* jshint node: true */

var rfcUtils = require('./Utils/rfcUtils.js')();
var report = './report/';

module.exports = function (logger) {
    var config = {
        source: {
            root: './wwwroot-src/',
        },
        destination: {
            root: './wwwroot/',
        },
        tools: {
            jscsConfig: './.jsjc.json',
            src: [
                './gulpfile.js',
                './gulp.config.js',
                './Utils/rfcUtils.js',
            ]
        },
    };

    var suite = new rfcUtils.SuiteBuilder(logger,
        [
            // Shared
            new rfcUtils.AppBuilder(logger, 'shared')
                .setHtmlDestination('./views/shared/_layout.cshtml')
                .addJs('./wwwroot/scripts/shared/')
                .hasThirdPartyJs()
                .build()
    ]);

    config.lessToCompile = suite.BuildLessToCompile();
    config.lessToWatch = suite.BuildLessToWatch();
    config.lessToValidate = suite.BuildLessToValidate();

    config.cssToAutoPrefix = suite.BuildCssToAutoPrefix();
    config.cssToInject = suite.BuildCssToInject();
    config.cssToBundle = suite.BuildCssToBundle();
    config.cssToDeploy = suite.BuildCssToDeploy();

    config.jsToValidate = suite.BuildJsToValidate();
    config.jsToInject = suite.BuildJsToInject();
    config.jsToBundle = suite.BuildJsToBundle();
    config.jsToDeploy = suite.BuildJsToDeploy();
    config.jsToWatch = suite.BuildJsToWatch();

    config.specsToBuild = suite.BuildSpecsToBuild();

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
