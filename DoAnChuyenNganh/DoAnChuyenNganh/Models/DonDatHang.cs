//------------------------------------------------------------------------------
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
    
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            this.ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
        }
    
        public int MaDonDatHang { get; set; }
        public Nullable<System.DateTime> ThoiGianDat { get; set; }
        public Nullable<bool> TinhTrangGiaoHang { get; set; }
        public Nullable<bool> DaThanhToan { get; set; }
        public Nullable<int> MaShipper { get; set; }
        public string DinhViKhachHang { get; set; }
        public Nullable<double> ThoiGianGiao { get; set; }
        public Nullable<int> MaKho { get; set; }
        public string DiaChi { get; set; }
        public string HoTenKhachHang { get; set; }
        public string SDTKhachHang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
        public virtual Kho Kho { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
