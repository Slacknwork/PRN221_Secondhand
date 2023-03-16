using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;
using System.Linq;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;

namespace PRN221_Secondhand.Controllers
{
    public class ProfileController : Controller
    {
        UserRepository userRepo = new UserRepository();
        [HttpGet]
        public IActionResult Index(HttpContext context)
        {
            var session = context.Session;
            User user = null;
            if (session != null)
            {
                var userid = session.GetString("user_id");
                if (userid != null)
                {
                    user = userRepo.GetAll().Where(p=>p.Id.Equals(userid)).FirstOrDefault();
                    user.Password = null;
                }
            }
            return View(user);
        }       
        [HttpPost]
        public IActionResult Edit(User newUserData, HttpContext context)
        {
            var session = context.Session;

            var contextid = session.GetString("user_id");
            if (newUserData.Id.Equals(contextid))
            {
                var user = userRepo.Get(contextid);
                if (user != null)
                {
                    user.Email = newUserData.Email;
                    user.Avatar = newUserData.Avatar;
                    user.Name = newUserData.Name;
                    user.Phone= newUserData.Phone;

                    userRepo.Update(user);
                }
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(HttpContext context, string oldPass, string newPass)
        {
            var session = context.Session;

            var contextid = session.GetString("user_id");
            if (contextid != null)
            {
                var user = userRepo.Get(contextid);
                if (user != null)
                {
                    if (user.Password.Equals(oldPass))
                    {
                        user.Password = newPass;
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
