using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace WebAppProject.Models
{
    public class User
    { 
        [Key]
        public string UserId { get; set; }

        [DisplayName("Apelido")]
        public string NickName { get; set; }

        [DisplayName("Nome do Usuário")]
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string UserEmail { get; set; }

        [DisplayName("Imagem do Usuário")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        //NÃO QUERO SALVAR NO BANCO
        [NotMapped]
        public HttpPostedFileBase Images { get; set; }

    }
}