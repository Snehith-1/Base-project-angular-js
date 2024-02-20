var args = require('yargs').argv,
    path = require('path'),
    flip = require('css-flip'),
    through = require('through2'),
    gulp = require('gulp'),
    $ = require('gulp-load-plugins')(),
    gulpsync = $.sync(gulp),
    PluginError = $.util.PluginError,
    del = require('del');

// production mode (see build task)
var isProduction = false;
// styles sourcemaps
var useSourceMaps = false;

// Switch to sass mode. 
// Example:z
//    gulp --usesass
var useSass = args.usesass;

// Angular template cache
// Example:
//    gulp --usecache
var useCache = args.usecache;

// ignore everything that begins with underscore
var hidden_files = '**/_*.*';
var ignored_files = '!' + hidden_files;

// MAIN PATHS
var paths = {
    app: '../app/',
    markup: 'jade/',
    styles: 'less/',
    scripts: 'js/'
}

// if sass -> switch to sass folder
if (useSass) {
    log('Using SASS stylesheets...');
    paths.styles = 'sass/';
}


// VENDOR CONFIG
var vendor = {
    // vendor scripts required to start the app
    base: {
        source: require('./vendor.base.json'),
        dest: '../app/js',
        name: 'base.js'
    },
    // vendor scripts to make the app work. Usually via lazy loading
    app: {
        source: require('./vendor.json'),
        dest: '../vendor'
    }
};


// SOURCES CONFIG 
var source = {
    scripts: [paths.scripts + 'app.module.js',
    // template modules
    paths.scripts + 'modules/**/*.module.js',
        paths.scripts + 'modules/**/*.js',
        paths.scripts + 'modules/**/*.run.js'

        // custom modules
        // paths.scripts + 'custom/**/*.module.js',
        //  paths.scripts + 'custom/**/*.js'
    ]
};

var sourceits = {
    scripts: [paths.scripts + 'custom/ems.its/**/*.js']
};
var sourcelgl = {
    scripts: [paths.scripts + 'custom/ems.lgl/**/*.js']
};
var sourceecms = {
    scripts: [paths.scripts + 'custom/ems.ecms/**/*.js']
};

var sourceasset = {
    scripts: [paths.scripts + 'custom/ems.asset/**/*.js']
};

var sourcehrm = {
    scripts: [paths.scripts + 'custom/ems.hrm/**/*.js']
};

var sourcersk = {
    scripts: [paths.scripts + 'custom/ems.rsk/**/*.js']
};

var sourcemastersamagro = {
    scripts: [paths.scripts + 'custom/ems.mastersamagro/**/*.js']
};

var sourceidas = {
    scripts: [paths.scripts + 'custom/ems.idas/**/*.js']
};

var sourceiasn = {
    scripts: [paths.scripts + 'custom/ems.iasn/**/*.js']
};

var sourcelrn = {
    scripts: [paths.scripts + 'custom/ems.lrn/**/*.js']
};

var sourcemaster = {
    scripts: [paths.scripts + 'custom/ems.master/**/*.js']
};

var sourcesystem = {
    scripts: [paths.scripts + 'custom/ems.system/**/*.js']
};

var sourceosd = {
    scripts: [paths.scripts + 'custom/ems.osd/**/*.js']
};

var sourcemarketing = {
    scripts: [paths.scripts + 'custom/ems.marketing/**/*.js']
};
var sourcesdc = {
    scripts: [paths.scripts + 'custom/ems.sdc/**/*.js']
};
var sourceaudit = {
    scripts: [paths.scripts + 'custom/ems.audit/**/*.js']
};
var sourcefoundation = {
    scripts: [paths.scripts + 'custom/ems.foundation/**/*.js']
};
var sourcebusinessteam = {
    scripts: [paths.scripts + 'custom/ems.businessteam/**/*.js']
};
var sourcemis = {
    scripts: [paths.scripts + 'custom/ems.mis/**/*.js']
};
var sourcehrloan = {
    scripts: [paths.scripts + 'custom/ems.hrloan/**/*.js']
};
var sourcebrs = {
    scripts: [paths.scripts + 'custom/ems.brs/**/*.js']
};
    // BUILD TARGET CONFIG 
var build = {
    scripts: paths.app + 'js'
};

// PLUGINS OPTIONS

var prettifyOpts = {
    indent_char: ' ',
    indent_size: 3,
    unformatted: ['a', 'sub', 'sup', 'b', 'i', 'u', 'pre', 'code']
};

var vendorUglifyOpts = {
    mangle: {
        except: ['$super'] // rickshaw requires this
    }
};

