using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTWEB_B4.Models
{
    public class HinhAnh
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int SanPhamId { get; set; }
        [ForeignKey("SanPhamId")]
        [ValidateNever]
        public SanPham SanPham { get; set; }

    }
}
