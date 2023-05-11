using LTWEB_B4.Data;
using LTWEB_B4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LTWEB_B4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SanPhamController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            IEnumerable<SanPham> sanpham = _db.SanPham.ToList();

            return View(sanpham);
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            SanPham sanpham = new SanPham();
            IEnumerable<SelectListItem> dstheloai = _db.TheLoaiModel.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            ViewBag.DSTheLoai = dstheloai;
            if(id==0)
            {
                return View(sanpham);
            }
            else
            {
                sanpham = _db.SanPham.Include("TheLoaiModel").FirstOrDefault(sp => sp.Id == id);
                return View(sanpham);
            }
        }
        [HttpPost]
        public IActionResult Upsert(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                if (sanpham.Id==0)
                {
                    _db.SanPham.Add(sanpham);
                }
                else
                {
                    _db.SanPham.Update(sanpham);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View();
                
        }
        public IActionResult Delete(int id)
        {
            var sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }
            _db.SanPham.Remove(sanpham);
            _db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult AddImage(int id)
        {
            SanPham sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
            return View(sanpham);
        }
        [HttpPost]
        public IActionResult AddImage(IFormFile File1, int id)
        {
            var sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
            if (File1 != null)
            {
                string FileName = "product-"+sanpham.Id+".png";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", FileName);

                var stream = new FileStream(path, FileMode.Create);

                File1.CopyToAsync(stream);

                string url = "~/images/" + FileName;
                sanpham.ImageUrl = url;
                _db.SanPham.Update(sanpham);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddImage2(int id)
        {
            SanPham sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
            return View(sanpham);
        }
        [HttpPost]
        public IActionResult AddImage2(IFormFile File1, int id)
        {
            var sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
            HinhAnh hinhanh = new HinhAnh();
            hinhanh.SanPham = sanpham;
            hinhanh.SanPhamId = sanpham.Id;
            if (File1 != null)
            {
                string FileName = "product-"+ sanpham.Id + _db.HinhAnh.Count() + ".png";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/detail", FileName);

                var stream = new FileStream(path, FileMode.Create);

                File1.CopyToAsync(stream);

                string url = "~/images/detail/" + FileName;
                hinhanh.ImageUrl = url;
                _db.HinhAnh.Add(hinhanh);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }       
        public IActionResult RemoveImg(int id)
        {
            IEnumerable < HinhAnh > ds = _db.HinhAnh.Include("SanPham").Where(ha => ha.SanPhamId == id);
            if (ds!=null)
            {
                foreach (HinhAnh h in ds)
                {
                    _db.Remove(h);
                }
                _db.SaveChanges();
            }
            
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddTheLoai(int id)
        {
            TheLoaiSanPham theLoaiSanPham = new TheLoaiSanPham() { SanPhamId = id, };
            IEnumerable<SelectListItem> dstheloai = _db.TheLoaiModel.Select(
               item => new SelectListItem
               {
                   Value = item.Id.ToString(),
                   Text = item.Name,
               });
            ViewBag.DSTheLoai = dstheloai;
            return View(theLoaiSanPham) ;

        }
        [HttpPost]
        public IActionResult AddTheLoai(TheLoaiSanPham theLoaiSanPham)
        {
            theLoaiSanPham.TheLoai = _db.TheLoaiModel.FirstOrDefault(tl => tl.Id == theLoaiSanPham.TheLoaiId);
            _db.TheLoaiSanPham.Add(theLoaiSanPham);
            _db.SaveChanges();
            return View();
        }
    }
}
