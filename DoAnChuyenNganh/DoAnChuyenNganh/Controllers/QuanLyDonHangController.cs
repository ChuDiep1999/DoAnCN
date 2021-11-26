using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;
namespace DoAnChuyenNganh.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        Ship2hEntities db = new Ship2hEntities();
        public ActionResult DonHangChuaCoShipper()
        {
            if (Session["TaiKhoan"] != null)
            {
                var listDSDHCG = db.DonDatHangs.Where(n => n.MaShipper == null && n.DaThanhToan == false && n.TinhTrangGiaoHang ==false).OrderBy(n => n.ThoiGianDat);
                return View(listDSDHCG);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult ChuaThanhToanChuaGiao()
        {
            if (Session["TaiKhoan"] != null)
            {
                var listDSDHCG = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == false && n.DaThanhToan == false&&n.MaShipper!=null).OrderBy(n => n.ThoiGianDat);
                return View(listDSDHCG);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult ChuaGiaoDaThanhToan()
        {
            if (Session["TaiKhoan"] != null)
            {
                var listDSDHCG = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == false && n.DaThanhToan == true && n.MaShipper != null).OrderBy(n => n.ThoiGianDat);
                return View(listDSDHCG);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult DaThanhToanDaGiao()
        {
            if (Session["TaiKhoan"] != null)
            {
                var listDSDHCG = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == true && n.DaThanhToan == true && n.MaShipper != null).OrderBy(n => n.ThoiGianDat);
                return View(listDSDHCG);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        public ActionResult ChonShipper(int? id)
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
                ViewBag.MaShipper = new SelectList(db.Shippers.OrderBy(n => n.MaShipper), "MaShipper", "MaShipper", model.MaShipper);
                return View(model);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChonShipper(DonDatHang ddh)
        {
            ViewBag.MaShipper = new SelectList(db.Shippers.OrderBy(n => n.MaShipper), "MaShipper", "MaShipper", ddh.MaShipper);
            DonDatHang tvUpdate = db.DonDatHangs.Single(n => n.MaDonDatHang == ddh.MaDonDatHang);
            tvUpdate.MaShipper = ddh.MaShipper;
            db.SaveChanges();
            return RedirectToAction("DonHangChuaCoShipper");
        }
        public ActionResult DanhSachShipperPartial()
        {
            var listShipper = db.Shippers.Where(n=>n.DangDiGiao==false);
            return PartialView(listShipper);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
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
        public ActionResult DuyetDonHang(DonDatHang ddh)
        {
            //Truy Van lay ra du lieu cua don hang do
            DonDatHang ddhUpdate = db.DonDatHangs.Single(n => n.MaDonDatHang == ddh.MaDonDatHang);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            db.SaveChanges();
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDonDatHang == ddh.MaDonDatHang);
            ViewBag.ListChiTietDH = listChiTietDH;
            ViewBag.ThongBaoDuyet = "Đã lưu thành công";
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