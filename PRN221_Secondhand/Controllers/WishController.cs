using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository;
using System;
using System.Linq;

namespace PRN221_Secondhand.Controllers
{
    public class WishController : Controller
    {
        WishRepository wishRepo = new WishRepository();
        public IActionResult Index()
        {

            var userid = HttpContext.Session.GetString("userid");
            if (userid != null)
            {
                var _listWish = wishRepo.GetAll().Include(p => p.Post.Category).Include(p => p.Post).Where(p => p.Status != 0).Where(p => p.UserId == userid);
                TempData["ERROR"] = "Get wishlist succsessfully";
                return View(_listWish);
            }
            return View();
        }
        public IActionResult Create(string id)
        {
            var userid = HttpContext.Session.GetString("userid");
            if (userid != null)
            {
                Wish wish = new Wish();
                Guid myuuid = Guid.NewGuid();
                wish.Id = myuuid.ToString();
                wish.Created = DateTime.Now;
                wish.UserId = userid;
                wish.Status = 1;
                wishRepo.Create(wish);
                TempData["ERROR"] = "Create wish succsessfully";
            }

            return RedirectToAction("Index", "Post");
        }
        public IActionResult Remove(string id)
        {
            Wish wish = wishRepo.Get(id);
            if (wish != null)
            {
                wish.Status = 0;
                wishRepo.Update(wish);
                TempData["ERROR"] = "Remove wish succsessfully";
            }
            return RedirectToAction("Index");
        }
    }
}
