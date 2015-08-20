using System.Web;
using System.Web.Http;

namespace AgaBackend
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
           
        }
    }
}
