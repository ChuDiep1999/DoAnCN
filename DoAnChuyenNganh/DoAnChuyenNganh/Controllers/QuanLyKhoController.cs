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
        [HttpGet]
        public ActionResult XemSoLuongMatHang(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Kho model = db.Khoes.SingleOrDefault(n => n.MaKho == id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                var listChiTietMH = db.ChiTietMatHangs.Where(n => n.MaKho == id);
                ViewBag.ListChiTietMH = listChiTietMH;
                return View(model);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult XemShipper(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {

                return View(db.Shippers.Where(n => n.MaKho == id));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult XemDonHang(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.TongThoiGianGiao = db.DonDatHangs.Where(n => n.MaShipper == id&& n.TinhTrangGiaoHang==false).Sum(n => n.ThoiGianGiao);
                return View(db.DonDatHangs.Where(n => n.MaShipper == id && n.TinhTrangGiaoHang == false));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult DonHangChuaCoShipperKho(int ?id)
        {
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.idKho = id;
                return View(db.DonDatHangs.Where(n => n.MaShipper == null && n.MaKho==id && n.ThoiGianGiao != null));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult DonHangChuaTinhThoiGian (int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.DonDatHangs.Where(n => n.MaShipper == null && n.MaKho == id && n.ThoiGianGiao==null));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult DonHangDaGiao(int ?id)
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.DonDatHangs.Where(n => n.MaShipper == id && n.TinhTrangGiaoHang == true));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpGet]
        public ActionResult TinhThoiGian (int? id)
        {

            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonDatHang model = db.DonDatHangs.SingleOrDefault(n => n.MaDonDatHang == id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDonDatHang == id);
                ViewBag.ListChiTietDH = listChiTietDH;
                return View(model);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpPost]
        public ActionResult TinhThoiGian(DonDatHang ddh)
        {
            DonDatHang ddhUpdate = db.DonDatHangs.Single(n => n.MaDonDatHang == ddh.MaDonDatHang);
            ddhUpdate.ThoiGianGiao = ddh.ThoiGianGiao;
            db.SaveChanges();
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDonDatHang == ddh.MaDonDatHang);
            ViewBag.ListChiTietDH = listChiTietDH;
            ViewBag.ThongBaoDuyetThoiGian = "Đã lưu thành công";
            return View(ddhUpdate);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}