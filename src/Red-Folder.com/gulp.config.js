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
                './Utils/SpecServer/app.js'
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
                            .addLess('./wwwroot/css/shared/')
                            .build(),

        // DependancyGraph
        new rfcUtils.AppBuilder(logger, 'DependancyGraph')
                            .setHtmlDestination('./views/Microservice/Index.cshtml')
                            .addJs('./wwwroot/scripts/dependancyGraph/')
                            .addLess('./wwwroot/css/dependancyGraph/')
                            .build(),

        // WebCrawlGraph
        new rfcUtils.AppBuilder(logger, 'webCrawlGraph')
                            .setHtmlDestination('./views/Samples/WebCrawlGraph.cshtml')
                            .addAngularJs('./wwwroot/scripts/webCrawlGraph/')
                            .addLess('./wwwroot/css/webCrawlGraph/')
                            .build(),

        // repoExplorer
        new rfcUtils.AppBuilder(logger, 'repoExplorer')
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
            watch: config.tools.src
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

    config.karma = getKarmaOptions();

    return config;

    function getKarmaOptions() {
        var options = {
            files: [
                './wwwroot/lib/angular/angular.js',
                './wwwroot/lib/angular-resource/angular-resource.js',
                './wwwroot/lib/angular-mocks/angular-mocks.js',
                './wwwroot/lib/bardjs/dist/bard.js',

                './wwwroot/lib/jquery/dist/jquery.js',
                './wwwroot/lib/bootstrap/dist/js/bootstrap.js',
                './wwwroot/lib/bootstrap-switch/dist/js/bootstrap-switch.js',
                './wwwroot/lib/angular-bootstrap-switch/dist/angular-bootstrap-switch.js',

                './wwwroot/scripts/*/app.js',
                './wwwroot/scripts/*/*.controller.js',
                './wwwroot/scripts/*/*.directive.js',
                './wwwroot/scripts/*/*.service.js',
                './wwwroot/scripts/*/*.filter.js',
                './wwwroot/scripts/*/*.spec.js'
            ],
            exclude: [],
            coverage: {
                dir: report + 'coverage',
                reporters: [
                    // reporters not supporting the `file` property
                    { type: 'html' },
                    { type: 'cobertura', subdir: 'cobertura', file: 'cobertura.txt' },
                    { type: 'lcov', subdir: 'report-lcov' },
                    { type: 'text-summary' } //, subdir: '.', file: 'text-summary.txt'}
                ]
            },
            junitReporter: {
                outputDir: report + 'results'
            },
            preprocessors: {}
        };
        options.preprocessors['./wwwroot/scripts/**/!(*.spec)+(.js)'] = ['coverage'];
        return options;
    }
};
