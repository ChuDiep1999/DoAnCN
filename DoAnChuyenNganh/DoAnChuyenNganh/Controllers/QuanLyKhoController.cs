using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;
namespace DoAnChuyenNganh.Controllers
{
    public class QuanLyKhoController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        // GET: QuanLyKho
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.Khoes.OrderBy(n => n.MaKho));
            }
            else
            {
                return RedirectToAction("DangNhap","Home");
            }
            
        }

        public ActionResult ThemKho()
        {
            ViewBag.MaKhuVuc = new SelectList(db.KhuVucs.OrderBy(n => n.TenKhuVuc), "MaKhuVuc", "TenKhuVuc");
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap","Home");
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemKho(Kho kho)
        {
            ViewBag.MaKhuVuc = new SelectList(db.KhuVucs.OrderBy(n => n.TenKhuVuc), "MaKhuVuc", "TenKhuVuc");
            db.Khoes.Add(kho);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == id);
                if (kho == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MaKhuVuc = new SelectList(db.KhuVucs.OrderBy(n => n.TenKhuVuc), "MaKhuVuc", "TenKhuVuc", kho.MaKhuVuc);
                return View(kho);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Kho kho)
        {
            ViewBag.MaKhuVuc = new SelectList(db.KhuVucs.OrderBy(n => n.TenKhuVuc), "MaKhuVuc", "TenKhuVuc", kho.MaKhuVuc);
            db.Entry(kho).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == id);
                if (kho == null)
                {
                    return HttpNotFound();
                }
                ViewBag.TenKhuVuc = new SelectList(db.KhuVucs.OrderBy(n => n.TenKhuVuc), "MaKhuVuc", "TenKhuVuc", kho.MaKhuVuc);
                return View(kho);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpPost]
        public ActionResult Xoa(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == id);
            if (kho == null)
            {
                return HttpNotFound();
            }
            db.Khoes.Remove(kho);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}