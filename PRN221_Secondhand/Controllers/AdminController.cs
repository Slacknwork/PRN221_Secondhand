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
            var objUserList = _userRepository.GetAll();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(objUserList.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Post(string? userId, int? page)
        {   
            if(userId == null)
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
            if(userId == null)
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
