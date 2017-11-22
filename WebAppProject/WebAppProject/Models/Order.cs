using System;
using System.ComponentModel.DataAnnotations;
using WebAppProject.Services;

namespace WebAppProject.Models
{
    /// <summary>
    /// Classe de Pedido
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo da data de pedido é obrigatório!")]
        [DataType(DataType.DateTime)]
        public DateTime DataPedido { get; set; }

        public StatusService StatusPedido { get; set; }

        [Required(ErrorMessage = "O campo da data de pagamento é obrigatório!")]
        [DataType(DataType.DateTime)]
        public string DataPagamento { get; set; }
        
        public double ValorTotal { get; set; }
    }

}