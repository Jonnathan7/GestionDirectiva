using System.Web.Optimization;
using System.Web;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Configuration;

namespace GDirectiva.Presentacion.Core.Helpers
{
    /// <summary>
    /// Helpers personalizados
    /// </summary>
    /// <remarks>
    /// Creación:   GMD 22122014 <br />
    /// Modificación:            <br />
    /// </remarks>
    public static class CustomHtmlHelper
    {
        /// <summary>
        /// Renderiza los archivos js optimizados segun la vista actual
        /// </summary>
        /// <param name="scriptView">Vista del script</param>
        /// <returns>Indicador</returns>
        public static IHtmlString RenderViewJs(string scriptView = null)
        {
            if (string.IsNullOrWhiteSpace(scriptView))
            {
                var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"] ?? "";
                var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"] ?? "";
                var area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"] ?? "general";
                scriptView = area.ToString().ToLower() + controllerName.ToString().ToLower() + actionName.ToString().ToLower();
            }

            scriptView = "~/Scripts/" + scriptView;

            var exists = BundleTable.Bundles.GetRegisteredBundles().Any(b => b.Path == scriptView);
            IHtmlString result = null;
            if (exists)
            {
                result = Scripts.Render(scriptView);
            }

            return result;
        }
    }
}
