using DoAnChuyenNganh.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class QuanLyShipperController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        // GET: QuanLyThanhVien
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoan"] != null)
            {
                int PageSize = 9;
                int PageNumber = (page ?? 1);
                return View(db.Shippers.OrderBy(n => n.MaShipper).ToPagedList(PageNumber, PageSize));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
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
                Shipper tv = db.Shippers.SingleOrDefault(n => n.MaShipper == id);
                if (tv == null)
                {
                    return HttpNotFound();
                }
                
                return View(tv);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Shipper sp)
        {
            Shipper tvUpdate = db.Shippers.Single(n => n.MaShipper == sp.MaShipper);
            ViewBag.MaCaLam = new SelectList(db.CaLams.OrderBy(n => n.CaLam1), "MaCa", "CaLam1", sp.MaCa);
            tvUpdate.HoTen = sp.HoTen;
            tvUpdate.Email = sp.Email;
            tvUpdate.SDT = sp.SDT;
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
                Shipper tv = db.Shippers.SingleOrDefault(n => n.MaShipper == id);
                if (tv == null)
                {
                    return HttpNotFound();
                }
                return View(tv);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpPost]
        public ActionResult Xoa(int id)
        {
            Shipper tv = db.Shippers.SingleOrDefault(n => n.MaShipper == id);
            db.Shippers.Remove(tv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}