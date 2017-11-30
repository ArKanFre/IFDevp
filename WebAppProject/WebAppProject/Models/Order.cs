using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DisplayName("Data do Pedido")]
        [Required(ErrorMessage = "O campo da data de pedido é obrigatório!")]
        [DataType(DataType.DateTime)]
        public DateTime DataPedido { get; set; }

        [DisplayName("Status")]
        [NotMapped]
        public StatusService StatusPedido { get; set; }

        [DisplayName("Tipo de Pagamento")]
        [Required(ErrorMessage = "O campo tipo de pagamento é obrigatório")]
        public string TpPagamento { get; set; }

        /*A ação abaixo faz com que carrega os dados do produto em modo LAZY,
          evitando que a aplicação execute os dados o tempo todo e fique
          sobrecarregada */
        public virtual ICollection<OrderDetails> Detalhes { get; set; }

        /* ATRIBUTOS QUE SERVIRÃO PARA O RELACIONAMENTO ENTRE OUTRAS
         * ENTIDADES
         */
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

    }

}