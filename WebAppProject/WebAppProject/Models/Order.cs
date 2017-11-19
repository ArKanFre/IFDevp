using System;
using System.ComponentModel.DataAnnotations;

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
        public DateTime DataPedido { get; set; }

        public string Cliente { get; set; }
        
        public double Valor { get; set; }
    }
}