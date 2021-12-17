using DoAnChuyenNganh.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    [Authorize(Roles = "QuanTri,QuanLyShipper")]
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
            try
            {
                Shipper tv = db.Shippers.SingleOrDefault(n => n.MaShipper == id);
                db.Shippers.Remove(tv);
                ViewBag.ThongbaoxoaShipper = "Đã xóa Shipper thành công";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ThongbaoxoaShipper = "Shipper này đang có đơn hàng, không thể xóa";
                return View(db.Shippers.SingleOrDefault(n => n.MaShipper == id));
            }
        }
        public ActionResult ShipperChuaPhanCong(int? page)
        {
            if (Session["TaiKhoan"] != null)
            {
                int PageSize = 9;
                int PageNumber = (page ?? 1);
                return View(db.Shippers.Where(n => n.MaCa == null && n.MaKho == null).OrderBy(n => n.MaShipper).ToPagedList(PageNumber, PageSize));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult PhanCong(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                Shipper tv = db.Shippers.SingleOrDefault(n => n.MaShipper == id);
                var listKho = db.Khoes.OrderBy(n => n.MaKho);
                var listCaLam = db.CaLams.OrderBy(n => n.MaCa);
                ViewBag.listKho = listKho;
                ViewBag.listCaLam = listCaLam;
                ViewBag.MaKho = new SelectList(db.Khoes.OrderBy(n => n.MaKho), "MaKho", "TenKho", tv.MaKho);
                ViewBag.MaCa = new SelectList(db.CaLams.OrderBy(n => n.MaCa), "MaCa", "CaLam1", tv.MaCa);
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
        public ActionResult PhanCong(Shipper sp)
        {
            Shipper tvUpdate = db.Shippers.Single(n => n.MaShipper == sp.MaShipper);
            int id = (int)sp.MaKho;
            tvUpdate.MaCa = sp.MaCa;
            tvUpdate.MaKho = sp.MaKho;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}