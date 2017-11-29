using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Products.Find(id),1)                     
                };
                Session[strCart] = lsCart;
            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = IsExistingCheck(id);
                if (check == -1)
                {
                    lsCart.Add(new Cart(db.Products.Find(id), 1));
                }
                else
                {
                    lsCart[check].Quantidade++;
                }
                Session[strCart] = lsCart;
            }
            return View("Index");
        }
        
        private int IsExistingCheck(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[strCart];

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
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.RemoveAt(check);

            return View("Index");
        }
    }
}