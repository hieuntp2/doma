//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace doma.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonHang
    {
        public DonHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
    
        public int ID { get; set; }
        public string UserID { get; set; }
        public string DiaChiGiao { get; set; }
        public System.DateTime ThoiGianGiao { get; set; }
        public short TinhTrang { get; set; }
        public string SoDienThoai { get; set; }
        public System.DateTime NgayTao { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
