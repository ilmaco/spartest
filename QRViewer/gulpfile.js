var gulp = require('gulp');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
var tsify = require('tsify');
var uglify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var buffer = require('vinyl-buffer');
var del = require('del');
var inject = require('gulp-inject');
var exec = require('child_process').exec;

var paths = {
    pages: ['wwwroot/*.html']
};

gulp.task('clean', function () {
    return del(['dist/**/*']);
});

gulp.task('copy-html', function () {
    return gulp.src(paths.pages)
        .pipe(gulp.dest('dist'));
});

gulp.task('inject', function () {
    var target = gulp.src('dist/index.html');
    var sources = gulp.src(['dist/bundle.js'], { read: false });

    return target.pipe(inject(sources))
        .pipe(gulp.dest('dist'));
});

gulp.task('default', gulp.series(gulp.parallel('copy-html'), function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['wwwroot/scripts/src.ts','wwwroot/scripts/dataUrl.ts'],
        cache: {},
        packageCache: {}
    })
        .plugin(tsify)
        .bundle()
        .pipe(source('bundle.js'))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(uglify())
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest('dist'));
}), "inject");


gulp.task('run-test', function (cb) {
    exec('npm run test', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})