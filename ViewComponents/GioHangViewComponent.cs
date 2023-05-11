using LTWEB_B4.Data;
using LTWEB_B4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;


namespace LTWEB_B4.ViewComponents
{
    

    public class GioHangViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public GioHangViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim!=null)
            {
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
                ViewBag.GioHang = giohang;
                return View();
            }
            return View();
            
        }
    }
}
