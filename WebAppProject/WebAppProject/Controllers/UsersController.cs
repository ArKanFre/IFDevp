using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebAppProject.Models;
using WebAppProject.Services;

namespace WebAppProject.Controllers
{
    public class UsersController : Controller
    {
        private MySystemWebContext db = new MySystemWebContext();
        private static UsersServices userServ = new UsersServices();

        [Authorize(Roles = "Admin")]
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Users/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Images";

                    if (user.Images != null)
                    {
                        pic = ImageService.UploadPicture(user.Images, folder);
                        if (!string.IsNullOrEmpty(pic))
                        {
                            user.Image = string.Format("{0}/{1}", folder, pic);
                        }
                    }
                    db.Users.Add(user);
                    db.SaveChanges();
                    userServ.CreateUserAsp(user.UserEmail, "User", "123@Senha");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // POST: Users/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Images";

                    if (user.Images != null)
                    {
                        pic = ImageService.UploadPicture(user.Images, folder);
                        if (!string.IsNullOrEmpty(pic))
                        {
                            user.Image = string.Format("{0}/{1}", folder, pic);
                        }
                    }
                    var db2 = new MySystemWebContext();
                    var currentUsers = db2.Users.Find(user.UserId);
                    if (currentUsers.UserEmail != user.UserEmail)
                    {
                        userServ.UpdateUser(user.UserEmail, user.UserEmail);
                    }
                    db2.Dispose();

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            userServ.DeleteUser(user.UserEmail);

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
