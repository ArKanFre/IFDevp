using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAppProject.Models;
using WebAppProject.Services;

namespace WebAppProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // A CADA MUDANÇA O DATABASE SERÁ APAGADO E CRIAREMOS OUTRO
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            CheckRolesAndSuperUser();
            // Métodos que já foram inicializados com o ASP.NET
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CheckRolesAndSuperUser()
        {
            UsersServices user = new UsersServices();
            user.CheckRoles("Admin");
            user.CheckRoles("User");
            user.CheckSuperUser();

        }
    }
}
