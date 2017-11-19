using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    public class Stock
    {
        //Data Anotations
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Quantidade é um campo obrigatório!")]
        [DefaultValue(0)]
        //Data Anotation para disponibilizar um range do 0 até o maior elemento de um inteiro
        [Range(0, Int32.MaxValue, ErrorMessage = "Valor mínimo é zero")]
        public int Quantidade { get; set; }


        /* ATRIBUTOS QUE SERVIRÃO PARA O RELACIONAMENTO ENTRE OUTRAS
         * ENTIDADES
         */        
        //É importante ter esse campo, pois é com ele que faremos o reconhecimento da chave estrangeira
        public int ProdutoId { get; set; }

        [Required]
        [ForeignKey("ProdutoId")]
        public Product Produto { get; set; }

    }
}