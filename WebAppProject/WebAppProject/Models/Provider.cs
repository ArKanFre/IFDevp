using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    /// <summary>
    /// Classe que é o fornecedor
    /// </summary>
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome do fornecedor é obrigatório!")]
        [MaxLength(100)]
        [DisplayName("Nome do Fornecedor")]
        public String NomeFornecedor { get; set; }

        [Required(ErrorMessage = "O campo data de inclusão é obrigatório!")]
        public DateTime DataInclusao { get; set; }

        /*A ação abaixo faz com que carrega os dados do produto em modo LAZY,
          evitando que a aplicação execute os dados o tempo todo e fique
          sobrecarregada */         
        public virtual ICollection<Product> Produtos { get; set; }

    }
}