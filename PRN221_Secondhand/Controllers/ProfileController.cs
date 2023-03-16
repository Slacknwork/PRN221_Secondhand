
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;

namespace PRN221_Secondhand.Controllers
{
    public class ProfileController : Controller
    {
        UserRepository userRepo = new UserRepository();
        [HttpGet]
        public IActionResult Index()
        {
            var userid = HttpContext.Session.GetString("userid");
            if (userid != null)
            {
                var user = userRepo.GetAll().Where(p => p.Id.Equals(userid)).FirstOrDefault();
                user.Password = null;
                //TempData["ERROR"] = "Get user's profile succsessfully";
                return View(user);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(User newUserData)
        {
            var userid = HttpContext.Session.GetString("userid");
            if (userid != null)
                if (newUserData.Id.Equals(userid))
                {
                    var user = userRepo.Get(userid);
                    if (user != null)
                    {
                        user.Email = newUserData.Email;
                        user.Avatar = newUserData.Avatar;
                        user.Name = newUserData.Name;
                        user.Phone = newUserData.Phone;

                        userRepo.Update(user); TempData["ERROR"] = "Edit user's profile succsessfully";
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
        public IActionResult ChangePassword(string oldPass, string newPass)
        {
            var userid = HttpContext.Session.GetString("userid");
            if (userid != null)
            {
                var user = userRepo.Get(userid);
                if (user.Password.Equals(oldPass))
                {
                    user.Password = newPass;

                    userRepo.Update(user); TempData["ERROR"] = "Change password succsessfully";
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string name, string avatar, string phone, string email, string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                User user = new User();
                Guid myuuid = Guid.NewGuid();
                user.Id = myuuid.ToString();
                user.Email = email;
                user.Password = password;
                user.Name = name;
                user.Avatar = avatar;
                user.Phone = phone;
                user.Status = 1;
                user.Created = DateTime.Now;
                user.Updated = DateTime.Now;

                userRepo.Create(user);
                TempData["ERROR"] = "Register succesfully!";
                //HttpContext.Session.SetString("userid", user.Id);
                //HttpContext.Session.SetString("username", user.Name);
                return RedirectToAction("Index", "Login");
            }
            else TempData["ERROR"] = "Password and confirm password not match!";
            return RedirectToAction("Index", "Home"); ;
        }
    }
}
