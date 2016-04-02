/// <binding AfterBuild='less, minify' />
"use strict";

var gulp = require('gulp');
var less = require('gulp-less');
var uglify = require('gulp-uglify');
var path = require('path');

var paths = {
    wwwroot: "./wwwroot/"
};

paths.lessSrc = paths.wwwroot + "css/**/*.less";
paths.lessDest = paths.wwwroot + "css/";
paths.jsSrc = [paths.wwwroot + "scripts/**/*.js", "!" + paths.wwwroot + "scripts/**/*.js"];
paths.jsDest = paths.wwwroot + "scripts/min";

gulp.task('less', function () {
    return gulp.src(paths.lessSrc)
      .pipe(less({
          paths: [path.join(__dirname, 'less', 'includes')]
      }))
      .pipe(gulp.dest(paths.lessDest));
});

gulp.task('watch-less', function() {
    return gulp.watch(paths.lessSrc, ['less']);
});

gulp.task('minify', function () {
    return gulp.src(paths.jsSrc)
        .pipe(uglify())
        .pipe(gulp.dest(paths.jsDest));
});

gulp.task('deployment-prepare', ['less', 'minify']);

