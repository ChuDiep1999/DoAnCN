using CaptchaMvc.HtmlHelpers;
using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DoAnChuyenNganh.Controllers
{
    public class HomeController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString(); //lay so luong nguoi truy cap
                ViewBag.SoLuongNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString(); //lay so luong nguoi truy cap
                ViewBag.TongDDH = ThongKeDonHang(); //Thong Ke DonHang
                ViewBag.TongThanhVien = ThongKeThanhVien();//Thong ke thanh vien
                ViewBag.TongShipper = ThongKeShipper();
                ViewBag.TongKho = ThongKeKho();
                ViewBag.SLDDHPass = ThongKeDonDatHangDaPass();
                ViewBag.PhanTramDDH = PhanTramDonDatHang();
                ViewBag.ShipperDG = ThongKeShipperDangGiao();
                ViewBag.PhanTramShipper = PhanTramShipper();
                ViewBag.ThongKeThanhVienMoiDangKy = ThongKeThanhVienMoiDangKy();
                ViewBag.PhanTramThanhVien = PhanTramThanhVien();
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap");
            }

        }
        public double PhanTramThanhVien()
        {
            double tv = db.Shippers.Where(n => n.LoaiThanhVien == null).Count();
            double tv1 = db.Shippers.Count();
            double phantram = (tv / tv1) * 100;
            return phantram;
        }
        public double ThongKeThanhVienMoiDangKy()
        {

            double tv = db.ThanhViens.Where(n => n.LoaiThanhVien == null).Count();
            return tv;
        }
        public double PhanTramShipper()
        {
            double sp = db.Shippers.Where(n => n.DangDiGiao == true).Count();
            double sp1 = db.Shippers.Count();
            double phantram = (sp / sp1) * 100;
            return phantram;
        }
        public double PhanTramDonDatHang()
        {
            double ddh = db.DonDatHangs.Where(n => n.DaThanhToan == true && n.TinhTrangGiaoHang == true).Count();
            double ddh1 = db.DonDatHangs.Count();
            double phantram = (ddh / ddh1) * 100;
            return phantram;
        }
        public double ThongKeDonDatHangDaPass()
        {

            double ddh = db.DonDatHangs.Where(n=>n.DaThanhToan==true&&n.TinhTrangGiaoHang==true).Count();
            return ddh;
        }
        public double ThongKeShipperDangGiao()
        {

            double sp = db.Shippers.Where(n => n.DangDiGiao == true).Count();
            return sp;
        }
        public double ThongKeKho()
        {
            
            double kho = db.Khoes.Count();
            return kho;
        }
        public double ThongKeDonHang()
        {
            //Dem don dat hang
            double ddh = db.DonDatHangs.Count();
            return ddh;
        }
        public double ThongKeThanhVien()
        {
            
            double sltv = db.ThanhViens.Count();
            return sltv;
        }
        public double ThongKeShipper()
        {
            //Dem don dat hang
            double slsp = db.Shippers.Count();
            return slsp;
        }
        public ActionResult Index2()
        {
            if (Session["TaiKhoan"] != null)
            {
                var listSP = db.Shippers.Where(n=>n.DangDiGiao==true);
                return View(listSP);
            }
            else
            {
                return RedirectToAction("DangNhap");
            }

        }
        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, TaiKhoan, DateTime.Now, DateTime.Now.AddHours(3), false, Quyen, FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
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
            var data = db.ThanhViens.Where(s => s.TenDangNhap.Equals(taikhoan) && s.MatKhau.Equals(matkhau)).ToList();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TenDangNhap  == taikhoan && n.MatKhau == matkhau);
            if (tv != null)
            {
                var listQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLTV == tv.MaLoaiThanhVien);
                string Quyen = "";
                if (listQuyen.Count() != 0)
                {
                    foreach (var item in listQuyen)
                    {
                        Quyen += item.Quyen.MaQuyen + ",";
                    }
                    Quyen = Quyen.Substring(0, Quyen.Length - 1);
                    PhanQuyen(tv.TenDangNhap.ToString(), Quyen);
                }    
                    Session["TaiKhoan"] = tv;
                Session["HoTen"] = data.FirstOrDefault().HoTen;
                Session["Id"]= data.FirstOrDefault().MaThanhVien;
                Session["LoaiThanhVien"] = data.FirstOrDefault().LoaiThanhVien;
                return RedirectToAction("Index","Home");
                
            }
            ViewBag.ThongBaoDangNhap = "Thông tin đăng nhập không chính xác";
            return View();
        }
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhap");
        }

        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            // Kiểm tra capcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    var check = db.ThanhViens.FirstOrDefault(s => s.TenDangNhap == tv.TenDangNhap);
                    if(tv.MatKhau==tv.NhapLaiMatKhau){
                        if (check == null)
                        {
                            ViewBag.ThongBaoDangKy = "Đăng ký thành công";
                            //thêm thành viên
                            db.ThanhViens.Add(tv);
                            db.SaveChanges();
                            return View();
                        }
                        else
                        {
                            ViewBag.error = "Tài khoản đã tồn tại";
                            ViewBag.ThongBaoDangKy = "Đăng ký thất bại";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.error2 = "Mật khẩu không trùng";
                        ViewBag.ThongBaoDangKy = "Đăng ký thất bại";
                        return View();
                    }    

                }
                else
                {
                    ViewBag.ThongBaoDangKy = "Đăng ký thất bại";
                    return View();
                }

            }
            ViewBag.ThongBaoDangKy = "Sai mã Captcha";
            return View();
        }
        public List<string> LoadCauHoi()
        {
            List<string> listCauHoi = new List<string>();
            listCauHoi.Add("Đội bóng mà bạn yêu thích");
            listCauHoi.Add("Bạn yêu màu gì");
            listCauHoi.Add("Bạn đến từ đâu");
            return listCauHoi;
        }
        

    }

   

}