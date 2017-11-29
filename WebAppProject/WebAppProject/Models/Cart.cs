using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class Cart
    {
        public Product Produto { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Não existe quantidade negativa!")]
        public int Quantidade { get; set; }

        public Cart (Product product, int quantity)
        {
            Produto = product;
            Quantidade = quantity;
        }

    }
}