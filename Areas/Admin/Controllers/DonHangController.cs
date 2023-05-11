using LTWEB_B4.Data;
using LTWEB_B4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTWEB_B4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DonHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<HoaDon> hoadon = _db.HoaDon.Include("ApplicationUser").ToList();
            return View(hoadon);
        }
        public IActionResult ChiTiet(int id)
        {
            IEnumerable<ChiTietHoaDon> chitiethoadon = _db.ChiTietHoaDon.Include("SanPham").Where(ct => ct.HoaDonId
            == id);
            return View(chitiethoadon);
        }
    }
}
