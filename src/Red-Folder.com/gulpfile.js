/// <binding />
"use strict";

/*
 * Setup
 */
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
/*
gulp.task('clean-css', function (done) {
    clean(config.destination.css, done);
});
*/
gulp.task('validate-less', function () {
    log("Validating Less");

    var tasks = config.lessToValidate().map(function (element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.lesshint())
            .pipe($.lesshint.reporter());
    });

    return merge(tasks);
});

gulp.task('compile-less', ['validate-less'], function () {
    log("Compiling Less to CSS");

    var tasks = config.lessToCompile().map(function (element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.less({
                paths: [path.join(__dirname, 'less', 'includes')]
            }))
            .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('autoprefix-css', ['compile-less'], function () {
    log("Autoprefixing CSS");

    var tasks = config.cssToValidate().map(function (element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.autoprefixer({ browser: ['last 2 versions', '> 5%'] }))
            .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('inject-css', ['compile-less', 'autoprefix-css'], function () {
    log("Injecting CSS");

    var tasks = config.cssToInject().map(function (element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++)
        {
            task = task.pipe($.inject(gulp.src(element.tags[i].css, {read: false}),
                                {
                                    ignorePath: element.tags[i].ignorePath,
                                    name: element.tags[i].tagName
                                }))
        }
        
        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('bundle-css', ['compile-less', 'autoprefix-css', 'inject-css'], function () {
    log('Bundle CSS');

    var tasks = config.cssToBundle().map(function (element) {
        return gulp.src(element.src)
                    .pipe($.print())
                    .pipe($.minifyCss())
                    .pipe($.concat(element.bundleName))
                    .pipe($.rev())
                    .pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});

gulp.task('deploy-css', ['bundle-css'], function () {
    log('Deploy CSS');

    var tasks = config.cssToDeploy().map(function (element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].css, { read: false }),
                                {
                                    ignorePath: element.tags[i].ignorePath,
                                    name: element.tags[i].tagName
                                }))
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});




/*
 * JavaScript
 */
//gulp.task('clean-js', function (done) {
//    clean(config.destination.js, done);
//});

gulp.task('validate-js', function () {
    log('Validating JS with JSHint and JSCS');

    var tasks = config.jsToValidate().map(function (element) {
        return gulp.src(element.src)
            .pipe($.print())
            .pipe($.jscs({ configPath: config.tools.jscsConfig }))
            .pipe($.jscs.reporter())
            .pipe($.jscs.reporter('fail'))
            .pipe($.jshint())
            .pipe($.jshint.reporter('jshint-stylish', { verbose: true }))
            .pipe($.jshint.reporter('fail'));
    });

    return merge(tasks);
});

gulp.task('inject-js', ['validate-js'], function () {
    log("Inject JS");

    var tasks = config.jsToInject().map(function (element) {
        var task = gulp.src(element.src)
                        .pipe($.print());

        for (var i = 0; i < element.tags.length; i++) {
            task = task.pipe($.inject(gulp.src(element.tags[i].js, { read: false }),
                                {
                                    ignorePath: element.tags[i].ignorePath,
                                    name: element.tags[i].tagName
                                }))
        }

        return task.pipe(gulp.dest(element.dest));
    });

    return merge(tasks);
});


gulp.task('minify-js', function () {
    log("Minify JavaScript")
    return gulp.src(config.source.js)
        .pipe($.uglify())
        .pipe($.rename({
            suffix: '.min'
        }))
        .pipe($.concat('site.js'))
        .pipe($.rev())
        .pipe(gulp.dest(config.destination.js));
});

gulp.task('inject', function () {
    var optionsList = config.wiredep.optionsList;
    var wiredep = require('wiredep').stream;

    var tmp = gulp.src(config.layoutFiles);

    optionsList.map(function (options)
    {
        tmp = tmp.pipe(wiredep(options));
    });

    return tmp
            .pipe($.inject(gulp.src(config.destination.js + "/**/*.js"),
                {
                    starttag: '<!-- injectdev:js -->',
                    endtag: '<!-- endinjectdev -->'
                }))
            .pipe(gulp.dest(config.layoutFolder));
            //.pipe(wiredep(action.options))
            
});

/*
 * Batched tasks
 */
gulp.task('deployment-prepare', function () {
    var argv = require('yargs').argv;

    var isRelease = argv.Release;

    if (isRelease) {
        log("Preparing for deployment");

        return gulp.start('deploy-css');
    }

    return gulp.noop;
});

/*
gulp.task('deployment-prepare-css', ['clean-css', 'compile-less', 'autoprefix-css']);
gulp.task('deployment-prepare-js', ['clean-js', 'validate-js', 'minify-js']);
gulp.task('deployment-prepare', ['deployment-prepare-css', 'deployment-prepare-js']);
*/

/*
 * File watchers
 */
gulp.task('watch-less', function () {
    return gulp.watch(config.lessToWatch(), ['validate-less','compile-less', 'autoprefix-css', 'inject-css']);
});


/*
 * Local utilities
 */
function clean(path, done) {
    log("Cleaning: " + path);
    del(path, done);
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