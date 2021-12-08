using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class QuanLyKhuVucController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        // GET: KhuVuc
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.KhuVucs.OrderBy(n => n.MaKhuVuc));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult ThemKhuVuc()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemKhuVuc(KhuVuc kv)
        {
            db.KhuVucs.Add(kv);
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
                KhuVuc kv = db.KhuVucs.SingleOrDefault(n => n.MaKhuVuc == id);
                if (kv == null)
                {
                    return HttpNotFound();
                }   
                return View(kv);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(KhuVuc kv)
        {
            
            db.Entry(kv).State = System.Data.Entity.EntityState.Modified;
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
                KhuVuc kv = db.KhuVucs.SingleOrDefault(n => n.MaKhuVuc == id);
                if (kv == null)
                {
                    return HttpNotFound();
                }
                ViewBag.TenKhuVuc = new SelectList(db.KhuVucs.OrderBy(n => n.TenKhuVuc), "MaKhuVuc", "TenKhuVuc", kv.MaKhuVuc);
                return View(kv);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpPost]
        public ActionResult Xoa(int id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                KhuVuc kv = db.KhuVucs.SingleOrDefault(n => n.MaKhuVuc == id);
                if (kv == null)
                {
                    return HttpNotFound();
                }
                db.KhuVucs.Remove(kv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ThongBaoLoiXoaKV = "Khu vực này đã có thông tin kho, bạn cần xóa thông tin kho trước";
                return View();
            }
           
        }
    }
}