using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppProject.Models;

namespace WebAppProject.Services
{
    /*
     * CLASSE GERADA A PARTIR DOS ESTUDOS DOS LINKS:
     * https://pt.stackoverflow.com/questions/158423/cria%C3%A7%C3%A3o-de-um-carrinho-de-compras
     * E https://pt.stackoverflow.com/questions/40857/implementa%C3%A7%C3%A3o-de-carrinho-de-compras-em-asp-net-mvc
     */
    public class CarrinhoComprasService
    {
        const string CHAVE_PEDIDOS = "CarrinhoWebApp";
        MySystemWebContext db = new MySystemWebContext();

        // ADICIONAR PEDIDOS NO CARRINHO
       /* public static double addOrders(Product produtoPedido)
        {
            List<Product> pedidos = 
        }


        public static double RetornarTotalCarrinho()
        {
            double total = 0.0;

            foreach (var item in RetornarCarrinho())
            {
                total += (item.Key.Preco * item.Value);
            }

            return total;
        }

        public static void AdicionarProdutoCarrinho(int produtoID, int quantidade)
        {
            if (RetornarProdutos() != null)
            {
                lista = RetornarProdutos();
            }
            else
            {
                CriarCarrinho();
            }

            if (db.Produto.Find(produtoID).Quantidade > 0)
            {
                if (lista.ContainsKey(produtoID))
                {
                    IncrementarProdutoCarrinho(produtoID);
                }
                else
                {
                    lista.Add(produtoID, quantidade);
                }
            }

            AtualizarLista();
        }

        public static void RemoverProdutoCarrinho(int produtoID)
        {
            lista = RetornarProdutos();
            lista.Remove(produtoID);
            AtualizarLista();
        }

        public static void AtualizarProdutoCarrinho(int produtoID, int novaQuantidade)
        {
            lista = RetornarProdutos();
            lista[produtoID] = novaQuantidade;
            AtualizarLista();
        }

        public static void IncrementarProdutoCarrinho(int produtoID)
        {
            Contexto db = new Contexto();
            int max = db.Produto.Find(produtoID).Quantidade;

            lista = RetornarProdutos();

            if (lista[produtoID] < max)
            {
                lista[produtoID]++;
            }
            else if (lista[produtoID] > max)
            {
                lista[produtoID] = max;
            }

            AtualizarLista();
        }

        public static void DecrementarProdutoCarrinho(int produtoID)
        {
            lista = RetornarProdutos();

            if (lista[produtoID] > 1)
            {
                lista[produtoID]--;
            }

            AtualizarLista();
        }

        public static void LimparCarrinho()
        {
            lista.Clear();
            AtualizarLista();
        }

        public static bool EstaNoCarrinho(int produtoID)
        {
            lista = RetornarProdutos();

            if (lista == null)
            {
                CriarCarrinho();
            }

            return lista.ContainsKey(produtoID);
        }

        private static void CriarCarrinho()
        {
            lista = new Dictionary<int, int>();
        }

        private static void ListCurrent()
        {
            HttpContext.Current.Session[CHAVE_PEDIDOS] = lista;
        }*/
    }
}