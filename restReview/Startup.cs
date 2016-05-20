using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(restReview.Startup))]
namespace restReview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
