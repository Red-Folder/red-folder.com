/// <binding />
"use strict";

/*
 * Setup
 */
var gulp = require('gulp');
var path = require('path');
var del = require('del');
var config = require('./gulp.config')();
var $ = require('gulp-load-plugins')({ lazy: true });

/*
 * CSS
 */
gulp.task('clean-css', function () {
    clean(config.destination.css);
});

gulp.task('compile-less', function () {
    log("Compiling Less to CSS");
    return gulp.src(config.source.less)
        .pipe($.print())
        .pipe($.less({
            paths: [path.join(__dirname, 'less', 'includes')]
        }))
        .pipe(gulp.dest(config.destination.css));
});

gulp.task('autoprefix-css', function () {
    log("Autoprefix CSS");
    return gulp.src(config.destination.css + '*.css')
        .pipe($.print())
        .pipe($.autoprefixer({ browser: ['last 2 versions', '> 5%'] }))
        .pipe(gulp.dest(config.destination.css));
});

/*
 * JavaScript
 */
gulp.task('clean-js', function () {
    clean(config.destination.js);
});

gulp.task('validate-js', function () {
    log('Analysing source with JSHint and JSCS');

    return gulp.src(config.source.jsToValidate)
        .pipe($.print())
        //.pipe($.jscs({ configPath: config.tools.jscsConfig }))
        //.pipe($.jscs.reporter())
        //.pipe($.jscs.reporter('fail'))
        .pipe($.jshint())
        .pipe($.jshint.reporter('jshint-stylish', { verbose: true }))
        .pipe($.jshint.reporter('fail'));
});

gulp.task('minify-js', function () {
    log("Minify JavaScript")
    return gulp.src(config.source.js)
        .pipe($.uglify())
        .pipe($.rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest(config.destination.js));
});

/*
 * Batched tasks
 */
gulp.task('deployment-prepare-css', ['clean-css', 'compile-less', 'autoprefix-css']);
gulp.task('deployment-prepare-js', ['clean-js', 'validate-js', 'minify-js']);
gulp.task('deployment-prepare', ['deployment-prepare-css', 'deployment-prepare-js']);

/*
 * File watchers
 */
gulp.task('watch-less', function () {
    return gulp.watch(config.source.less, ['less']);
});

/*
 * Local utilities
 */
function clean(path) {
    log("Cleaning: " + path);
    del(path);
}

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