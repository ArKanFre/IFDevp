using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    public class Product
    {
        //Data Anotations
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome do produto é obrigatório!")]
        [MaxLength(100)]
        [DisplayName("Nome do Produto")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório!")]
        [DisplayName("Descrição")]
        public String Descricao { get; set; }

        [Required(ErrorMessage = "O campo preço é obrigatório")]
        [DisplayName("Preço")]
        public double Preco { get; set; }

        /* ATRIBUTOS QUE SERVIRÃO PARA O RELACIONAMENTO ENTRE OUTRAS
         * ENTIDADES
         */
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Category Categoria { get; set; }

        public int FornecedorId { get; set; }

        [ForeignKey("FornecedorId")]
        public Provider Fornecedor { get; set; }

    }
}