using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class UserViewModels
    {
        public int UserId { get; set; }

        public int UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public int UserEmail { get; set; }

        public RoleViewModels Role { get; set; }
                
        public List<RoleViewModels> Roles { get; set; }
    }
}