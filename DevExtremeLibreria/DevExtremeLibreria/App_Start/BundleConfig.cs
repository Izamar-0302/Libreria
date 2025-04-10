using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace DevExtremeLibreria {

    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            var scriptBundle = new Bundle("~/Scripts/bundle");

            // jQuery (solo si es necesario)
            scriptBundle.Include("~/Scripts/jquery-3.6.3.js");

            // Bootstrap (usando bootstrap.bundle.js que incluye Popper.js)
            scriptBundle.Include("~/Scripts/bootstrap.bundle.min.js"); // Utiliza el bundle que incluye Popper.js

            // Custom site styles
            var styleBundle = new Bundle("~/Content/bundle");
            styleBundle.Include("~/Content/dx.material.custom-scheme.css")
                       .Include("~/Content/bootstrap.css")
                       .Include("~/Content/Site.css");

            // Agregar los bundles a la tabla de bundles
            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
            // BundleTable.EnableOptimizations = true; // Uncomment to enable optimizations
        }
    }
}