using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using iclippo.com;

namespace iclippo.com
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();

            // Add Administrator.
            if (!Roles.RoleExists("administrator"))
            {
                Roles.CreateRole("administrator");
            }
            if (Membership.GetUser("admin") == null)
            {
                Membership.CreateUser("admin", "Pa$$w0rd", "hasitha@fclanka.com");
                Roles.AddUserToRole("admin", "administrator");
            }

            // Add Routes.
            RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get last error from the server
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                if (exc.InnerException != null)
                {
                    exc = new Exception(exc.InnerException.Message);
                    Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",
                        true);
                }
            }
        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "HomeRoute",
                "Home",
                "~/Default.aspx"
            );
            routes.MapPageRoute(
                "AboutRoute",
                "About",
                "~/About.aspx"
            );
            routes.MapPageRoute(
                "ContactRoute",
                "Contact",
                "~/Contact.aspx"
            );
            routes.MapPageRoute(
                "SigninRoute",
                "Signin",
                "~/Account/Login.aspx"
            );
            routes.MapPageRoute(
                "SignupRoute",
                "Signup",
                "~/Account/Register.aspx"
            );
            routes.MapPageRoute(
                "ChangePassRoute",
                "ChangePassword",
                "~/Account/ChangePassword.aspx"
            );
            routes.MapPageRoute(
                "ProductListRoute",
                "ProductList",
                "~/ProductList.aspx"
            );

            routes.MapPageRoute(
                "ProductsByCategoryRoute",
                "ProductList/{categoryName}",
                "~/ProductList.aspx"
            );
            routes.MapPageRoute(
                "ProductByNameRoute",
                "Product/{productName}",
                "~/ProductDetails.aspx"
            );
        }
    }
}
