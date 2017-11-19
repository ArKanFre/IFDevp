using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class Itens
    {
        [Key]
        public int Id { get; set; }

        public Order Pedido { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Não existe pedido negativo!")]
        public int Quantidade { get; set; }

        public string Produto { get; set; }

        public double ValorUnitario { get; set; }
    }
}