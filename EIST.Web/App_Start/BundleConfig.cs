using System.Web;
using System.Web.Optimization;

namespace EIST.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/Site.css"));

            // Font Awesome //
            bundles.Add(new StyleBundle("~/Plugins/font-awesome").Include(
                      //"~/Plugins/font-awesome.min.css",
                      "~/Plugins/global_assets/css/icons/fontawesome/styles.min.css",
                      "~/Plugins/global_assets/css/icons/icomoon/styles.min.css",
                      "~/Plugins/global_assets/css/icons/material/styles.min.css"));

            //--------------------CSS--------------------

            // Theme Global CSS---
            bundles.Add(new StyleBundle("~/Plugins/theme-global").Include(
                      "~/Plugins/global_assets/css/icons/icomoon/styles.min.css",
                      "~/Plugins/assets/css/bootstrap.min.css",
                      "~/Plugins/assets/css/bootstrap_limitless.min.css",
                      "~/Plugins/assets/css/layout.min.css",
                      "~/Plugins/assets/css/components.min.css",
                      "~/Plugins/assets/css/colors.min.css"));

            // Custom CSS---
            bundles.Add(new StyleBundle("~/Plugins/site").Include(
                      "~/Plugins/site.css"));


            //------------------Scripts------------------

            // Theme Core Scripts---
            bundles.Add(new ScriptBundle("~/bundles/theme-core").Include(
                      "~/Plugins/global_assets/js/main/jquery.min.js",
                      "~/Plugins/global_assets/js/main/bootstrap.bundle.min.js",
                      "~/Plugins/global_assets/js/Plugins/loaders/blockui.min.js",
                      "~/Plugins/global_assets/js/Plugins/ui/slinky.min.js"));

            // Theme App Script---
            bundles.Add(new ScriptBundle("~/bundles/theme-app").Include(
                      "~/Plugins/assets/js/app.js"));

            // Dashboard Script---
            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                      "~/Plugins/global_assets/js/demo_pages/dashboard.js"));

            // Select2 Script---form_multiselect
            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                "~/Plugins/global_assets/js/plugins/forms/selects/select2.min.js",
                      "~/Plugins/global_assets/js/demo_pages/form_select2.js"));

            // Login Script---
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Plugins/global_assets/js/demo_pages/login.js"));

            // Uniform Script---
            bundles.Add(new ScriptBundle("~/bundles/uniform").Include(
                      "~/Plugins/global_assets/js/Plugins/forms/styling/uniform.min.js"));

            // Sticky Script---
            //bundles.Add(new ScriptBundle("~/bundles/sticky").Include(
            //          "~/Plugins/global_assets/js/Plugins/ui/sticky.min.js"));

            // NavBar Sticky Script---
            bundles.Add(new ScriptBundle("~/bundles/navbar-sticky").Include(
                      "~/Plugins/global_assets/js/Plugins/ui/sticky.min.js",
                      "~/Plugins/global_assets/js/demo_pages/navbar_multiple_sticky.js"));

            // Bootstrap Date Time Scripts---
            bundles.Add(new ScriptBundle("~/bundles/daterange-picker").Include(
                "~/Plugins/global_assets/js/Plugins/ui/moment/moment.min.js",
                "~/Plugins/global_assets/js/Plugins/pickers/daterangepicker.js",
                "~/Plugins/global_assets/js/Plugins/pickers/pickadate/picker.js",
                "~/Plugins/global_assets/js/Plugins/pickers/pickadate/picker.date.js",
                "~/Plugins/global_assets/js/Plugins/pickers/pickadate/picker.time.js",
                "~/Scripts/custom-date-format.js"));

            // Bootstrap File Scripts---
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-file").Include(
                      "~/Plugins/global_assets/js/Plugins/uploaders/fileinput/fileinput.min.js",
                      "~/Plugins/global_assets/js/demo_pages/uploader_bootstrap.js"));

            // Sweet Alert Scripts---
            bundles.Add(new ScriptBundle("~/bundles/sweet-alert").Include(
                "~/Plugins/global_assets/js/Plugins/notifications/sweet_alert.min.js",
                "~/Plugins/global_assets/js/demo_pages/extra_sweetalert.js"));

            // ckeditor //
            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                "~/Plugins/global_assets/js/plugins/editors/ckeditor/ckeditor.js",
                "~/Plugins/global_assets/js/demo_pages/editor_ckeditor.js"));

            // Ajax Call Script---
            bundles.Add(new ScriptBundle("~/bundles/ajax-call").Include(
                "~/Scripts/ajax-call.js"));

            // Common Script---
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                      "~/Scripts/common.js"));
          
            // Theme Checkbox Radio //
            bundles.Add(new ScriptBundle("~/bundles/theme-check-radio").Include(
                "~/Plugins/global_assets/js/demo_pages/form_checkboxes_radios.js"));
        }
    }
}
