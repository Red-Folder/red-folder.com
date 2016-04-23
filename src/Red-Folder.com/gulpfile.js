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
var config = require('./gulp.config')();
var merge = require('merge-stream');
var $ = require('gulp-load-plugins')({ lazy: true });

/*
 * CSS
 */
gulp.task('validate-less', function() {
    log('Validating Less');

    var tasks = config.lessToValidate().map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.lesshint())
            .pipe($.lesshint.reporter());
    });

    return merge(tasks);
});

gulp.task('compile-less', ['validate-less'], function() {
    log('Compiling Less to CSS');

    var tasks = config.lessToCompile().map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.less({
                paths: [path.join(__dirname, 'less', 'includes')],
            }))
            .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('autoprefix-css', ['compile-less'], function() {
    log('Autoprefixing CSS');

    var tasks = config.cssToAutoPrefix().map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.autoprefixer({ browser: ['last 2 versions', '> 5%'], }))
            .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('inject-css', ['compile-less', 'autoprefix-css'], function() {
    log('Injecting CSS');

    var tasks = config.cssToInject().map(function(element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].css, { read: false }), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName,
            }));
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('bundle-css', ['compile-less', 'autoprefix-css', 'inject-css'], function() {
    log('Bundle CSS');

    var tasks = config.cssToBundle().map(function(element) {
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

    var tasks = config.cssToDeploy().map(function(element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].css, { read: false, }), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName,
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

    var tasks = config.jsToValidate().map(function(element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.jscs({ configPath: config.tools.jscsConfig }))
            .pipe($.jscs.reporter())
            .pipe($.jscs.reporter('fail'))
            .pipe($.jshint())
            .pipe($.jshint.reporter('jshint-stylish', { verbose: true, }))
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

    var tasks = config.jsToInject().map(function(element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].js, { read: false, }), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName,
            }));
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('bundle-js', ['inject-js'], function() {
    log('Bundle JS');

    var tasks = config.jsToBundle().map(function(element) {
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

    var tasks = config.jsToDeploy().map(function(element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].js, { read: false, }), {
                ignorePath: element.tags[i].ignorePath,
                name: element.tags[i].tagName,
            }));
        }

        return task.pipe(gulp.dest(element.dest));
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

/*
 * File watchers
 */
gulp.task('watch-less', function() {
    return gulp.watch(config.lessToWatch(), ['validate-less','compile-less', 'autoprefix-css', 'inject-css']);
});

gulp.task('watch-js', function() {
    return gulp.watch(config.jsToWatch(), ['validate-js', 'inject-js']);
});

function log(msg) {
    if (typeof (msg) === 'object') {
        for (var item in msg) {
            if (msg.hasOwnProperty(item)) {
                $.util.log($.util.colors.blue(msg[item]));
            }
        }
    } else {
        $.util.log($.util.colors.blue(msg));
    }
}

gulp.task('validate-gulp', function() {
    log('Validating Gulp & Config');

    return gulp.src(['./gulpfile.js', './gulp.config.js'])
        .pipe($.print())
        .pipe($.jscs({ configPath: config.tools.jscsConfig }))
        .pipe($.jscs.reporter())
        .pipe($.jscs.reporter('fail'))
        .pipe($.jshint())
        .pipe($.jshint.reporter('jshint-stylish', { verbose: true, }))
        .pipe($.jshint.reporter('fail'));
});
