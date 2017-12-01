using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web.Configuration;
using WebAppProject.Models;

namespace WebAppProject.Services
{
    public class UsersServices : IDisposable
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();

        /*
         * MÉTODO QUE FAZ A VERIFICAÇÃO DO SUPER USUÁRIO
         */
        public void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            //Procurando pelo e-mail e definindo o meu super usuário
            var email = WebConfigurationManager.AppSettings["emailSuperUser"];
            var userName = WebConfigurationManager.AppSettings["superUser"];
            var user = userManager.FindByEmail(WebConfigurationManager.AppSettings["emailSuperUser"]);

            if (user == null)
            {
                CreateUserAsp(email, "Admin", WebConfigurationManager.AppSettings["password"]);
                return;
            }

            userManager.AddToRole(user.Id, "Admin");
        }

        /*
         * MÉTODO QUE FAZ A GERAÇÃO DO SUPER USUÁRIO
         */
        public void CreateUserAsp(string email, string roleName, string pass)
        {
            try
            {
                var store = new UserStore<ApplicationUser>(userContext);
                var userManager = new ApplicationUserManager(store);

                var userASP = new ApplicationUser
                {
                    Email = email
                };

                userManager.Create(userASP, pass);
                userManager.AddToRole(userASP.Id, roleName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*
         * MÉTODO QUE FAZ A VERIFICAÇÃO DOS CONTROLES DA
         * VIEW, EDIT, CREATE E DELETE
         */
        public void CheckRoles(String roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // Condição para verificar se não existe um desses acessos
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        public void Dispose()
        {
            userContext.Dispose();
        }
    }
}