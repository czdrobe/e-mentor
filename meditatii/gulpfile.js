var gulp = require('gulp');
var typescript = require('typescript');
var tsc = require('gulp-typescript');
var embedTemplates = require('gulp-angular-embed-templates');
var sourcemaps     = require('gulp-sourcemaps');

var systemjsBuilder = require('systemjs-builder');

gulp.task('inline-templates', function () {
    return gulp.src(['app/**/*.ts', 'typings/index.d.ts'])
    //.pipe(embedTemplates())
    //.pipe(sourcemaps.init())
    .pipe(tsc({
      "target": "es5",
      "module": "commonjs",
      "moduleResolution": "node",
      "sourceMap": true,
      "emitDecoratorMetadata": true,
      "experimentalDecorators": true,
      "removeComments": true,
      "noImplicitAny": true,
      "suppressImplicitAnyIndexErrors": true,
      "lib": [ "es2015", "dom" ],
    }))
    .js.pipe(gulp.dest('dist'));
    
    });
    
    gulp.task('bundle-app', gulp.series('inline-templates', function() {
    
        var builder = new systemjsBuilder('', 'systemjs.config.js');
        return builder.buildStatic('app', 'dist/js/app.min.js', {
            minify: true,
            mangle: true
        });
    }));


    
    
    gulp.task('production', gulp.series('bundle-app', function(){}));

    //USE BUNDLE-APP
    //TO DO find a way to include the html template for angular components