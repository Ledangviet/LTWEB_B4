using LTWEB_B4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LTWEB_B4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TheLoaiModel> TheLoaiModel { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<GioHang> GioHang { get; set; }
        public DbSet<HinhAnh> HinhAnh { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<TheLoaiSanPham> TheLoaiSanPham { get; set; }

    }
}