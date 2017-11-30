using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class CartController : Controller
    {
        private MySystemWebContext db = new MySystemWebContext();
        private string strCart = "Cart";

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<OrderDetails> lsCart = new List<OrderDetails>
                {
                    new OrderDetails(db.Products.Find(id), 1)                     
                };
                Session[strCart] = lsCart;
            }
            else
            {
                List<OrderDetails> lsCart = (List<OrderDetails>)Session[strCart];
                int check = IsExistingCheck(id);
                if (check == -1)
                {
                    lsCart.Add(new OrderDetails(db.Products.Find(id), 1));
                }
                else
                {
                    lsCart[check].Quantity++;
                }
                Session[strCart] = lsCart;
            }
            return View("Index");
        }
        
        private int IsExistingCheck(int? id)
        {
            List<OrderDetails> lsCart = (List<OrderDetails>)Session[strCart];

            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Produto.Id == id) return i;
            }
            return -1;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<OrderDetails> lsCart = (List<OrderDetails>)Session[strCart];
            lsCart.RemoveAt(check);

            return View("Index");
        }

        public ActionResult UpdateCart(FormCollection form)
        {
            string[] quantities = form.GetValues("quantity");
            List<OrderDetails> lstCart = (List<OrderDetails>)Session[strCart];

            for (int i = 0; i < lstCart.Count; i++)
            {
                lstCart[i].Quantity = Convert.ToInt32(quantities[i]);
            }
            Session[strCart] = lstCart;

            return View("Index");
        }

        public ActionResult Checkout()
        {
            return View("Checkout");
        }

        public ActionResult ProcessOrder(FormCollection form)
        {
            List<OrderDetails> lstCart = (List<OrderDetails>)Session[strCart];

            // 1 - Salvar na taela Pedidos
            Order pedido = new Order()
            {
                DataPedido = DateTime.Now,
                TpPagamento = "Debit Card"
            };
            db.Pedidos.Add(pedido);
            db.SaveChanges();

            // 2 - Salvar na tabela Detalhes dos Pedidos
            foreach (OrderDetails cart in lstCart)
            {
                OrderDetails orderDetail = new OrderDetails()
                {
                    IdOrder = pedido.Id,
                    IdProduct = cart.Produto.Id,
                    Quantity = cart.Produto.Quantidade,
                    Price = cart.Produto.Preco
                };
                db.DetailsOrders.Add(orderDetail);
                db.SaveChanges();
            }

            // 3 - Remover a sessão do carrinho
            Session.Remove(strCart);

            return View("OrderSuccess");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}