namespace WebAppProject.Services
{
    /*
     * CLASSE QUE PRESTARÁ SERVIÇO DE STATUS
     * PRINCIPALMENTE PARA O STATUS SERVICE
     */
    public class StatusService
    {
        enum Pedidos
        {
            Aguardando,
            Finalizado,
            Inativo,
            Inicializado

        };

        Pedidos orders = Pedidos.Inicializado;

    }
}