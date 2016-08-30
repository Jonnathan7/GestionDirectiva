using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GDirectiva.Presentacion.Web.Startup))]
namespace GDirectiva.Presentacion.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
