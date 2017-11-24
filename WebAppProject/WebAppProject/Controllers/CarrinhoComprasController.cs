using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        private MySystemWebContext db = new MySystemWebContext();

        public ActionResult AdicionarCarrinho(int id)
        {
            // Código retirado da questão: https://pt.stackoverflow.com/questions/40857/implementa%C3%A7%C3%A3o-de-carrinho-de-compras-em-asp-net-mvc
            // Ao invés de colocar uma lista de ítens de Design, vamos colocar
            // Um objeto da entidade Pedido, que já possui List<ItemDesign>.
            // List<ItemDesign> carrinho = Session["Carrinho"] != null ? (List<ItemDesign>)Session["Carrinho"] : new List<ItemDesign>();
            Order carrinho = Session["Carrinho"] != null ? (Order)Session["Carrinho"] : new Order();

            var product = db.Products.Find(id);

            if (product != null)
            {
                var itemDesign = new DetailsOrder();
                itemDesign.Produto = product;
                itemDesign.Produto.Quantidade = 1;

                if (carrinho.Detalhes.FirstOrDefault(x => x.IdDetailsOrder == product.Id) != null)
                {
                    carrinho.Detalhes.FirstOrDefault(x => x.IdDetailsOrder == product.Id).Produto.Quantidade += 1;
                }

                else
                {
                    carrinho.Detalhes.Add(itemDesign);
                }

                // Aqui podemos fazer o cálculo do valor

                carrinho.ValorTotal = carrinho.Detalhes.Select(i => i.Produto).Sum(d => d.Preco);

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho()
        {
            Order carrinho = Session["Carrinho"] != null ? (Order)Session["Carrinho"] : new Order();

            return View(carrinho);
        }

        // GET: CarrinhoCompras
        public ActionResult Index()
        {
            var detailsOrders = db.DetailsOrders.Include(d => d.Pedido).Include(d => d.Produto);
            return View(detailsOrders.ToList());
        }

        // GET: CarrinhoCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailsOrder);
        }

        // GET: CarrinhoCompras/Create
        public ActionResult Create()
        {
            ViewBag.IdOrder = new SelectList(db.Pedidos, "Id", "Id");
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Nome");
            return View();
        }

        // POST: CarrinhoCompras/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetailsOrder,DataPagamento,ValorTotal,IdProduct,IdOrder")] DetailsOrder detailsOrder)
        {
            if (ModelState.IsValid)
            {
                db.DetailsOrders.Add(detailsOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = new SelectList(db.Pedidos, "Id", "Id", detailsOrder.IdOrder);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Nome", detailsOrder.IdProduct);
            return View(detailsOrder);
        }

        // GET: CarrinhoCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOrder = new SelectList(db.Pedidos, "Id", "Id", detailsOrder.IdOrder);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Nome", detailsOrder.IdProduct);
            return View(detailsOrder);
        }

        // POST: CarrinhoCompras/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetailsOrder,DataPagamento,ValorTotal,IdProduct,IdOrder")] DetailsOrder detailsOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailsOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOrder = new SelectList(db.Pedidos, "Id", "Id", detailsOrder.IdOrder);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Nome", detailsOrder.IdProduct);
            return View(detailsOrder);
        }

        // GET: CarrinhoCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailsOrder);
        }

        // POST: CarrinhoCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            db.DetailsOrders.Remove(detailsOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
