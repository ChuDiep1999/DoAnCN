using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class TaiKhoanController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        // GET: TaiKhoan
        public ActionResult ThongTinTaiKhoan()
        {
            if (Session["TaiKhoan"] != null)
            {
                int id = (int)Session["Id"];
                ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id);
                return View(tv);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpPost]
        public ActionResult ThongTinTaiKhoan(ThanhVien tv)
        {
            try
            {
                ThanhVien tvUpdate = db.ThanhViens.Single(n => n.MaThanhVien == tv.MaThanhVien);
                tvUpdate.HoTen = tv.HoTen;
                tvUpdate.Email = tv.Email;
                tvUpdate.SDT = tv.SDT;
                tvUpdate.DiaChi = tv.DiaChi;
                db.SaveChanges();
                Session["HoTen"] = tv.HoTen;
                return RedirectToAction("Index","Home");
            }
            catch
            {
                
                return View(tv);
            }
        }
    }
}