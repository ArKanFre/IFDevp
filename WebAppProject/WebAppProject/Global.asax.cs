using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAppProject.Models;

namespace WebAppProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Parte que aponta para a conexão default
            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);
            //AddRolesSuperUser(db);
            db.Dispose();
            // Métodos que já foram inicializados com o ASP.NET
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /*
         * ADICIONANDO PERMISSÃO AO SUPER USUÁRIO
         */
        private void AddRolesSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByEmail(ConfigurationManager.AppSettings["emailSuperUser"]);
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            // Verificando se o usuário não possui acesso restrito
            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                userManager.AddToRole(user.Id, "Admin");
            }

        }

        /*
         * MÉTODO QUE FAZ A GERAÇÃO DO SUPER USUÁRIO
         */
        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Procurando pelo e-mail e definindo o meu super usuário
            var user = userManager.FindByEmail(ConfigurationManager.AppSettings["emailSuperUser"]);
            
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = ConfigurationManager.AppSettings["superUser"],
                    Email = ConfigurationManager.AppSettings["emailSuperUser"]
                };

                userManager.Create(user, ConfigurationManager.AppSettings["password"]);

            }

        }

        /*
         * MÉTODO QUE FAZ A GERAÇÃO DOS CONTROLES DA
         * VIEW, EDIT, CREATE E DELETE
         */
        private void CreateRoles(ApplicationDbContext db)
        {            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            // Condição para verificar se não existe um desses acessos
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole("User"));
            }

        }
    }
}
