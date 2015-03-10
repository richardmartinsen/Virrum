var gulp = require('gulp');
var sourcemaps = require('gulp-sourcemaps');
var less = require('gulp-less');
var superMario = require('gulp-plumber');
var concat = require('gulp-concat');
var amdOptimize = require('gulp-amd-optimizer');
var uglify = require('gulp-uglify');
var karma = require('karma');
var expect = require('gulp-expect-file');

gulp.task('default', ['build'], function () {

});

gulp.task('build', ['less', 'mainJS', 'printless', 'copyContent'], function () {

});

gulp.task('test', function () {
    return karma.server.start({
        configFile: __dirname + '/karma.conf.js',
        autoWatch: true
    });
});

gulp.task('watch', ['less'], function () {
    gulp.watch('projects/Virrum.Web/Content/less/**/*.less', ['less', 'printless']);
});

gulp.task('less', function () {
    return gulp.src([
            'projects/Virrum.Web/Content/less/Virrum-bootstrap-theme.less'
    ],
        {
            base: 'projects/Virrum.Web/Content/less'
        })
        .pipe(superMario())
        .pipe(sourcemaps.init())
        .pipe(less({
            paths: ['projects/Virrum.Web/Content/less'],
            sourceMapRootpath: '',
            compress: true
        }))
        .pipe(concat('main.css'))
        .pipe(sourcemaps.write('./', { includeContent: false, sourceRoot: '/Content/less' }))
        .pipe(gulp.dest('projects/Virrum.Web/Content/BuiltFiles'))
		.pipe(expect(['projects/Virrum.Web/Content/BuiltFiles/main.css',
					  'projects/Virrum.Web/Content/BuiltFiles/main.css.map']));
});


gulp.task('printless', function () {
    return gulp.src([
            'projects/Virrum.Web/Content/less/Pages/Shared/print.less'
    ],
        {
            base: 'projects/Virrum.Web/Content/less'
        })
        .pipe(superMario())
        .pipe(less({
            paths: ['projects/Virrum.Web/Content/less'],
            sourceMapRootpath: '',
            compress: true
        }))
        .pipe(concat('print.css'))
        .pipe(gulp.dest('projects/Virrum.Web/Content/BuiltFiles'))
		.pipe(expect(['projects/Virrum.Web/Content/BuiltFiles/print.css']));
});

gulp.task('mainJS', ['decoJS', 'copyJS'], function () {
    return gulp.src([
            'projects/Virrum.Web/bower_components/jquery/dist/jquery.js',
            'projects/Virrum.Web/bower_components/bootstrap/dist/js/bootstrap.js',
            'projects/Virrum.Web/bower_components/bootstrap-datepicker-n9/js/bootstrap-datepicker.js',
            'projects/Virrum.Web/bower_components/bootstrap-datepicker-n9/js/locales/bootstrap-datepicker.no.js',
            'projects/Virrum.Web/bower_components/requirejs/require.js',
            'projects/Virrum.Web/Scripts/paths.js',
            'projects/Virrum.Web/Scripts/BuiltFiles/deco.js',
            'projects/Virrum.Web/Scripts/main.js'
    ], {
        base: 'projects/Virrum.Web'
    })
	.pipe(expect(['projects/Virrum.Web/bower_components/jquery/dist/jquery.js'
				 , 'projects/Virrum.Web/bower_components/bootstrap/dist/js/bootstrap.js'
				 , 'projects/Virrum.Web/bower_components/bootstrap-datepicker-n9/js/bootstrap-datepicker.js'
				 , 'projects/Virrum.Web/bower_components/bootstrap-datepicker-n9/js/locales/bootstrap-datepicker.no.js'
				 , 'projects/Virrum.Web/bower_components/requirejs/require.js'
				 , 'projects/Virrum.Web/Scripts/paths.js'
				 , 'projects/Virrum.Web/Scripts/BuiltFiles/deco.js'
				 , 'projects/Virrum.Web/Scripts/main.js']))
	.pipe(sourcemaps.init())
        .pipe(concat('main.js'))
        .pipe(uglify())
        .pipe(sourcemaps.write('./', { includeContent: false, sourceRoot: '/' }))
        .pipe(gulp.dest('projects/Virrum.Web/Scripts/BuiltFiles'))
		.pipe(expect(['projects/Virrum.Web/Scripts/BuiltFiles/main.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/main.js.map']));
});

