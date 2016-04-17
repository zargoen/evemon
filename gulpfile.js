/// <binding AfterBuild='build' Clean='clean' ProjectOpened='watch' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cleanCss = require("gulp-clean-css"),
    uglify = require("gulp-uglify"),
    sourcemaps = require("gulp-sourcemaps");

var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(sourcemaps.init())
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(sourcemaps.write("."))
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cleanCss())
        .pipe(gulp.dest("."));
});

gulp.task("watch", function () {
    return gulp.watch([paths.js, paths.css], ["build"]);
});

gulp.task("build", ["min:js", "min:css"]);

gulp.task("watch", function () {
    return gulp.watch([paths.js, paths.css], ["clean", "build"]);
});