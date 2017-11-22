using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels
{
    public class UserViewModels
    {
        public string UserId { get; set; }

        public string NickName { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        public RoleViewModels Role { get; set; }
        
    }
}