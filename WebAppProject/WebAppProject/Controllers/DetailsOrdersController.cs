using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class DetailsOrdersController : Controller
    {
        private MySystemWebContext db = new MySystemWebContext();



        // GET: DetailsOrders
        public ActionResult Index()
        {
            var detailsOrders = db.DetailsOrders.Include(d => d.Pedido).Include(d => d.Produto);
            return View(detailsOrders.ToList());
        }

        // GET: DetailsOrders/Details/5
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

        // GET: DetailsOrders/Create
        public ActionResult Create()
        {
            ViewBag.IdOrder = new SelectList(db.Pedidos, "Id", "Id");
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Nome");
            return View();
        }

        // POST: DetailsOrders/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetailsOrder,DataPagamento,IdProduct,IdOrder")] DetailsOrder detailsOrder)
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

        // GET: DetailsOrders/Edit/5
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

        // POST: DetailsOrders/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetailsOrder,DataPagamento,IdProduct,IdOrder")] DetailsOrder detailsOrder)
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

        // GET: DetailsOrders/Delete/5
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

        // POST: DetailsOrders/Delete/5
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
