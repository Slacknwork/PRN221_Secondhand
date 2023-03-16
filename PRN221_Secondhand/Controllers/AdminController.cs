using Microsoft.AspNetCore.Mvc;
using PRN221_Secondhand.Models.Admin;
using Repository.Models;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PRN221_Secondhand.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserRepository _userRepository;
        public AdminController()
        {
            _userRepository = new UserRepository();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            IEnumerable<User> objUserList = _userRepository.GetAll();

            return View(objUserList);
        }

        public IActionResult Post()
        {
            return View();
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
            return View();
        }



    }
}
