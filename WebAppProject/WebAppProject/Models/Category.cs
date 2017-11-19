using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class Category
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo nome da categoria é obrigatório!")]
        [MaxLength(100)]
        [DisplayName("Nome da Categoria")]
        public String NomeCategoria { get; set; }

        /*A ação abaixo faz com que carrega os dados do produto em modo LAZY,
          evitando que a aplicação execute os dados o tempo todo e fique
          sobrecarregada */
        public virtual ICollection<Product> Produtos { get; set; }
    }
}