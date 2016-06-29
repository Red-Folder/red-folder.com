'use strict';

/* jshint node: true */

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

    var suite = new rfcUtils.SuiteBuilder([
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