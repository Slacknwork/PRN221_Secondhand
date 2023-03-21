using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Secondhand.Models.Admin;
using Repository.Models;
using Repository.Repository;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using X.PagedList;

namespace PRN221_Secondhand.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;
        public AdminController()
        {
            _userRepository = new UserRepository();
            _postRepository = new PostRepository();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users(int? page)
        {
            if(HttpContext.Session.GetString("adminid")==null)
                return RedirectToAction("Login");
            var objUserList = _userRepository.GetAll();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(objUserList.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Post(string? userId, int? page)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            if (userId == null)
            {
                return NotFound();
            }
            User user = _userRepository.GetAll().Include(p => p.Posts).Where(u => u.Id.Equals(userId)).FirstOrDefault();
            var objUserList = _postRepository.GetAll().Include(p => p.User).Include(p => p.Category).Where(p => p.UserId == userId);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(objUserList.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult HidenPost(string? postId, string userId)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            if (postId == null)
            {
                return NotFound();
            }
            var post = _postRepository.Get(postId);
            if (post.Status == 0)
            {
                post.Status = 1;
            }
            else
            {
                post.Status = 0;
            }
            _postRepository.Update(post);
            return Redirect("/Admin/Post?userId=" + userId);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public IActionResult Login(string txtemail,string txtpass)
        {
            AdminRepository adminRepository= new AdminRepository();
            Admin user = adminRepository.GetAll().Where(a => a.Username.Equals(txtemail) 
                && a.Password.Equals(txtpass) && a.Status==1).FirstOrDefault();
            if (user == null)
            {
                TempData["ERROR"] = "Wrong Email or Password";
                return RedirectToAction("Index");
            }
            HttpContext.Session.SetString("adminid", user.Id);
            HttpContext.Session.SetString("username", user.Name);
            return RedirectToAction("Users");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(PasswordValidate newAdmin)
        {
            return View();
        }

        [HttpGet]
        [ActionName("BanUser")]
        public ActionResult BanUser(string? userId)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            if (userId == null)
            {
                return NotFound();
            }
            var user = _userRepository.Get(userId);
            if(user.Status == 0)
            {   
                user.Status = 1;
            }
            else
            {
                user.Status = 0;
            }
            _userRepository.Update(user);
            return RedirectToAction("Users");
        }



    }
}