var compassOpts = {
    project: path.join(__dirname, '../'),
    css: 'app/css',
    sass: 'master/sass/',
    image: 'app/img'
};

var compassOptsThemes = {
    project: path.join(__dirname, '../'),
    css: 'app/css',
    sass: 'master/sass/themes/', // themes in a subfolders
    image: 'app/img'
};

var tplCacheOptions = {
    root: 'app',
    filename: 'templates.js',
    //standalone: true,
    module: 'app.core',
    base: function (file) {
        return file.path.split('jade')[1];
    }
};

var injectOptions = {
    name: 'templates',
    transform: function (filepath) {
        return 'script(src=\'' +
            filepath.substr(filepath.indexOf('app')) +
            '\')';
    }
}

//---------------
// TASKS
//---------------


// JS APP
gulp.task('scripts:app', function () {
    log('Building scripts..');
    // Minify and copy all JavaScript (except vendor scripts)
    return gulp.src(source.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('app.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptits:appits', function () {
    log('Building ITS Scripts..');
    return gulp.src(sourceits.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appits.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptlgl:applgl', function () {
    log('Building LGL Scripts..');
    return gulp.src(sourcelgl.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('applgl.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});
gulp.task('scriptecms:appecms', function () {
    log('Building ECMS Scripts..');
    return gulp.src(sourceecms.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appecms.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptsystem:appsystem', function () {
    log('Building System Scripts..');
    return gulp.src(sourcesystem.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appsystem.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptasset:appasset', function () {
    log('Building Asset Scripts..');
    return gulp.src(sourceasset.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appasset.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scripthrm:apphrm', function () {
    log('Building HR Scripts..');
    return gulp.src(sourcehrm.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('apphrm.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptrsk:apprsk', function () {
    log('Building RSK Scripts..');
    return gulp.src(sourcersk.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('apprsk.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptmastersamagro:appmastersamagro', function () {
    log('Building Master SAMAGRO Scripts..');
    return gulp.src(sourcemastersamagro.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appmastersamagro.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});


gulp.task('scriptidas:appidas', function () {
    log('Building IDAS Scripts..');
    return gulp.src(sourceidas.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appidas.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptiasn:appiasn', function () {
    log('Building IASN Scripts..');
    return gulp.src(sourceiasn.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appiasn.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptlrn:applrn', function () {
    log('Building LRN Scripts..');
    return gulp.src(sourcelrn.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('applrn.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptmaster:appmaster', function () {
    log('Building MASTER Scripts..');
    return gulp.src(sourcemaster.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appmaster.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptosd:apposd', function () {
    log('Building OSD Scripts..');
    return gulp.src(sourceosd.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('apposd.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptmarketing:appmarketing', function () {
    log('Building MARKETING Scripts..');
    return gulp.src(sourcemarketing.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appmarketing.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptsdc:appsdc', function () {
    log('Building SDC Scripts..');
    return gulp.src(sourcesdc.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appsdc.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptaudit:appaudit', function () {
    log('Building AUDIT Scripts..');
    return gulp.src(sourceaudit.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appaudit.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});

gulp.task('scriptbrs:appbrs', function () {
    log('Building BRS Scripts..');
    return gulp.src(sourcebrs.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appbrs.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});
gulp.task('scripthrloan:apphrloan', function () {
    log('Building HRLOAN Scripts..');
    return gulp.src(sourcehrloan.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('apphrloan.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});
gulp.task('scriptfoundation:appfoundation', function () {
    log('Building FOUNDATION Scripts..');
    return gulp.src(sourcefoundation.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appfoundation.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});
gulp.task('scriptbusinessteam:appbusinessteam', function () {
    log('Building BUSINESSTEAM Scripts..');
    return gulp.src(sourcebusinessteam.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appbusinessteam.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});
gulp.task('scriptmis:appmis', function () {
    log('Building POWERBI MIS Scripts..');
    return gulp.src(sourcemis.scripts)
        .pipe($.jsvalidate())
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
        .pipe($.concat('appmis.js'))
        .pipe($.ngAnnotate())
        .on('error', handleError)
        .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
        .on('error', handleError)
        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
        .pipe(gulp.dest(build.scripts));
});
// VENDOR BUILD
gulp.task('vendor', gulpsync.sync(['vendor:base', 'vendor:app']));

// Build the base script to start the application from vendor assets
gulp.task('vendor:base', function () {
    log('Copying base vendor assets..');
    return gulp.src(vendor.base.source)
        .pipe($.expectFile(vendor.base.source))
        .pipe($.if(isProduction, $.uglify()))
        .pipe($.concat(vendor.base.name))
        .pipe(gulp.dest(vendor.base.dest))
    ;
});

// copy file from bower folder into the app vendor folder
gulp.task('vendor:app', function () {
    log('Copying vendor assets..');

    var jsFilter = $.filter('**/*.js');
    var cssFilter = $.filter('**/*.css');

    return gulp.src(vendor.app.source, { base: 'bower_components' })
        .pipe($.expectFile(vendor.app.source))
        .pipe(jsFilter)
        .pipe($.if(isProduction, $.uglify(vendorUglifyOpts)))
        .pipe(jsFilter.restore())
        .pipe(cssFilter)
        .pipe($.if(isProduction, $.minifyCss()))
        .pipe(cssFilter.restore())
        .pipe(gulp.dest(vendor.app.dest));

});

// APP LESS
//gulp.task('styles:app', function () {
//    log('Building application styles..');
//    return gulp.src(source.styles.app)
//        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
//        .pipe(useSass ? $.compass(compassOpts) : $.less())
//        .on('error', handleError)
//        .pipe($.if(isProduction, $.minifyCss()))
//        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
//        .pipe(gulp.dest(build.styles));
//});

// APP RTL
//gulp.task('styles:app:rtl', function () {
//    log('Building application RTL styles..');
//    return gulp.src(source.styles.app)
//        .pipe($.if(useSourceMaps, $.sourcemaps.init()))
//        .pipe(useSass ? $.compass(compassOpts) : $.less())
//        .on('error', handleError)
//        .pipe(flipcss())
//        .pipe($.if(isProduction, $.minifyCss()))
//        .pipe($.if(useSourceMaps, $.sourcemaps.write()))
//        .pipe($.rename(function (path) {
//            path.basename += "-rtl";
//            return path;
//        }))
//        .pipe(gulp.dest(build.styles));
//});

// LESS THEMES
//gulp.task('styles:themes', function () {
//    log('Building application theme styles..');
//    return gulp.src(source.styles.themes)
//        .pipe(useSass ? $.compass(compassOptsThemes) : $.less())
//        .on('error', handleError)
//        .pipe(gulp.dest(build.styles));
//});

// JADE
//gulp.task('templates:index', ['templates:views'], function () {
//    log('Building index..');

//    var tplscript = gulp.src(build.templates.cache, { read: false });
//    return gulp.src(source.templates.index)
//        .pipe($.if(useCache, $.inject(tplscript, injectOptions))) // inject the templates.js into index
//        .pipe($.jade())
//        .on('error', handleError)
//        .pipe($.htmlPrettify(prettifyOpts))
//        .pipe(gulp.dest(build.templates.index))
//        ;
//});

// JADE
//gulp.task('templates:views', function () {
//    log('Building views.. ' + (useCache ? 'using cache' : ''));

//    if (useCache) {

//        return gulp.src(source.templates.views)
//            .pipe($.jade())
//            .on('error', handleError)
//            .pipe($.angularTemplatecache(tplCacheOptions))
//            .pipe($.if(isProduction, $.uglify({ preserveComments: 'some' })))
//            .pipe(gulp.dest(build.scripts));
//        ;
//    }
//    else {

//        return gulp.src(source.templates.views)
//            .pipe($.if(!isProduction, $.changed(build.templates.views, { extension: '.html' })))
//            .pipe($.jade())
//            .on('error', handleError)
//            .pipe($.htmlPrettify(prettifyOpts))
//            .pipe(gulp.dest(build.templates.views))
//            ;
//    }
//});

//---------------
// WATCH
//---------------

// Rerun the task when a file changes
gulp.task('watch', function () {
    log('Starting watch and LiveReload..');

    $.livereload.listen();

    gulp.watch(source.scripts, ['scripts:app']);
    gulp.watch(sourceits.scripts, ['scriptits:appits']);
    gulp.watch(sourcelgl.scripts, ['scriptlgl:applgl']);
    gulp.watch(sourceecms.scripts, ['scriptecms:appecms']);
    gulp.watch(sourceasset.scripts, ['scriptasset:appasset']);
    gulp.watch(sourcehrm.scripts, ['scripthrm:apphrm']);
    gulp.watch(sourcersk.scripts, ['scriptrsk:apprsk']);
    gulp.watch(sourcemastersamagro.scripts, ['scriptmastersamagro:appmastersamagro']);
    gulp.watch(sourceidas.scripts, ['scriptidas:appidas']);
    gulp.watch(sourceiasn.scripts, ['scriptiasn:appiasn']);
    gulp.watch(sourcelrn.scripts, ['scriptlrn:applrn']);
    gulp.watch(sourcesystem.scripts, ['scriptsystem:appsystem']);
    gulp.watch(sourcemaster.scripts, ['scriptmaster:appmaster']);
    gulp.watch(sourceosd.scripts, ['scriptosd:apposd']);
    gulp.watch(sourcemarketing.scripts, ['scriptmarketing:appmarketing']);
    gulp.watch(sourcesdc.scripts, ['scriptsdc:appsdc']);
    gulp.watch(sourceaudit.scripts, ['scriptaudit:appaudit']);
    gulp.watch(sourcebrs.scripts, ['scriptbrs:appbrs']);
    gulp.watch(sourcehrloan.scripts, ['scripthrloan:apphrloan']);
    gulp.watch(sourcefoundation.scripts, ['scriptfoundation:appfoundation']);
    gulp.watch(sourcebusinessteam.scripts, ['scriptbusinessteam:appbusinessteam']);
    gulp.watch(sourcemis.scripts, ['scriptmis:appmis']);

    // a delay before triggering browser reload to ensure everything is compiled
    var livereloadDelay = 1500;
    // list of source file to watch for live reload
    var watchSource = [].concat(
        source.scripts,
        sourceits.scripts,
        sourcelgl.scripts,
        sourceecms.scripts,
        sourceasset.scripts,
        sourcesystem.scripts,
        sourcehrm.scripts,
        sourcersk.scripts,
        sourcemastersamagro.scripts,
        sourceidas.scripts,
        sourceiasn.scripts,
        sourceosd.scripts,
        sourcelrn.scripts,
        sourcemarketing.scripts,
        sourcesdc.scripts,
        sourceaudit.scripts,
        sourcebrs.scripts,
        sourcehrloan.scripts,
        sourcefoundation.scripts,
        sourcebusinessteam.scripts,
        sourcemis.scripts

    );

    gulp
        .watch(watchSource)
        .on('change', function (event) {
            setTimeout(function () {
                $.livereload.changed(event.path);
            }, livereloadDelay);
        });

});

// lint javascript
gulp.task('lint', function () {
    return gulp
        .src(source.scripts)
        .pipe($.jshint())
        .pipe($.jshint.reporter('jshint-stylish', { verbose: true }))
        .pipe($.jshint.reporter('fail'));
});

// Remove all files from the build paths
gulp.task('clean', function (done) {
    var delconfig = [].concat(
        build.scripts,
        vendor.app.dest
    );

    log('Cleaning: ' + $.util.colors.blue(delconfig));
    // force: clean files outside current directory
    del(delconfig, { force: true }, done);
});

//---------------
// MAIN TASKS
//---------------

// build for production (minify)
gulp.task('build', gulpsync.sync([
    'prod',
    'vendor',
    'assets'
]));

gulp.task('prod', function () {
    log('Starting production build...');
    isProduction = true;
});

// build with sourcemaps (no minify)
gulp.task('sourcemaps', ['usesources', 'default']);
gulp.task('usesources', function () { useSourceMaps = true; });

// default (no minify)
gulp.task('default', gulpsync.sync([
    'vendor',
    'assets',
    'watch'
]), function () {

    log('************');
    log('* All Done * You can start editing your code, LiveReload will update your browser after any change..');
    log('************');

});

gulp.task('assets', [
    'scripts:app',
    'scriptits:appits',
    'scriptlgl:applgl',
    'scriptecms:appecms',
    'scriptasset:appasset',
    'scripthrm:apphrm',
     'scriptrsk:apprsk',
     'scriptmastersamagro:appmastersamagro',
     'scriptidas:appidas',
     'scriptiasn:appiasn',
     'scriptlrn:applrn',
     'scriptsystem:appsystem',
     'scriptmaster:appmaster',
     'scriptosd:apposd',
     'scriptmarketing:appmarketing',
     'scriptsdc:appsdc',
     'scriptaudit:appaudit',
     'scriptbrs:appbrs',
     'scripthrloan:apphrloan',
     'scriptfoundation:appfoundation',
     'scriptbusinessteam:appbusinessteam',
     'scriptmis:appmis',

]);


/////////////////////


// Error handler
function handleError(err) {
    log(err.toString());
    this.emit('end');
}

// Mini gulp plugin to flip css (rtl)
function flipcss(opt) {

    if (!opt) opt = {};

    // creating a stream through which each file will pass
    var stream = through.obj(function (file, enc, cb) {
        if (file.isNull()) return cb(null, file);

        if (file.isStream()) {
            // Todo: isStream!
        }

        var flippedCss = flip(String(file.contents), opt);
        file.contents = new Buffer(flippedCss);
        cb(null, file);
    });

    // returning the file stream
    return stream;
}

// log to console using 
function log(msg) {
    $.util.log($.util.colors.blue(msg));
}
