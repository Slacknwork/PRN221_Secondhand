using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;
using System.Linq;

namespace PRN221_Secondhand.Controllers
{
    public class LoginController : Controller
    {
        UserRepository userRepository;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public IActionResult Login(string txtemail,string txtpass)
        {
            userRepository = new UserRepository();
            User user =  userRepository.GetAll().Where(a=> a.Email.Equals(txtemail)&&a.Password.Equals(txtpass)&&a.Status==1).FirstOrDefault();
            if (user == null) 
            {
                TempData["ERROR"] = "Wrong Email or Password";  
                return RedirectToAction("Index"); 
            }
            HttpContext.Session.SetString("userid", user.Id);
            HttpContext.Session.SetString("username", user.Name);
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        [ActionName("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["ERROR"] = "Logout succsessful";
            return RedirectToAction("Index");
        }
    }
}
