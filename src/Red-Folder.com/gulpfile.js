/// <binding AfterBuild='less' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
"use strict";

var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');

var paths = {
    wwwroot: "./wwwroot/"
};

paths.lessSrc = paths.wwwroot + "css/**/*.less";
paths.lessDest = paths.wwwroot + "css/";

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
