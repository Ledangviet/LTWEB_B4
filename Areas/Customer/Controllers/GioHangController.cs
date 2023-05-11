using LTWEB_B4.Data;
using LTWEB_B4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LTWEB_B4.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GioHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            GioHangViewModel giohang = new GioHangViewModel()
            {
                DSGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList(),
                HoaDon = new HoaDon(),
            };

            foreach(var item in giohang.DSGioHang)
            {
                item.ProductPrice = item.Quantity * item.SanPham.Price;
                giohang.HoaDon.Total += item.ProductPrice;
            }
            return View(giohang);
        }

        public IActionResult ThanhToan()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            GioHangViewModel giohang = new GioHangViewModel()
            {
                DSGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList(),
                HoaDon = new HoaDon(),
            };

            foreach (var item in giohang.DSGioHang)
            {
                item.ProductPrice = item.Quantity * item.SanPham.Price;
                giohang.HoaDon.Total += item.ProductPrice;
            }
            return View(giohang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(GioHangViewModel giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            giohang.DSGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList();
            giohang.HoaDon.ApplicationUserId = claim.Value;
            giohang.HoaDon.OrderDate = DateTime.Now;
            giohang.HoaDon.OrderStatus = "Dang Xac nhan";
            foreach(var item in giohang.DSGioHang)
            {
                item.ProductPrice = item.Quantity * item.SanPham.Price;
                giohang.HoaDon.Total += item.ProductPrice;
            }
            _db.HoaDon.Add(giohang.HoaDon);
            _db.SaveChanges();

            foreach(var item in giohang.DSGioHang)
            {
                ChiTietHoaDon chitiethoadon = new ChiTietHoaDon()
                {
                    SanPhamId = item.SanPhamId,
                    HoaDonId = giohang.HoaDon.Id,
                    ProductsPrice = item.ProductPrice,
                    Quantity = item.Quantity,
                };
                _db.ChiTietHoaDon.Add(chitiethoadon);
                _db.SaveChanges();
            }

            _db.GioHang.RemoveRange(giohang.DSGioHang);
            _db.SaveChanges();
            return RedirectToAction("ThanhToanThanhCong");

        }
        public IActionResult ThanhToanThanhCong()
        {
            return View();
        }
        public IActionResult Giam(int giohangId)
        {
            var giohang = _db.GioHang.Include("SanPham").FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity -= 1;

            if(giohang.Quantity == 0)
            {
                _db.GioHang.Remove(giohang);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Tang(int giohangId)
        {
            var giohang = _db.GioHang.Include("SanPham").FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity += 1;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Xoa(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            _db.GioHang.Remove(giohang);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
