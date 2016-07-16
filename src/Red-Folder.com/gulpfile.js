// jscs: disable
/// <binding />
// jscs: enable

'use strict';

/*
 * Setup
 */
/* jshint node: true */
var gulp = require('gulp');
var path = require('path');
var del = require('del');
var eventStream = require('event-stream');
var merge = require('merge-stream');
var browserSync = require('browser-sync');
var $ = require('gulp-load-plugins')({lazy: true});

var config = require('./gulp.config')(log);

var port = 8001;    //config.defaultPort;

/*
 * CSS
 */
gulp.task('validate-less', function() {
    log('Validating Less');

    var tasks = config.lessToValidate.map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.lesshint())
            .pipe($.lesshint.reporter());
    });

    return merge(tasks);
});

gulp.task('compile-less', ['validate-less'], function() {
    log('Compiling Less to CSS');

    var tasks = config.lessToCompile.map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.less({
                paths: [path.join(__dirname, 'less', 'includes')]
            }))
            .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('autoprefix-css', ['compile-less'], function() {
    log('Autoprefixing CSS');

    var tasks = config.cssToAutoPrefix.map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.autoprefixer({browser: ['last 2 versions', '> 5%']}))
            .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('inject-css', ['compile-less', 'autoprefix-css'], function() {
    log('Injecting CSS');

    var tasks = config.cssToInject.map(function(element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].css, {read: false}), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName
            }));
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('bundle-css', ['compile-less', 'autoprefix-css', 'inject-css'], function() {
    log('Bundle CSS');

    var tasks = config.cssToBundle.map(function(element) {
        return gulp.src(element.src)
                    .pipe($.print())
                    .pipe($.minifyCss())
                    .pipe($.concat(element.bundleName))
                    .pipe($.rev())
                    .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('deploy-css', ['bundle-css'], function() {
    log('Deploy CSS');

    var tasks = config.cssToDeploy.map(function(element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].css, {read: false}), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName
            }));
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

/*
 * JavaScript
 */
gulp.task('validate-js', function() {
    log('Validating JS with JSHint and JSCS');

    var tasks = config.jsToValidate.map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.jscs({configPath: config.tools.jscsConfig}))
            .pipe($.jscs.reporter())
            .pipe($.jscs.reporter('fail'))
            .pipe($.jshint())
            .pipe($.jshint.reporter('jshint-stylish', {verbose: true}))
            .pipe($.jshint.reporter('fail'));
    });

    return merge(tasks);
});

gulp.task('inject-bower', function() {
    log('Inject Bower dependancies');

    var wiredep = require('wiredep').stream;

    return gulp.src(config.wiredep.src)
                .pipe($.print())
                .pipe(wiredep(config.wiredep.options))
                .pipe(gulp.dest(config.wiredep.dest));
});

gulp.task('inject-js', ['validate-js', 'inject-bower'], function() {
    log('Inject JS');

    var tasks = config.jsToInject.map(function (element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].js).pipe($.angularFilesort()), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName
            }));
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('bundle-js', ['inject-js'], function() {
    log('Bundle JS');

    var tasks = config.jsToBundle.map(function(element) {
        return gulp.src(element.src)
                    .pipe($.print())
                    .pipe($.uglify())
                    .pipe($.concat(element.bundleName))
                    .pipe($.rev())
                    .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('deploy-js', ['bundle-js', 'inject-bower'], function() {
    log('Deploy JS');

    var tasks = config.jsToDeploy.map(function(element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].js, {read: false}), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName
            }));
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

/*
 * Testing
 */
gulp.task('karma', function (done) {
    startTests(true /* singleRun */, done);
});

gulp.task('serve-specs', ['build-specs'], function (done) {
    log('run the spec runner');

    $.nodemon(config.specRunner.server)
        .on('restart', /*['vet'],*/ function (ev) {
            log('*** nodemon restarted');
            log('files changed:\n' + ev);
            setTimeout(function () {
                browserSync.notify('reloading now ...');
                browserSync.reload({stream: false});
            }, config.specRunner.browserReloadDelay);
        })
        .on('start', function () {
            log('*** nodemon started');
            startBrowserSync();
        })
        .on('crash', function () {
            log('*** nodemon crashed: script crashed for some reason');
        })
        .on('exit', function () {
            log('*** nodemon exited cleanly');
        });

    done();
});

gulp.task('build-specs', function (done) {
    log('building the spec runner pages');

    var wiredep = require('wiredep').stream;

    var tasks = config.specsToBuild.map(function (element) {
        return gulp
            .src(config.specRunner.specTemplate)
            .pipe(wiredep(config.specRunner.wiredep.options))
            .pipe($.inject(gulp.src(element.src, {read: false}), {relative: true}))
            .pipe($.inject(gulp.src(element.testlibraries, {read: false}), {name: 'testlibraries', relative: true}))
            .pipe($.inject(gulp.src(element.specs, {read: false}), {name: 'specs', relative: true}))
            .pipe($.rename(element.name + '.html'))
            .pipe(gulp.dest(config.specRunner.specOutput));
    });

    return merge(tasks);
});

/*
 * Batched tasks
 */
gulp.task('deployment-prepare', function() {
    var argv = require('yargs').argv;

    var isRelease = argv.Release;

    if (isRelease) {
        log('Preparing for deployment');

        return gulp.start('deploy-css')
                .start('deploy-js');
    }

    return gulp.noop;
});

gulp.task('validate-tools', function () {
    log('Validating Gulp, Config & tools');

    return gulp.src(config.tools.src)
        .pipe($.print())
        .pipe($.jscs({configPath: config.tools.jscsConfig}))
        .pipe($.jscs.reporter())
        .pipe($.jscs.reporter('fail'))
        .pipe($.jshint())
        .pipe($.jshint.reporter('jshint-stylish', {verbose: true}))
        .pipe($.jshint.reporter('fail'));
});

/*
 * File watchers
 */
gulp.task('watch-less', function() {
    return gulp.watch(config.lessToWatch, ['validate-less','compile-less', 'autoprefix-css', 'inject-css']);
});

gulp.task('watch-js', function() {
    return gulp.watch(config.jsToWatch, ['validate-js', 'inject-js']);
});

/*
 * Methods
 */
function log(msg) {
    if (typeof (msg) === 'object') {
        $.util.log($.util.colors.blue(JSON.stringify(msg)));
        //for (var item in msg) {
        //    if (msg.hasOwnProperty(item)) {
        //        $.util.log($.util.colors.blue(msg[item]));
        //    }
        //}
    } else {
        $.util.log($.util.colors.blue(msg));
    }
}

// Gulp example with Karma directly
function startTests(singleRun, done) {
    log('Karma started');
    var child;
    var excludeFiles = [];
    var fork = require('child_process').fork;
    var Server = require('karma').Server;
    var serverSpecs = config.serverIntegrationSpecs;

    new Server({
        configFile: __dirname + '/karma.conf.js',
        exclude: excludeFiles,
        singleRun: !!singleRun
    }, karmaCompleted).start();

    ////////////////

    function karmaCompleted(karmaResult) {
        log('Karma completed');
        if (child) {
            log('shutting down the child process');
            child.kill();
        }
        if (karmaResult === 1) {
            done('karma: tests failed with code ' + karmaResult);
        } else {
            done();
        }
    }
}

function startBrowserSync() {
    log('Starting BrowserSync on port ' + port);

    // TODO - Load these from the config
    // Will need to cycle through all of the relevant JavaScript

    gulp.watch(config.lessToCompile.map(function(element) { return element.src; }), ['compile-less'])
        .on('change', changeEvent);
    
    var options = {
        proxy: 'localhost:' + port,
        port: 3000,
        files: [
            './wwwroot/scripts/**/*.*',
            './wwwroot/css/**/*.css'
        ],
        ghostMode: { // these are the defaults t,f,t,t
            clicks: true,
            location: false,
            forms: true,
            scroll: true
        },
        injectChanges: true,
        logFileChanges: true,
        logLevel: 'debug',
        logPrefix: 'gulp-patterns',
        notify: true,
        reloadDelay: 0, //1000
        startPath: config.specRunner.startPath
    };

    browserSync(options);
}

function changeEvent(event) {
    var srcPattern = new RegExp('/.*(?=/' + config.source + ')/');
    log('File ' + event.path.replace(srcPattern, '') + ' ' + event.type);
}