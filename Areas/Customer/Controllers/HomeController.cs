using LTWEB_B4.Data;
using LTWEB_B4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace LTWEB_B4.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;


        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include(sp => sp.TheLoaiModel).ToList();
            return View(sanpham);
        }

        public IActionResult Detail(int sanphamId)
        {
            GioHang giohang = new GioHang()
            {
                SanPhamId = sanphamId,
                SanPham = _db.SanPham.Include(sp => sp.TheLoaiModel).FirstOrDefault(sp => sp.Id == sanphamId),
                Quantity = 1,
            };
            ViewBag.DSHinhAnh = _db.HinhAnh.Include("SanPham").Where(ha => ha.SanPhamId == sanphamId).ToList();
            ViewBag.DSTheLoaiSP = _db.TheLoaiSanPham.Include("SanPham").Where(tl => tl.SanPhamId == sanphamId).ToList();

            return View(giohang);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Detail(GioHang giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);


            giohang.ApplicationUserId = claim.Value;

            var giohangFromDb = _db.GioHang.Where(gh => gh.ApplicationUserId == giohang.ApplicationUserId).FirstOrDefault(gh => gh.SanPhamId == giohang.SanPhamId);
            if(giohangFromDb == null)
            {
                _db.GioHang.Add(giohang);
            }
            else
            {
                giohangFromDb.Quantity += giohang.Quantity;
            }

            
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}