gulp.task('copyContent', function () {
    gulp.src(['projects/Virrum.Web/bower_components/bootstrap/fonts/glyphicons-halflings-regular.ttf',
			 'projects/Virrum.Web/bower_components/bootstrap/fonts/glyphicons-halflings-regular.woff',
			 'projects/Virrum.Web/bower_components/bootstrap/fonts/glyphicons-halflings-regular.woff2'])
        .pipe(gulp.dest('projects/Virrum.Web/Content/BuiltFiles'))
		.pipe(expect(['projects/Virrum.Web/Content/BuiltFiles/glyphicons-halflings-regular.ttf',
			 'projects/Virrum.Web/Content/BuiltFiles/glyphicons-halflings-regular.woff',
		    'projects/Virrum.Web/Content/BuiltFiles/glyphicons-halflings-regular.woff2']));
});

gulp.task('copyJS', function () {
    gulp.src(['projects/Virrum.Web/bower_components/es6-promise/promise.js',
			  'projects/Virrum.Web/bower_components/moment/moment.js',
			  'projects/Virrum.Web/bower_components/requirejs/require.js',
			  'projects/Virrum.Web/bower_components/moment/locale/nb.js',
			  'projects/Virrum.Web/bower_components/lodash/dist/lodash.js',
			  'projects/Virrum.Web/bower_components/when/when.js',
			  'projects/Virrum.Web/bower_components/knockout/dist/knockout.js',
			  'projects/Virrum.Web/bower_components/bootstrap/js/affix.js'])
        .pipe(gulp.dest('projects/Virrum.Web/Scripts/BuiltFiles'))
		.pipe(expect(['projects/Virrum.Web/Scripts/BuiltFiles/promise.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/moment.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/require.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/nb.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/lodash.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/when.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/knockout.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/affix.js']));
});

gulp.task('copyMinJS', function () {
    gulp.src(['projects/Virrum.Web/bower_components/es6-promise/promise.min.js',
			  'projects/Virrum.Web/bower_components/moment/min/moment.min.js',
			  'projects/Virrum.Web/bower_components/requirejs/require.js',
			  'projects/Virrum.Web/bower_components/moment/locale/nb.js',
			  'projects/Virrum.Web/bower_components/lodash/dist/lodash.min.js',
			  'projects/Virrum.Web/bower_components/when/when.js',
			  'projects/Virrum.Web/bower_components/knockout/dist/knockout.js',
			  'projects/Virrum.Web/bower_components/knockout/dist/knockout.js',
			  'projects/Virrum.Web/bower_components/bootstrap/js/affix.js'])
        .pipe(gulp.dest('projects/Virrum.Web/Scripts/BuiltFiles'))
		.pipe(expect(['projects/Virrum.Web/Scripts/BuiltFiles/promise.min.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/moment.min.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/require.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/nb.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/lodash.min.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/when.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/knockout.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/affix.js']));
});

gulp.task('decoJS', function () {
    return gulp.src('projects/Virrum.Web/bower_components/deco/Dist/deco.js', { base: requireConfig.baseUrl })
        .pipe(superMario())
        .pipe(sourcemaps.init())
        .pipe(amdOptimize(requireConfig, { umd: true }))
        .pipe(concat('deco.js'))
        .pipe(sourcemaps.write('./', { includeContent: false, sourceRoot: '/bower_components' }))
        .pipe(gulp.dest('projects/Virrum.Web/Scripts/BuiltFiles'))
		.pipe(expect(['projects/Virrum.Web/Scripts/BuiltFiles/deco.js',
					  'projects/Virrum.Web/Scripts/BuiltFiles/deco.js.map']));
});

var requireConfig = {
    baseUrl: 'projects/Virrum.Web/bower_components',
    paths: {
        'knockout': 'knockout/dist/knockout.debug',
        "require": "requirejs/require"
    },
    packages: [
        { name: 'when', location: 'when', main: 'when' }
    ],
    exclude: [
        'exports'
    ]
};