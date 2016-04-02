/// <binding AfterBuild='less, minify' />
"use strict";

var gulp = require('gulp');
/*
var less = require('gulp-less');
var uglify = require('gulp-uglify');
var jshint = require('gulp-jshint');
var jscs = require('gulp-jscs');
var using = require('gulp-using');
var util = require('gulp-util');
var gulpprint = require('gulp-print');
*/

var $ = require('gulp-load-plugins')({ lazy: true });

var path = require('path');

var paths = {
    wwwroot: "./wwwroot/"
};

paths.lessSrc = paths.wwwroot + "css/**/*.less";
paths.lessDest = paths.wwwroot + "css/";
paths.jsSrc = [paths.wwwroot + "scripts/**/*.js", "!" + paths.wwwroot + "scripts/min/**/*.js"];
paths.jsDest = paths.wwwroot + "scripts/min";
paths.jscsConfig = "./.jsjc.json";

gulp.task('js-validate', function () {
    log('Analysing source with JSHint and JSCS');

    return gulp.src(paths.jsSrc)
        //.pipe(using())
        .pipe($.print())
        //.pipe($.jscs({ configPath: paths.jscsConfig }))
        //.pipe($.jscs.reporter())
        //.pipe($.jscs.reporter('fail'))
        .pipe($.jshint())
        .pipe($.jshint.reporter('jshint-stylish', { verbose: true }))
        .pipe($.jshint.reporter('fail'));
});

gulp.task('less', function () {
    return gulp.src(paths.lessSrc)
      .pipe($.less({
          paths: [path.join(__dirname, 'less', 'includes')]
      }))
      .pipe(gulp.dest(paths.lessDest));
});

gulp.task('watch-less', function() {
    return gulp.watch(paths.lessSrc, ['less']);
});

gulp.task('minify', function () {
    return gulp.src(paths.jsSrc)
        .pipe($.uglify())
        .pipe(gulp.dest(paths.jsDest));
});

gulp.task('deployment-prepare', ['less', 'minify']);



///////////////////
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