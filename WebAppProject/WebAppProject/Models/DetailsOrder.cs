using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    public class DetailsOrder
    {
        [Key]
        public int IdProduct { get; set; }

        [Key]
        public int IdOrder { get; set; }
        
        [ForeignKey("IdProduct")]
        public Product Produto { get; set; }

        [ForeignKey("IdOrder")]
        public Order Pedido { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Não existe pedido negativo!")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo da data de pagamento é obrigatório!")]
        [DataType(DataType.DateTime)]
        public string DataPagamento { get; set; }

        public double ValorTotal { get; set; }
    }
}