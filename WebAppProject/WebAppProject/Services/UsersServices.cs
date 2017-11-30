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
         * ADICIONANDO PERMISSÃO AO SUPER USUÁRIO
         
        public void AddRolesSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var user = userManager.FindByEmail(WebConfigurationManager.AppSettings["emailSuperUser"]);
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            // Verificando se o usuário não possui acesso restrito
            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                userManager.AddToRole(user.Id, "Admin");
            }

        } */

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
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email
            };

            userManager.Create(userASP, pass);
            userManager.AddToRole(userASP.Id, roleName);
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

        public bool DeleteUser(string userEmail)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var user = userManager.FindByEmail(userEmail);

            if (user == null)
            {
                return false;
            }

            var response = userManager.Delete(user);

            return response.Succeeded;
        }

        public bool UpdateUser(string userEmail, string userName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var user = userManager.FindByEmail(userEmail);

            if (user == null)
            {
                return false;
            }
            user.Email = userEmail;
            user.UserName = userName;
            var response = userManager.Update(user);

            return response.Succeeded;
        }

    }
}