// Karma configuration
// Generated on Sat May 07 2016 18:01:33 GMT+0100 (GMT Daylight Time)

module.exports = function (config) {
  var gulpConfig = require('./gulp.config')();

  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['mocha', 'chai', 'sinon', 'chai-sinon'],


    // list of files / patterns to load in the browser
    files: gulpConfig.karma.files,
      // TODO
      // Need to load the source files from the config.  Then need to get the spec files.
    

    // list of files to exclude
    exclude: gulpConfig.karma.exclude,


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: gulpConfig.karma.preprocessors,

    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress', 'coverage'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['PhantomJS'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: true,

    // Concurrency level
    // how many browser should be started simultaneous
    concurrency: Infinity,

    // Notify karma of the available plugins
    //plugins: [
    //    'karma-jasmine',
    //    'karma-phantomjs-launcher',
    //    'karma-html-detailed-reporter'
    //],

    coverageReporter: {
        dir: gulpConfig.karma.coverage.dir,
        reporters: gulpConfig.karma.coverage.reporters
    }

  })
}
