//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyQuanTapHoa.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            this.ChiTietNhapKhoes = new HashSet<ChiTietNhapKho>();
        }
    
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<int> GiaNhap { get; set; }
        public Nullable<int> GiaBan { get; set; }
        public Nullable<int> SLBayBan { get; set; }
        public Nullable<int> SLTrongKho { get; set; }
        public Nullable<System.DateTime> HanSuDung { get; set; }
        public byte[] Image { get; set; }
        public int MaLoai { get; set; }
        public int MaDonViTinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietNhapKho> ChiTietNhapKhoes { get; set; }
        public virtual DonViTinh DonViTinh { get; set; }
        public virtual LoaiSanPham LoaiSanPham { get; set; }
    }
}
