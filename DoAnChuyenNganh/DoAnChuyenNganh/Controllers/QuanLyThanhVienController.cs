using DoAnChuyenNganh.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class QuanLyThanhVienController : Controller
    {
        Ship2hEntities db = new Ship2hEntities();
        // GET: QuanLyThanhVien
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoan"] != null)
            {
                int PageSize = 9;
                int PageNumber = (page ?? 1);
                return View(db.ThanhViens.OrderBy(n => n.MaThanhVien).ToPagedList(PageNumber, PageSize));
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
                ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id);
                ViewBag.MaLoaiThanhVien = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien", tv.MaLoaiThanhVien);
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
        public ActionResult ChinhSua(ThanhVien tv)
        {

            ThanhVien tvUpdate = db.ThanhViens.Single(n => n.MaThanhVien == tv.MaThanhVien);

            tvUpdate.HoTen = tv.HoTen;
            tvUpdate.Email = tv.Email;
            tvUpdate.SDT = tv.SDT;
            tvUpdate.DiaChi = tv.DiaChi;
            tvUpdate.MaLoaiThanhVien = tv.MaLoaiThanhVien;
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
                ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id);
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
                ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id);
                if (tv.MaLoaiThanhVien==1)
                {
                    ViewBag.ThongBaoXoa = "Bạn không có quyền xóa Admin";
                    return View(tv);
                }
                db.ThanhViens.Remove(tv);
                db.SaveChanges();
                return RedirectToAction("Index");
        }
        public ActionResult ThanhVienChuaPhanLoai(int? page)
        {
            if (Session["TaiKhoan"] != null)
            {
                int PageSize = 9;
                int PageNumber = (page ?? 1);
                return View(db.ThanhViens.Where(n => n.MaLoaiThanhVien == null).OrderBy(n => n.MaThanhVien).ToPagedList(PageNumber, PageSize));
            }
            else
            {
                return RedirectToAction("DangNhap", "Home");
            }
        }
        [HttpGet]
        public ActionResult PhanLoai(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id&&n.MaLoaiThanhVien==null);
                ViewBag.MaLoaiThanhVien = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien", tv.MaLoaiThanhVien);
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
        public ActionResult PhanLoai(ThanhVien tv)
        {

            ThanhVien tvUpdate = db.ThanhViens.Single(n => n.MaThanhVien == tv.MaThanhVien);
            tvUpdate.MaLoaiThanhVien = tv.MaLoaiThanhVien;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}