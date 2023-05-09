using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KTRA.Models;

namespace KTRA.Controllers
{
    public class SachController : Controller
    {
        THDAPMEntities db = new THDAPMEntities();
        
        // GET: Sach
        public ActionResult Index()
        {
            var dsSach = db.Saches.OrderByDescending(s => s.TenSach).ToList();
            return View(dsSach);
        }
        
        public ActionResult SachTheoTacGia(int id)
        {
            return View("Index", db.Saches.Where(tg => tg.MaTacGia == id).ToList());
        }
        // GET: Sach/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Saches.Where(s => s.MaSach == id).FirstOrDefault());
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia");
            return View();
        }

        // POST: Sach/Create
        [HttpPost]
        public ActionResult Create(Sach sach)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(sach.TenSach))
                    ModelState.AddModelError(string.Empty, "Tên sach không được để trống");
                if (ModelState.IsValid)
                {
                    db.Saches.Add(sach);
                    db.SaveChanges();
                    return Redirect("/Sach/Index");
                }
            }
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            return View();
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            return View(sach);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Sach sach)
        {
            if (ModelState.IsValid)
            {
                var sanPhamDB = db.Saches.FirstOrDefault(kh => kh.MaSach == sach.MaSach);
                if (sanPhamDB != null)
                {
                    sanPhamDB.TenSach = sach.TenSach;
                    sanPhamDB.DonGia = sach.DonGia;
                    sanPhamDB.MaTacGia = sach.MaTacGia;
                }
                db.SaveChanges();
                return Redirect("/Sach/Index");
            }
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            return View(sach);
        }

        // GET: Sach/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sach = db.Saches.Find(id);
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
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return Redirect("/Sach/Index");
        }
    }
}
