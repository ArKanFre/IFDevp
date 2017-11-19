using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            AddRolesSuperUser(db);
            db.Dispose();
            // Métodos que já foram inicializados com o ASP.NET
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /*
         * ADICIONANDO PERMISSÕES AO SUPER USUÁRIO
         */
        private void AddRolesSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByEmail("arionmelkan@gmail.com");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            // Verificando se o usuário não possui acesso ao método
            if (!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");
            }

            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }

            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }

            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }

        }

        /*
         * MÉTODO QUE FAZ A GERAÇÃO DO SUPER USUÁRIO
         */
        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Procurando pelo e-mail e definindo o meu super usuário
            var user = userManager.FindByEmail("arionmelkan@gmail.com");
            
            if (user == null)
            {
                Response.Redirect("~/Views/Account/Login.cshtml");
            }

        }

        /*
         * MÉTODO QUE FAZ A GERAÇÃO DOS CONTROLES DA
         * VIEW, EDIT, CREATE E DELETE
         */
        private void CreateRoles(ApplicationDbContext db)
        {
            // A variável roleManager armazena os métodos: View, Create, Edit e Delete
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            // Condição para verificar se não existe um desses métodos
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }

        }
    }
}
