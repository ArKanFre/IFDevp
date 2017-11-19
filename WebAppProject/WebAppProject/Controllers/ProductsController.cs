using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class ProductsController : Controller
    {
        private MySystemWebContext db = new MySystemWebContext();

        // Todo mundo pode olhar
        [AllowAnonymous]
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Categoria).Include(p => p.Fornecedor);
            return View(products.ToList());
        }

        // Todo mundo pode olhar
        [AllowAnonymous]
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

        // Comando para permitir um usuário que tem acesso ao cadastrado
        [Authorize(Roles = "Create")]
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categories, "CategoriaId", "NomeCategoria");
            ViewBag.FornecedorId = new SelectList(db.Providers, "Id", "NomeFornecedor");
            return View();
        }

        // Comando para permitir um usuário que tem acesso ao cadastrado
        [Authorize(Roles = "Create")]
        // POST: Products/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Preco,CategoriaId,FornecedorId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categories, "CategoriaId", "NomeCategoria", product.CategoriaId);
            ViewBag.FornecedorId = new SelectList(db.Providers, "Id", "NomeFornecedor", product.FornecedorId);
            return View(product);
        }

        // Comando para permitir um usuário que tem acesso a edição
        [Authorize(Roles = "Edit")]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoriaId = new SelectList(db.Categories, "CategoriaId", "NomeCategoria", product.CategoriaId);
            ViewBag.FornecedorId = new SelectList(db.Providers, "Id", "NomeFornecedor", product.FornecedorId);
            return View(product);
        }

        // Comando para permitir um usuário que tem acesso a edição
        [Authorize(Roles = "Edit")]
        // POST: Products/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Preco,CategoriaId,FornecedorId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categories, "CategoriaId", "NomeCategoria", product.CategoriaId);
            ViewBag.FornecedorId = new SelectList(db.Providers, "Id", "NomeFornecedor", product.FornecedorId);
            return View(product);
        }

        // Comando para permitir um usuário que tem acesso ao delete
        [Authorize(Roles = "Delete")]
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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

        // Comando para permitir um usuário que tem acesso ao delete
        [Authorize(Roles = "Delete")]
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
