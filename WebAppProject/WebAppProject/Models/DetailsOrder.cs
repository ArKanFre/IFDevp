using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    public class DetailsOrder
    {
        [Key]
        public int IdDetailsOrder { get; set; }
        
        [Required(ErrorMessage = "O campo da data de pagamento é obrigatório!")]
        [DataType(DataType.DateTime)]
        public string DataPagamento { get; set; }
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Produto { get; set; }

        [ForeignKey("IdOrder")]
        public virtual Order Pedido { get; set; }
    }
}