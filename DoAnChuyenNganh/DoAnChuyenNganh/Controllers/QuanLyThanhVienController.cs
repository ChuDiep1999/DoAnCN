using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class QuanLyThanhVienController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        // GET: QuanLyThanhVien
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.ThanhViens.OrderBy(n => n.MaThanhVien));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
    }
}