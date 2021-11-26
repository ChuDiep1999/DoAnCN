using CaptchaMvc.HtmlHelpers;
using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap");
            }

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
                Session["TaiKhoan"] = tv;
                Session["HoTen"] = data.FirstOrDefault().HoTen;
                Session["Id"]= data.FirstOrDefault().MaThanhVien;
                Session["LoaiThanhVien"] = data.FirstOrDefault().LoaiThanhVien;
                return RedirectToAction("Index","Home");
                
            }
            ViewBag.ThongBaoDangNhap = "Thông tin đăng nhập không chính xác";
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