using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarsRental.Startup))]
namespace CarsRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
