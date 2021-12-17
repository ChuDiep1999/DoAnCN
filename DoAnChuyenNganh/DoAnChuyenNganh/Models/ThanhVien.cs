﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnChuyenNganh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class ThanhVien
    {
        public int MaThanhVien { get; set; }
        [DisplayName("Tên Đăng Nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string TenDangNhap { get; set; }
        [DisplayName("Mật Khẩu")]
       
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Định dạng email không chính xác")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }
        [DisplayName("Câu Hỏi")]
        public string CauHoi { get; set; }
        [Required(ErrorMessage = "Câu trả lời không được để trống")]
        [DisplayName("Câu Trả Lời")]
        public string CauTraLoi { get; set; }
        public string NhapLaiMatKhau { get; set; }
        public string NhapLaiMatKhauMoi { get; set; }

        public string MatKhauMoi { get; set; }
        public string MatKhauCu { get; set; }
        public Nullable<int> MaLoaiThanhVien { get; set; }
        public string AnhDaiDien { get; set; }
    
        public virtual LoaiThanhVien LoaiThanhVien { get; set; }
    }
}
