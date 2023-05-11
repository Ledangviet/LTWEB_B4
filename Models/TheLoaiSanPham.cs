using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTWEB_B4.Models
{
    public class TheLoaiSanPham
    {
        [Key]
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        [ForeignKey("SanPhamId")]
        [ValidateNever]
        public SanPham SanPham { get; set; }
        public int TheLoaiId { get; set; }
        [ForeignKey("TheLoaiId")]
        [ValidateNever]
        public TheLoaiModel TheLoai { get; set; }
    }
}
