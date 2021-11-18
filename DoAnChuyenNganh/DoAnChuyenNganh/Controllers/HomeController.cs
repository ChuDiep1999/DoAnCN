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
        DataDoAnChuyenNganhEntities db = new DataDoAnChuyenNganhEntities();
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
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
                return View("Index");
                
            }
            ViewBag.ThongBaoDangNhap = "Thông tin đăng nhập không chính xác";
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhap");
        }
    }
   

}