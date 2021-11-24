using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnChuyenNganh.Models
{
    public class ItemShipper
    {
        public string TenShipper { get; set; }
        public int MaShipper { get; set; }
        public string CaLam { get; set; }
        public ItemShipper(int iMaShipper)
        {
            using(Ship2hEntities db = new Ship2hEntities())
            {
                this.MaShipper = iMaShipper;
                Shipper sp = db.Shippers.Single(n => n.MaShipper == iMaShipper);
                this.CaLam = sp.CaLam.CaLam1;
                this.TenShipper = sp.HoTen;
            }    
        }
        public ItemShipper()
        { }
    }
}