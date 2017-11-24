using System;
using System.Collections.Generic;
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

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Não existe pedido negativo!")]
        public int Quantidade { get; set; }

        [Required]
        public Decimal ValorTotal { get; set; }

        /*A ação abaixo faz com que carrega os dados do produto em modo LAZY,
          evitando que a aplicação execute os dados o tempo todo e fique
          sobrecarregada */
        public virtual ICollection<DetailsOrder> Detalhes { get; set; }

    }

}