using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    public class OrderDetails
    {
        [Key]
        public int IdDetailsOrder { get; set; }
        
        [DisplayName("Preço")]
        public double Price { get; set; }

        [DefaultValue(1)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Não existe quantidade de produto negativa ou zerada!")]
        [DisplayName("Quantidade")]
        public int Quantity { get; set; }

        /* ATRIBUTOS QUE SERVIRÃO PARA O RELACIONAMENTO ENTRE OUTRAS
         * ENTIDADES
         */
        public int IdProduct { get; set; }

        public int IdOrder { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Produto { get; set; }

        [ForeignKey("IdOrder")]
        public virtual Order Pedido { get; set; }

        /* CONSTRUTORES */
        public OrderDetails()
        { }

        public OrderDetails(Product prod, int quantity)
        {
            Produto = prod;
            Quantity = quantity;
        }

    }
}