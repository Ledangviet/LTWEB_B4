using System.ComponentModel.DataAnnotations;

namespace LTWEB_B4.Models
{
    public class TheLoaiModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Không được để trống tên thể loại!")]
        [Display(Name ="Thể loại")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Không đúng định dạng ngày!")]
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
