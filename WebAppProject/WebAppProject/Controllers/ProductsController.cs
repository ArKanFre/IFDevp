using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebAppProject.Models;
using WebAppProject.Services;
using PagedList;

namespace WebAppProject.Controllers
{
    public class ProductsController : Controller
    {
        private MySystemWebContext db = new MySystemWebContext();

        // Todo mundo pode olhar
        [AllowAnonymous]
        // GET: Products
        public ActionResult Produtos(int? page, int? category)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            if (category != null)
            {
                ViewBag.category = category;
                var productList = db.Products
                                .OrderByDescending(p => p.Id)
                                .Where(c => c.CategoriaId == category)
                                .ToPagedList(pageNumber, pageSize);
                return View(productList);
            }
            else
            {
                var productList = db.Products.OrderByDescending(p => p.Id).ToPagedList(pageNumber, pageSize);
                return View(productList);
            }
        }

        [HttpGet]
        public ActionResult BuscaProduto(int? page, int? category)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            if (category != null)
            {
                ViewBag.category = category;
                var productList = db.Products
                                .OrderByDescending(p => p.Id)
                                .Where(c => c.CategoriaId == category)
                                .ToPagedList(pageNumber, pageSize);
                return View(productList);
            }
            else
            {
                var productList = db.Products.OrderByDescending(p => p.Id).ToPagedList(pageNumber, pageSize);
                return View(productList);
            }
        }

        [HttpPost]
        public ActionResult BuscaProduto(string texto)
        {
            return View(db.Products.Where(x => x.Nome.Contains(texto)).OrderBy(x => x.Nome));
        }

        // Aó administrador
        [Authorize(Roles="Admin")]
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
            else
            {
                product.Categoria = db.Categories.Find(product.CategoriaId);
                product.Fornecedor = db.Providers.Find(product.FornecedorId);
            }

            return View(product);
        }

        // Comando para permitir um usuário que tem acesso ao cadastrado
        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categories, "CategoriaId", "NomeCategoria");
            ViewBag.FornecedorId = new SelectList(db.Providers, "Id", "NomeFornecedor");
            return View();
        }

        // Comando para permitir um usuário que tem acesso ao cadastrado
        [Authorize(Roles = "Admin")]
        // POST: Products/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Images";

                    if (product.Images != null)
                    {
                        pic = ImageService.UploadPicture(product.Images, folder);
                        if (!string.IsNullOrEmpty(pic))
                        {
                            product.Image = string.Format("{0}/{1}", folder, pic);
                        }
                    }
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.CategoriaId = new SelectList(db.Categories, "CategoriaId", "NomeCategoria", product.CategoriaId);
            ViewBag.FornecedorId = new SelectList(db.Providers, "Id", "NomeFornecedor", product.FornecedorId);
            return View(product);
        }

        // Comando para permitir um usuário que tem acesso a edição
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        // POST: Products/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Images";

                    if (product.Images != null)
                    {
                        pic = ImageService.UploadPicture(product.Images, folder);
                        if (!string.IsNullOrEmpty(pic))
                        {
                            product.Image = string.Format("{0}/{1}", folder, pic);
                        }
                    }
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.CategoriaId = new SelectList(db.Categories, "CategoriaId", "NomeCategoria", product.CategoriaId);
            ViewBag.FornecedorId = new SelectList(db.Providers, "Id", "NomeFornecedor", product.FornecedorId);
            return View(product);
        }

        // Comando para permitir um usuário que tem acesso ao delete
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
