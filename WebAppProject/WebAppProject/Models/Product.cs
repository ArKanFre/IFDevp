using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

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
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório!")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "É para conter duas casas decimais")]
        [Required(ErrorMessage = "O campo preço é obrigatório")]
        [DisplayName("Preço")]
        public double Preco { get; set; }

        [DisplayName("Imagem do Produto")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [DisplayName("Quantidade do Produto")]
        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Não temos quantidade negativa!")]
        public int Quantidade { get; set; }

        //NÃO QUERO SALVAR NO BANCO
        [NotMapped]
        public HttpPostedFileBase Images { get; set; }

        /* ATRIBUTOS QUE SERVIRÃO PARA O RELACIONAMENTO ENTRE OUTRAS
         * ENTIDADES
         */
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Category Categoria { get; set; }

        public int FornecedorId { get; set; }

        [ForeignKey("FornecedorId")]
        public Provider Fornecedor { get; set; }

        /*A ação abaixo faz com que carrega os dados do produto em modo LAZY,
          evitando que a aplicação execute os dados o tempo todo e fique
          sobrecarregada */
        [NotMapped]
        public virtual ICollection<OrderDetails> Detalhes { get; set; }

    }
}