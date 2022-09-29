using LTWEB_B4.Data;
using LTWEB_B4.Models;
using Microsoft.AspNetCore.Mvc;

namespace LTWEB_B4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db; //Chua thong tin co so du lieu
        }
        public IActionResult Index()
        {
            var theloai = _db.TheLoaiModel;
            ViewBag.TheLoai = theloai.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TheLoaiModel theLoaiModel)
        {
            if (ModelState.IsValid)
            {
                _db.TheLoaiModel.Add(theLoaiModel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theLoaiModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theLoai = _db.TheLoaiModel.Find(id);
            return View(theLoai);
        }
        [HttpPost]
        public IActionResult Edit(TheLoaiModel theloai)
        {
            if (ModelState.IsValid)
            {
                _db.TheLoaiModel.Update(theloai);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoaiModel.Find(id);
            return View(theloai);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var theloai = _db.TheLoaiModel.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            _db.TheLoaiModel.Remove(theloai);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoaiModel.Find(id);
            ViewBag.TenTheLoai = theloai.Name;
            ViewBag.DateCreated = theloai.DateCreated;
            return View(theloai);

        }
    }
}
