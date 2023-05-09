using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KTRA.Models;

namespace KTRA.Controllers
{
    public class TacGiaController : Controller
    {

        THDAPMEntities db = new THDAPMEntities();

        // GET: Sach
        public ActionResult Index()
        {
            var dsTacGia = db.TacGias.OrderByDescending(s => s.TenTacGia).ToList();
            return View(dsTacGia);
        }

        public ActionResult Shop()
        {
            var dsSach = db.Saches.OrderByDescending(s => s.TenSach).ToList();
            return View(dsSach);
        }

        public ActionResult PartialTacGia()
        {
            return PartialView(db.TacGias.OrderByDescending(tg => tg.TenTacGia).ToList());
        }

        // GET: Sach/Details/5
        public ActionResult Details(int id)
        {
            return View(db.TacGias.Where(s => s.MaTacGia == id).FirstOrDefault());
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sach/Create
        [HttpPost]
        public ActionResult Create(TacGia tg)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(tg.TenTacGia))
                    ModelState.AddModelError(string.Empty, "Tên sach không được để trống");
                if (ModelState.IsValid)
                {
                    db.TacGias.Add(tg);
                    db.SaveChanges();
                    return Redirect("/TacGia/Index");
                }
            }
            return View();
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sach = db.TacGias.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TacGia tg)
        {
            if (ModelState.IsValid)
            {
                var sanPhamDB = db.TacGias.FirstOrDefault(kh => kh.MaTacGia == tg.MaTacGia);
                if (sanPhamDB != null)
                {
                    sanPhamDB.TenTacGia = tg.TenTacGia;
                }
                db.SaveChanges();
                return Redirect("/TacGia/Index");
            }
            return View(tg);
        }

        // GET: Sach/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sach = db.TacGias.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TacGia tg = db.TacGias.Find(id);
            db.TacGias.Remove(tg);
            db.SaveChanges();
            return Redirect("/Sach/Index");
        }
    }
}
