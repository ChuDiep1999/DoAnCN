using CaptchaMvc.HtmlHelpers;
using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class ShipperController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        // GET: Shipper
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(Shipper sp)
        {

            // Kiểm tra capcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    var check = db.Shippers.FirstOrDefault(s => s.TenDangNhap == sp.TenDangNhap);
                    if (sp.MatKhau == sp.NhapLaiMatKhau)
                    {
                        if (check == null)
                        {
                            ViewBag.ThongBaoDangKyShipper = "Đăng ký thành công";
                            //thêm thành viên
                            sp.MaLoaiThanhVien = 7;
                            sp.DangDiGiao = false;
                            db.Shippers.Add(sp);
                            db.SaveChanges();
                            return View();
                        }
                        else
                        {
                            ViewBag.error = "Tài khoản đã tồn tại";
                            ViewBag.ThongBaoDangKyShipper = "Đăng ký thất bại";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.error2 = "Mật khẩu không trùng";
                        ViewBag.ThongBaoDangKyShipper = "Đăng ký thất bại";
                        return View();
                    }

                }
                else
                {
                    ViewBag.ThongBaoDangKyShipper = "Đăng ký thất bại";
                    return View();
                }

            }
            ViewBag.ThongBaoDangKyShipper = "Sai mã Captcha";
            return View();
        }
        public ActionResult DoiMatKhau()
        {

            if (Session["TaiKhoanShipper"] != null)
            {
                int id = (int)Session["IdShipper"];
                Shipper sp = db.Shippers.SingleOrDefault(n => n.MaShipper == id);
                return View(sp);
            }
            else
            {
                return RedirectToAction("DangNhap");
            }
        }
        public ActionResult DangNhap()
        {
           
                return View();
   
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string taikhoan = f["txtTenDangNhap"].ToString();
            string matkhau = f["txtMatKhau"].ToString();
            var data = db.Shippers.Where(s => s.TenDangNhap.Equals(taikhoan) && s.MatKhau.Equals(matkhau)).ToList();
            Shipper tv = db.Shippers.SingleOrDefault(n => n.TenDangNhap == taikhoan && n.MatKhau == matkhau);
            if (tv != null)
            {
                Session["TaiKhoanShipper"] = tv;
                Session["HoTenShipper"] = data.FirstOrDefault().HoTen;
                Session["IdShipper"] = data.FirstOrDefault().MaShipper;
                Session["LoaiThanhVien"] = data.FirstOrDefault().LoaiThanhVien;
                return RedirectToAction("Index", "Shipper");

            }
            ViewBag.ThongBaoDangNhapShipper = "Thông tin đăng nhập không chính xác";
            return View();
        }
        public ActionResult Index()
        {
            
            if (Session["TaiKhoanShipper"] != null)
            {
                int id = (int)Session["IdShipper"];
                var listDSDHCG = db.DonDatHangs.Where(n=>n.MaShipper==id&&n.TinhTrangGiaoHang==false).OrderBy(n => n.ThoiGianDat);
                return View(listDSDHCG);
            }
            else
            {
                return RedirectToAction("DangNhap", "Shipper");
            }
        }
        public ActionResult DaGiao()
        {

            if (Session["TaiKhoanShipper"] != null)
            {
                int id = (int)Session["IdShipper"];
                var listDSDHCG = db.DonDatHangs.Where(n => n.MaShipper == id && n.TinhTrangGiaoHang == true).OrderBy(n => n.ThoiGianDat);
                return View(listDSDHCG);
            }
            else
            {
                return RedirectToAction("DangNhap", "Shipper");
            }
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? id, int? maSP)
            {
                if (Session["TaiKhoanShipper"] != null)
                {
                    maSP= (int)Session["IdShipper"];
                if (id == null||maSP==null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    DonDatHang model = db.DonDatHangs.SingleOrDefault(n => n.MaDonDatHang == id&&n.MaShipper==maSP);
                    if (model == null)
                    {
                        return HttpNotFound();
                    }
                    var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDonDatHang == id&&n.DonDatHang.MaShipper==maSP);
                    ViewBag.ListChiTietDH = listChiTietDH;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("DangNhap", "Shipper");
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
        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhap");
        }
        public ActionResult ThongTinTaiKhoan()
        {
            if (Session["TaiKhoanShipper"] != null)
            {
                int id = (int)Session["IdShipper"];
                Shipper tv = db.Shippers.SingleOrDefault(n => n.MaShipper == id);
                return View(tv);
            }
            else
            {
                return RedirectToAction("DangNhap", "Shipper");
            }
        }
        [HttpPost]
        public ActionResult ThongTinTaiKhoan(Shipper tv)
        {
            try
            {

                Shipper tvUpdate = db.Shippers.Single(n => n.MaShipper == tv.MaShipper);
                tvUpdate.HoTen = tv.HoTen;
                tvUpdate.Email = tv.Email;
                tvUpdate.SDT = tv.SDT;
                tvUpdate.DangDiGiao = tv.DangDiGiao;
                db.SaveChanges();
                Session["HoTenShipper"] = tv.HoTen;
                return RedirectToAction("Index", "Shipper");
            }
            catch
            {
                return View(tv);
            }
        }
    }
}