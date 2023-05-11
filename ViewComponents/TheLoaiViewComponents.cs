using LTWEB_B4.Data;
using Microsoft.AspNetCore.Mvc;

namespace LTWEB_B4.ViewComponents
{
    public class TheLoaiViewComponents : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public TheLoaiViewComponents(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var theloai = _db.TheLoaiModel.ToList();
            return View(theloai);
        }
    }
}
