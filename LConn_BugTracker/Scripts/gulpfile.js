'use strict'

var gulp = require('gulp');
var browserSync = require('browser-sync').create();
var sass = require('gulp-sass');
var rename = require('gulp-rename');
var del = require('del');
var runSequence = require('run-sequence');
var replace = require('gulp-replace');
var injectPartials = require('gulp-inject-partials');
var inject = require('gulp-inject');
var sourcemaps = require('gulp-sourcemaps');
var concat = require('gulp-concat');
var merge = require('merge-stream');

gulp.paths = {
    dist: 'dist',
};

var paths = gulp.paths;



// Static Server + watching scss/html files
gulp.task('serve', ['sass'], function() {

    browserSync.init({
        port: 3000,
        server: "./",
        ghostMode: false,
        notify: false
    });

    gulp.watch('scss/**/*.scss', ['sass']);
    gulp.watch('**/*.html').on('change', browserSync.reload);
    gulp.watch('js/**/*.js').on('change', browserSync.reload);

});



// Static Server without watching scss files
gulp.task('serve:lite', function() {

    browserSync.init({
        server: "./",
        ghostMode: false,
        notify: false
    });

    gulp.watch('**/*.css').on('change', browserSync.reload);
    gulp.watch('**/*.html').on('change', browserSync.reload);
    gulp.watch('js/**/*.js').on('change', browserSync.reload);

});



gulp.task('sass', function () {
    return gulp.src('./scss/**/style.scss')
        .pipe(sourcemaps.init())
        .pipe(sass({outputStyle: 'expanded'}).on('error', sass.logError))
        .pipe(sourcemaps.write('./maps'))
        .pipe(gulp.dest('./css'))
        .pipe(browserSync.stream());
});



gulp.task('sass:watch', function () {
    gulp.watch('./scss/**/*.scss');
});


/*sequence for injecting partials and replacing paths*/
gulp.task('inject', function() {
  runSequence('injectPartial' , 'injectCommonAssets' , 'injectLayoutStyles', 'replacePath');
});



/* inject partials like sidebar and navbar */
gulp.task('injectPartial', function () {
  return gulp.src("./**/*.html", { base: "./" })
    .pipe(injectPartials())
    .pipe(gulp.dest("."));
});



/* inject Js and CCS assets into HTML */
gulp.task('injectCommonAssets', function () {
  return gulp.src('./**/*.html')
    .pipe(inject(gulp.src([ 
        './vendors/typicons.font/font/typicons.css',
        './vendors/css/vendor.bundle.base.css', 
        './vendors/js/vendor.bundle.base.js',
    ], {read: false}), {name: 'base', relative: true}))
    .pipe(inject(gulp.src([
        './css/*.css', 
        './js/off-canvas.js', 
        './js/hoverable-collapse.js', 
        './js/template.js',
        './js/settings.js', 
        './js/todolist.js'
    ], {read: false}), {relative: true}))
    .pipe(gulp.dest('.'));
});

/* inject Js and CCS assets into HTML */
gulp.task('injectLayoutStyles', function () {
    var verticalLightStream = gulp.src(['./**/vertical-default-light/**/*.html',
            './**/vertical-boxed/**/*.html',
            './**/vertical-compact/**/*.html',
            './**/vertical-dark-sidebar/**/*.html',
            './**/vertical-fixed/**/*.html',
            './**/vertical-hidden-toggle/**/*.html',
            './**/vertical-icon-menu/**/*.html',
            './**/vertical-toggle-overlay/**/*.html',
            './index.html'])
        .pipe(inject(gulp.src([
            './css/vertical-layout-light/style.css', 
        ], {read: false}), {relative: true}))
        .pipe(gulp.dest('.'));
    var horizontalLightStream = gulp.src('./**/horizontal-default-light/**/*.html')
        .pipe(inject(gulp.src([
            './css/horizontal-layout-light/style.css', 
        ], {read: false}), {relative: true}))
        .pipe(gulp.dest('.'));
    var horizontalDarkStream = gulp.src('./**/horizontal-default-dark/**/*.html')
        .pipe(inject(gulp.src([
            './css/horizontal-layout-dark/style.css',
        ], {read: false}), {relative: true}))
        .pipe(gulp.dest('.'));
    var verticalDarkStream = gulp.src('./**/vertical-default-dark/**/*.html')
        .pipe(inject(gulp.src([
            './css/vertical-layout-dark/style.css', 
        ], {read: false}), {relative: true}))
        .pipe(gulp.dest('.'));
    return merge(verticalLightStream, horizontalLightStream, horizontalDarkStream, verticalDarkStream);
});

