using System;
using System.Collections.Generic;

namespace WebAppProject.Models
{
    public class RoleViewModels
    {
        public int RoleId { get; set; }

        public String RoleName { get; set; }

        public virtual ICollection<UserViewModels> UserView { get; set; }

    }
}