using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnChuyenNganh.Models.MetaData
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            public int MaThanhVien { get; set; }
            [DisplayName("Tên Đăng Nhập")]
            [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
            public string TenDangNhap { get; set; }
            [DisplayName("Mật Khẩu")]
            [Required(ErrorMessage = "Mật khẩu không được để trống")]
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

          
        }
    }
}