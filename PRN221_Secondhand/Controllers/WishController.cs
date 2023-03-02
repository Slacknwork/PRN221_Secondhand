using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository;
using System.Linq;

namespace PRN221_Secondhand.Controllers
{
    public class WishController : Controller
    {
        WishRepository wishRepo = new WishRepository();
        public IActionResult Index()
        {
            var _listWish = wishRepo.GetAll().Include(p => p.Post.Category).Include(p => p.Post).Where(p => p.Status != 0);
            return View(_listWish);
        }
        public IActionResult Remove(string id)
        {
            Wish wish = wishRepo.Get(id);
            if (wish != null)
            {
            wish.Status = 0;
            wishRepo.Update(wish);
            }
            return RedirectToAction("Index");
        }
    }
}
