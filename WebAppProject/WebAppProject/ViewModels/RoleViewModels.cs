using System;
using System.Collections.Generic;

namespace WebAppProject.ViewModels
{
    public class RoleViewModels
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public virtual ICollection<UserViewModels> UserView { get; set; }

    }
}