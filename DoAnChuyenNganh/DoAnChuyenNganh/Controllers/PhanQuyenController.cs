using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class PhanQuyenController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        
        // GET: PhanQuyen
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.LoaiThanhViens.Where(n=>n.MaLoaiThanhVien!=7).OrderBy(n => n.TenLoaiThanhVien));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpGet]
        public ActionResult PhanQuyen(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                LoaiThanhVien ltv = db.LoaiThanhViens.SingleOrDefault(n => n.MaLoaiThanhVien == id);
                if (ltv == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MaQuyen = db.Quyens;
                ViewBag.LoaiTVQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLTV == id);
                return View(ltv);
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpPost]
        public ActionResult PhanQuyen(int? MaLTV, IEnumerable<LoaiThanhVien_Quyen> listPhanQuyen)
        {
            var listDaPhanQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLTV == MaLTV);
            if (listDaPhanQuyen.Count() != 0)
            {
                db.LoaiThanhVien_Quyen.RemoveRange(listDaPhanQuyen);
                db.SaveChanges();
            }
            if (listPhanQuyen != null)
            {
                foreach (var item in listPhanQuyen)
                {
                    item.MaLTV = int.Parse(MaLTV.ToString());
                    db.LoaiThanhVien_Quyen.Add(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}