/*replace image path and linking after injection*/
gulp.task('replacePath', function(){
    gulp.src(['./demo/*/pages/*/*.html'], { base: "./" })
        .pipe(replace('="images/', '="../../../../images/'))
        .pipe(replace('href="pages/', 'href="../../pages/'))
        .pipe(replace('href="index.html"', 'href="../../index.html"'))
        .pipe(gulp.dest('.'));
    gulp.src(['./demo/*/pages/*.html'], { base: "./" })
        .pipe(replace('="images/', '="../../../images/'))
        .pipe(replace('"pages/', '"../pages/'))
        .pipe(replace('href="index.html"', 'href="../index.html"'))
        .pipe(gulp.dest('.'));
    gulp.src(['./demo/*/index.html'], { base: "./" })
        .pipe(replace('="images/', '="../../images/'))
        .pipe(gulp.dest('.'));
});

/*sequence for building vendor scripts and styles*/
gulp.task('bundleVendors', function() {
    runSequence('clean:vendors', 'buildBaseVendorStyles','buildBaseVendorScripts', 'copyAddonsStyles', 'copyAddonsScripts');
});

gulp.task('clean:vendors', function () {
    return del([
      'vendors/**/*'
    ]);
});

/*Building vendor scripts needed for basic template rendering*/
gulp.task('buildBaseVendorScripts', function() {
    return gulp.src([
        './node_modules/jquery/dist/jquery.min.js', 
        './node_modules/popper.js/dist/umd/popper.min.js', 
        './node_modules/bootstrap/dist/js/bootstrap.min.js', 
        './node_modules/perfect-scrollbar/dist/perfect-scrollbar.min.js'
    ])
      .pipe(concat('vendor.bundle.base.js'))
      .pipe(gulp.dest('./vendors/js'));
});

/*Building vendor styles needed for basic template rendering*/
gulp.task('buildBaseVendorStyles', function() {
    return gulp.src(['./node_modules/perfect-scrollbar/css/perfect-scrollbar.css'])
      .pipe(concat('vendor.bundle.base.css'))
      .pipe(gulp.dest('./vendors/css'));
});

