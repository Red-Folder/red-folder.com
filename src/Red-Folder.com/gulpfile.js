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
var browserSync = require('browser-sync');
var $ = require('gulp-load-plugins')({ lazy: true });

var port = 8001;    //config.defaultPort;

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
            task = task.pipe($.inject(gulp.src(element.tags[i].js).pipe($.angularFilesort()), {
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
 * Testing
 */
gulp.task('karma', function (done) {
    startTests(true /* singleRun */, done);
});

gulp.task('serve-specs', ['build-specs'], function (done) {
    log('run the spec runner');
    serve(true /* isDev */, true /* specRunner */);
    done();
});

gulp.task('build-specs', function (done) {
    log('building the spec runner');

    var wiredep = require('wiredep').stream;
    //var templateCache = config.temp + config.templateCache.file;
    //var options = config.getWiredepDefaultOptions();
    //var specs = config.specs;

    var appJs = ['./wwwroot/scripts/repoExplorer/app.js',
        './wwwroot/scripts/repoExplorer/*.controller.js',
        './wwwroot/scripts/repoExplorer/*.directive.js',
        './wwwroot/scripts/repoExplorer/*.service.js'
    ];
    var nodeModules = 'node_modules';
    var testlibraries = [
            nodeModules + '/mocha/mocha.js',
            nodeModules + '/chai/chai.js',
            nodeModules + '/mocha-clean/index.js',
            nodeModules + '/sinon-chai/lib/sinon-chai.js'
    ];
    //var specHelpers = ['./wwwroot/scripts/test-helpers/*.js'];
    var specs = ['./wwwroot/scripts/repoExplorer/*.spec.js'];

    //if (args.startServers) {
    //    specs = [].concat(specs, config.serverIntegrationSpecs);
    //}
    //options.devDependencies = true;

    var wiredepOptions = config.wiredep.options;
    wiredepOptions.devDependencies = true;

    return gulp
        .src('./wwwroot/scripts/repoExplorer/specs.html')  // config.specRunner)
        .pipe(wiredep(wiredepOptions))
        .pipe($.inject(gulp.src(appJs, { read: false }), { relative: true }))
        .pipe($.inject(gulp.src(testlibraries, { read: false, }), { name: 'testlibraries', relative: true } ))
        //.pipe($.inject(gulp.src(specHelpers, { read: false, }), { name: 'spechelpers' }))
        .pipe($.inject(gulp.src(specs, { read: false, }), { name: 'specs', relative: true }))
        //.pipe(inject(templateCache, 'templates'))
        .pipe(gulp.dest('./wwwroot/scripts/repoExplorer'));
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

/*
 * Methods
 */
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

// Gulp example with Karma directly
function startTests(singleRun, done) {
    log('Karma started');
    var child;
    var excludeFiles = [];
    var fork = require('child_process').fork;
    var Server = require('karma').Server;
    var serverSpecs = config.serverIntegrationSpecs;

    //if (args.startServers) {
    //    log('Starting servers');
    //    var savedEnv = process.env;
    //    savedEnv.NODE_ENV = 'dev';
    //    savedEnv.PORT = 8888;
    //    child = fork(config.nodeServer);
    //} else {
    //    if (serverSpecs && serverSpecs.length) {
    //        excludeFiles = serverSpecs;
    //    }
    //}

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

function serve(isDev, specRunner) {
    // var debug = args.debug || args.debugBrk;
    // var debugMode = args.debug ? '--debug' : args.debugBrk ? '--debug-brk' : '';
    // var debugMode = '--debug';
    var nodeOptions = getNodeOptions(isDev);

    if (isDev) {
        //        runNodeInspector();
        nodeOptions.nodeArgs = ['--debug=5858'];
    }

    //if (args.verbose) {
    //    console.log(nodeOptions);
    //}

    return $.nodemon(nodeOptions)
        .on('restart', /*['vet'],*/ function (ev) {
            log('*** nodemon restarted');
            log('files changed:\n' + ev);
            setTimeout(function () {
                browserSync.notify('reloading now ...');
                browserSync.reload({ stream: false });
            }, 1000 /*config.browserReloadDelay*/);
        })
        .on('start', function () {
            log('*** nodemon started');
            startBrowserSync(isDev, specRunner);
        })
        .on('crash', function () {
            log('*** nodemon crashed: script crashed for some reason');
        })
        .on('exit', function () {
            log('*** nodemon exited cleanly');
        });
}

function getNodeOptions(isDev) {
    return {
        script: './Utils/app.js',  //config.nodeServer,
        delayTime: 1,
        env: {
            'PORT': 8001,           // port,
            'NODE_ENV': 'dev'       //isDev ? 'dev' : 'build'
        }//,
        //watch: [config.server]
    };
}

function startBrowserSync(isDev, specRunner) {
    //if (args.nosync || browserSync.active) {
    //    return;
    //}

    log('Starting BrowserSync on port ' + port);

    // If build: watches the files, builds, and restarts browser-sync.
    // If dev: watches less, compiles it to css, browser-sync handles reload
    //if (isDev) {
    //    gulp.watch([config.less], ['styles'])
    //        .on('change', changeEvent);
    //} else {
    //    gulp.watch([config.less, config.js, config.html], ['browserSyncReload'])
    //        .on('change', changeEvent);
    //}

    var options = {
        proxy: 'localhost:' + port,
        port: 3000,
        //files: isDev ? [
        //    config.client + '**/*.*',
        //    '!' + config.less,
        //    config.temp + '**/*.css'
        //] : [],
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
        reloadDelay: 0 //1000
    };
    if (specRunner) {
        options.startPath = 'specs.html';   //config.specRunnerFile;
    }

    browserSync(options);
}
