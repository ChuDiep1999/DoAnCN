using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnChuyenNganh.Models.MetaData
{
    [MetadataTypeAttribute(typeof(ShipperMetadata))]
    public partial class Shipper
    {
        internal sealed class ShipperMetadata
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

            public int MaShipper { get; set; }
            public string HoTen { get; set; }
            public string SDT { get; set; }
            public string Email { get; set; }
            public Nullable<int> MaKho { get; set; }
            public Nullable<int> MaLoaiThanhVien { get; set; }
            public Nullable<int> MaCa { get; set; }
            public string DinhVi { get; set; }
            public string TenDangNhap { get; set; }
            public string MatKhau { get; set; }
            public Nullable<bool> DangDiGiao { get; set; }
            public string NhapLaiMatKhau { get; set; }
            public string NhapLaiMatKhauMoi { get; set; }

            public string MatKhauMoi { get; set; }
            public string MatKhauCu { get; set; }
           
        }
    }
}