/*Scripts for addons*/
gulp.task('copyAddonsScripts', function() {
    gulp.src(['node_modules/chart.js/dist/Chart.min.js'])
        .pipe(gulp.dest('./vendors/chart.js'));
    gulp.src(['node_modules/jquery-bar-rating/dist/jquery.barrating.min.js'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-sparkline/jquery.sparkline.min.js'])
        .pipe(gulp.dest('./vendors/jquery-sparkline')); 
    gulp.src(['node_modules/progressbar.js/dist/progressbar.min.js'])
        .pipe(gulp.dest('./vendors/progressbar.js'));
    gulp.src(['node_modules/moment/moment.js'])
        .pipe(gulp.dest('./vendors/moment'));
    gulp.src(['node_modules/fullcalendar/dist/fullcalendar.min.js'])
        .pipe(gulp.dest('./vendors/fullcalendar'));
    gulp.src(['node_modules/d3/d3.min.js'])
        .pipe(gulp.dest('./vendors/d3'));
    gulp.src(['node_modules/c3/c3.js'])
        .pipe(gulp.dest('./vendors/c3'));
    gulp.src(['node_modules/chartist/dist/chartist.min.js'])
        .pipe(gulp.dest('./vendors/chartist'));
    gulp.src(['node_modules/flot/jquery.flot.js'])
        .pipe(gulp.dest('./vendors/flot'));
    gulp.src(['node_modules/flot/jquery.flot.resize.js'])
        .pipe(gulp.dest('./vendors/flot'));
    gulp.src(['node_modules/flot/jquery.flot.categories.js'])
        .pipe(gulp.dest('./vendors/flot'));
    gulp.src(['node_modules/flot/jquery.flot.fillbetween.js'])
        .pipe(gulp.dest('./vendors/flot'));
    gulp.src(['node_modules/flot/jquery.flot.stack.js'])
        .pipe(gulp.dest('./vendors/flot'));
    gulp.src(['node_modules/flot/jquery.flot.pie.js'])
        .pipe(gulp.dest('./vendors/flot'));
    gulp.src(['node_modules/flot.curvedlines/curvedLines.js'])
        .pipe(gulp.dest('./vendors/flot'));
    gulp.src(['node_modules/justgage/raphael-2.1.4.min.js'])
        .pipe(gulp.dest('./vendors/justgage'));
    gulp.src(['node_modules/justgage/justgage.js'])
        .pipe(gulp.dest('./vendors/justgage'));
    gulp.src(['node_modules/morris.js/morris.min.js'])
        .pipe(gulp.dest('./vendors/morris.js'));
    gulp.src(['node_modules/raphael/raphael.min.js'])
        .pipe(gulp.dest('./vendors/raphael'));
    gulp.src(['node_modules/jquery-tags-input/dist/jquery.tagsinput.min.js'])
        .pipe(gulp.dest('./vendors/jquery-tags-input'));
    gulp.src(['node_modules/progressbar.js/dist/progressbar.min.js'])
        .pipe(gulp.dest('./vendors/progressbar.js'));
    gulp.src(['node_modules/inputmask/dist/jquery.inputmask.bundle.js'])
        .pipe(gulp.dest('./vendors/inputmask'));
    gulp.src(['node_modules/inputmask/dist/inputmask/phone-codes/phone.js'])
        .pipe(gulp.dest('./vendors/inputmask'));
    gulp.src(['node_modules/inputmask/dist/inputmask/phone-codes/phone-be.js'])
        .pipe(gulp.dest('./vendors/inputmask'));    
    gulp.src(['node_modules/inputmask/dist/inputmask/phone-codes/phone-ru.js'])
        .pipe(gulp.dest('./vendors/inputmask'));
    gulp.src(['node_modules/inputmask/dist/inputmask/bindings/inputmask.binding.js'])
        .pipe(gulp.dest('./vendors/inputmask'));
    gulp.src(['node_modules/dropify/dist/js/dropify.min.js'])
        .pipe(gulp.dest('./vendors/dropify'));
    gulp.src(['node_modules/dropzone/dist/dropzone.js'])
        .pipe(gulp.dest('./vendors/dropzone'));
    gulp.src(['node_modules/jquery-file-upload/js/jquery.uploadfile.min.js'])
        .pipe(gulp.dest('./vendors/jquery-file-upload'));
    gulp.src(['node_modules/jquery-asColor/dist/jquery-asColor.min.js'])
        .pipe(gulp.dest('./vendors/jquery-asColor'));
    gulp.src(['node_modules/jquery-asGradient/dist/jquery-asGradient.min.js'])
        .pipe(gulp.dest('./vendors/jquery-asGradient'));
    gulp.src(['node_modules/jquery-asColorPicker/dist/jquery-asColorPicker.min.js'])
        .pipe(gulp.dest('./vendors/jquery-asColorPicker'));
    gulp.src(['node_modules/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js'])
        .pipe(gulp.dest('./vendors/bootstrap-datepicker'));
    gulp.src(['node_modules/moment/min/moment.min.js'])
        .pipe(gulp.dest('./vendors/moment'));
    gulp.src(['node_modules/x-editable/dist/bootstrap3-editable/js/bootstrap-editable.min.js'])
        .pipe(gulp.dest('./vendors/x-editable'));
    gulp.src(['node_modules/tempusdominus-bootstrap-4/build/js/tempusdominus-bootstrap-4.js'])
        .pipe(gulp.dest('./vendors/tempusdominus-bootstrap-4'));
    gulp.src(['node_modules/jquery.repeater/jquery.repeater.min.js'])
        .pipe(gulp.dest('./vendors/jquery.repeater'));
    gulp.src(['node_modules/typeahead.js/dist/typeahead.bundle.min.js'])
        .pipe(gulp.dest('./vendors/typeahead.js'));
    gulp.src(['node_modules/select2/dist/js/select2.min.js'])
        .pipe(gulp.dest('./vendors/select2'));
    gulp.src(['node_modules/codemirror/lib/codemirror.js'])
        .pipe(gulp.dest('./vendors/codemirror'));
    gulp.src(['node_modules/codemirror/mode/javascript/javascript.js'])
        .pipe(gulp.dest('./vendors/codemirror'));
    gulp.src(['node_modules/codemirror/mode/shell/shell.js'])
        .pipe(gulp.dest('./vendors/codemirror'));
    gulp.src(['node_modules/quill/dist/quill.min.js'])
        .pipe(gulp.dest('./vendors/quill'));
    gulp.src(['node_modules/simplemde/dist/simplemde.min.js'])
        .pipe(gulp.dest('./vendors/simplemde'));
    gulp.src(['node_modules/jquery-validation/dist/jquery.validate.min.js'])
        .pipe(gulp.dest('./vendors/jquery-validation'));
    gulp.src(['node_modules/bootstrap-maxlength/bootstrap-maxlength.min.js'])
        .pipe(gulp.dest('./vendors/bootstrap-maxlength'));
    gulp.src(['node_modules/jquery-steps/build/jquery.steps.min.js'])
        .pipe(gulp.dest('./vendors/jquery-steps'));
    gulp.src(['node_modules/jquery-mapael/js/jquery.mapael.min.js'])
        .pipe(gulp.dest('./vendors/jquery-mapael'));   
    gulp.src(['node_modules/jquery-mapael/js/maps/france_departments.min.js'])
        .pipe(gulp.dest('./vendors/jquery-mapael'));
    gulp.src(['node_modules/jquery-mapael/js/maps/world_countries.min.js'])
        .pipe(gulp.dest('./vendors/jquery-mapael'));
    gulp.src(['node_modules/jquery-mapael/js/maps/usa_states.min.js'])
        .pipe(gulp.dest('./vendors/jquery-mapael'));
    gulp.src(['node_modules/jvectormap/jquery-jvectormap.min.js'])
        .pipe(gulp.dest('./vendors/jvectormap'));
    gulp.src(['node_modules/jvectormap/tests/assets/jquery-jvectormap-world-mill-en.js'])
        .pipe(gulp.dest('./vendors/jvectormap'));
    gulp.src(['node_modules/jvectormap/tests/assets/jquery-jvectormap-world-mill-en.js'])
        .pipe(gulp.dest('./vendors/jvectormap'));
    gulp.src(['node_modules/datatables.net/js/jquery.dataTables.js'])
        .pipe(gulp.dest('./vendors/datatables.net'));
    gulp.src(['node_modules/datatables.net-bs4/js/dataTables.bootstrap4.js'])
        .pipe(gulp.dest('./vendors/datatables.net-bs4'));
    gulp.src(['node_modules/datatables.net-bs4/js/dataTables.bootstrap4.js'])
        .pipe(gulp.dest('./vendors/datatables.net-bs4'));
    gulp.src(['node_modules/jsgrid/dist/jsgrid.min.js'])
        .pipe(gulp.dest('./vendors/jsgrid'));
    gulp.src(['node_modules/owl-carousel-2/owl.carousel.min.js'])
        .pipe(gulp.dest('./vendors/owl-carousel-2'));
    gulp.src(['node_modules/clipboard/dist/clipboard.min.js'])
        .pipe(gulp.dest('./vendors/clipboard'));
    gulp.src(['node_modules/colcade/colcade.js'])
        .pipe(gulp.dest('./vendors/colcade'));
    gulp.src(['node_modules/jquery-contextmenu/dist/jquery.contextMenu.min.js'])
        .pipe(gulp.dest('./vendors/jquery-contextmenu'));
    gulp.src(['node_modules/dragula/dist/dragula.min.js'])
        .pipe(gulp.dest('./vendors/dragula'));
    gulp.src(['node_modules/jquery-toast-plugin/dist/jquery.toast.min.js'])
        .pipe(gulp.dest('./vendors/jquery-toast-plugin'));
    gulp.src(['node_modules/twbs-pagination/jquery.twbsPagination.min.js'])
        .pipe(gulp.dest('./vendors/twbs-pagination'));
    gulp.src(['node_modules/sweetalert/dist/sweetalert.min.js'])
        .pipe(gulp.dest('./vendors/sweetalert'));
    gulp.src(['node_modules/jquery.avgrund/jquery.avgrund.min.js'])
        .pipe(gulp.dest('./vendors/jquery.avgrund'));
    gulp.src(['node_modules/nouislider/distribute/nouislider.min.js'])
        .pipe(gulp.dest('./vendors/nouislider')); 
    gulp.src(['node_modules/ion-rangeslider/js/ion.rangeSlider.min.js'])
        .pipe(gulp.dest('./vendors/ion-rangeslider/js'));
    gulp.src(['node_modules/pwstabs/assets/jquery.pwstabs.min.js'])
        .pipe(gulp.dest('./vendors/pwstabs'));
    gulp.src(['./node_modules/summernote/dist/**/*'])
        .pipe(gulp.dest('./vendors/summernote/dist'));
    gulp.src(['./node_modules/tinymce/**/*'])
        .pipe(gulp.dest('./vendors/tinymce'));
    gulp.src(['./node_modules/ace-builds/src-min/**/*'])
        .pipe(gulp.dest('./vendors/ace-builds/src-min'));
    gulp.src(['./node_modules/lightgallery/dist/**/*'])
        .pipe(gulp.dest('./vendors/lightgallery'));  
});


/*Styles for addons*/
gulp.task('copyAddonsStyles', function() {
    gulp.src(['./node_modules/@mdi/font/css/materialdesignicons.min.css'])
        .pipe(gulp.dest('./vendors/mdi/css'));
    gulp.src(['./node_modules/@mdi/font/fonts/*'])
        .pipe(gulp.dest('./vendors/mdi/fonts'));
    gulp.src(['./node_modules/typicons.font/src/font/typicons.css'])
        .pipe(gulp.dest('./vendors/typicons.font/font'));
    gulp.src(['./node_modules/typicons.font/src/font/*'])
        .pipe(gulp.dest('./vendors/typicons.font/font'));
    gulp.src(['./node_modules/font-awesome/css/font-awesome.min.css'])
        .pipe(gulp.dest('./vendors/font-awesome/css'));
    gulp.src(['./node_modules/font-awesome/fonts/*'])
        .pipe(gulp.dest('./vendors/font-awesome/fonts'));
    gulp.src(['./node_modules/flag-icon-css/css/flag-icon.min.css'])
        .pipe(gulp.dest('./vendors/flag-icon-css/css'));
    gulp.src(['./node_modules/flag-icon-css/flags/**/*'])
        .pipe(gulp.dest('./vendors/flag-icon-css/flags'));
    gulp.src(['./node_modules/simple-line-icons/css/simple-line-icons.css'])
        .pipe(gulp.dest('./vendors/simple-line-icons/css'));
    gulp.src(['./node_modules/simple-line-icons/fonts/*'])
        .pipe(gulp.dest('./vendors/simple-line-icons/fonts'));
    gulp.src(['./node_modules/ti-icons/css/themify-icons.css'])
        .pipe(gulp.dest('./vendors/ti-icons/css'));
    gulp.src(['./node_modules/ti-icons/fonts/*'])
        .pipe(gulp.dest('./vendors/ti-icons/fonts'));         
    gulp.src(['node_modules/fullcalendar/dist/fullcalendar.min.css'])
        .pipe(gulp.dest('./vendors/fullcalendar'));
    gulp.src(['node_modules/c3/c3.min.css'])
        .pipe(gulp.dest('./vendors/c3'));
    gulp.src(['node_modules/chartist/dist/chartist.min.css'])
        .pipe(gulp.dest('./vendors/chartist'));
    gulp.src(['node_modules/morris.js/morris.css'])
        .pipe(gulp.dest('./vendors/morris.js'));
    gulp.src(['node_modules/jquery-tags-input/dist/jquery.tagsinput.min.css'])
        .pipe(gulp.dest('./vendors/jquery-tags-input'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/fontawesome-stars.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/bars-1to10.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/bars-horizontal.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/bars-movie.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/bars-pill.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/bars-reversed.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/bars-square.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/bootstrap-stars.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/css-stars.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/dist/themes/fontawesome-stars-o.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/jquery-bar-rating/examples/css/examples.css'])
        .pipe(gulp.dest('./vendors/jquery-bar-rating'));
    gulp.src(['node_modules/dropify/dist/css/dropify.min.css'])
        .pipe(gulp.dest('./vendors/dropify'));
    gulp.src(['node_modules/jquery-file-upload/css/uploadfile.css'])
        .pipe(gulp.dest('./vendors/jquery-file-upload'));
    gulp.src(['node_modules/tempusdominus-bootstrap-4/build/css/tempusdominus-bootstrap-4.min.css'])
        .pipe(gulp.dest('./vendors/tempusdominus-bootstrap-4'));
    gulp.src(['node_modules/jquery-asColorPicker/dist/css/asColorPicker.min.css'])
        .pipe(gulp.dest('./vendors/jquery-asColorPicker/css'));
    gulp.src(['node_modules/jquery-asColorPicker/dist/images/*'])
        .pipe(gulp.dest('./vendors/jquery-asColorPicker/images'));
    gulp.src(['node_modules/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css'])
        .pipe(gulp.dest('./vendors/bootstrap-datepicker'));
    gulp.src(['node_modules/x-editable/dist/bootstrap3-editable/css/bootstrap-editable.css'])
        .pipe(gulp.dest('./vendors/x-editable'));
    gulp.src(['node_modules/select2/dist/css/select2.min.css'])
        .pipe(gulp.dest('./vendors/select2')); 
    gulp.src(['node_modules/select2-bootstrap-theme/dist/select2-bootstrap.min.css'])
        .pipe(gulp.dest('./vendors/select2-bootstrap-theme'));
    gulp.src(['node_modules/codemirror/lib/codemirror.css'])
        .pipe(gulp.dest('./vendors/codemirror'));
    gulp.src(['node_modules/codemirror/theme/ambiance.css'])
        .pipe(gulp.dest('./vendors/codemirror'));
    gulp.src(['node_modules/dropify/dist/css/dropify.min.css'])
        .pipe(gulp.dest('./vendors/dropify'));
    gulp.src(['node_modules/quill/dist/quill.snow.css'])
        .pipe(gulp.dest('./vendors/quill'));
    gulp.src(['node_modules/simplemde/dist/simplemde.min.css'])
        .pipe(gulp.dest('./vendors/simplemde'));
    gulp.src(['node_modules/jvectormap/jquery-jvectormap.css'])
        .pipe(gulp.dest('./vendors/jvectormap')); 
    gulp.src(['node_modules/datatables.net-bs4/css/dataTables.bootstrap4.css'])
        .pipe(gulp.dest('./vendors/datatables.net-bs4'));
    gulp.src(['node_modules/jsgrid/dist/jsgrid.min.css'])
        .pipe(gulp.dest('./vendors/jsgrid'));
    gulp.src(['node_modules/jsgrid/dist/jsgrid-theme.min.css'])
        .pipe(gulp.dest('./vendors/jsgrid'));
    gulp.src(['node_modules/owl-carousel-2/assets/owl.carousel.min.css'])
        .pipe(gulp.dest('./vendors/owl-carousel-2'));
    gulp.src(['node_modules/owl-carousel-2/assets/owl.theme.default.min.css'])
        .pipe(gulp.dest('./vendors/owl-carousel-2'));
    gulp.src(['node_modules/jquery-contextmenu/dist/jquery.contextMenu.min.css'])
        .pipe(gulp.dest('./vendors/jquery-contextmenu'));
    gulp.src(['node_modules/dragula/dist/dragula.min.css'])
        .pipe(gulp.dest('./vendors/dragula')); 
    gulp.src(['node_modules/jquery-toast-plugin/dist/jquery.toast.min.css'])
        .pipe(gulp.dest('./vendors/jquery-toast-plugin'));
    gulp.src(['node_modules/sweetalert2/dist/sweetalert2.min.css'])
        .pipe(gulp.dest('./vendors/sweetalert2'));
    gulp.src(['node_modules/nouislider/distribute/nouislider.min.css'])
        .pipe(gulp.dest('./vendors/nouislider'));
    gulp.src(['node_modules/ion-rangeslider/css/ion.rangeSlider.css'])
        .pipe(gulp.dest('./vendors/ion-rangeslider/css'));
    gulp.src(['node_modules/ion-rangeslider/css/ion.rangeSlider.skinFlat.css'])
        .pipe(gulp.dest('./vendors/ion-rangeslider/css'));
    gulp.src(['node_modules/ion-rangeslider/img/*'])
        .pipe(gulp.dest('./vendors/ion-rangeslider/img'));
    gulp.src(['node_modules/pwstabs/assets/jquery.pwstabs.min.css'])
        .pipe(gulp.dest('./vendors/pwstabs'));
});

gulp.task('default', ['serve']);
