using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace DevExtremeLibreria
{

    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            // Script Bundle
            var scriptBundle = new Bundle("~/Scripts/bundle");

            // jQuery (solo si es necesario)
            scriptBundle.Include("~/Scripts/jquery-3.6.3.js");

            // Bootstrap (usando bootstrap.bundle.js que incluye Popper.js)
            scriptBundle.Include("~/Scripts/bootstrap.bundle.min.js")
                        .Include("~/Scripts/DevExtreme/js/dx.all.js");

            // Incluir el archivo site.js
            scriptBundle.Include("~/Scripts/site.js");

            // Style Bundle con ambos temas (claro y oscuro)
            var styleBundle = new Bundle("~/Content/bundle");
            styleBundle.Include("~/Content/dx.material.lime.light.css")  // Tema claro
                        .Include("~/Content/bootstrap.css")
                        .Include("~/Content/Site.css");

            // Agregar los bundles a la tabla de bundles
            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

            // Activar optimización si es necesario
            // BundleTable.EnableOptimizations = true; // Descomentar para habilitar optimización
        }
    }
}