using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository;
using System.Linq;

namespace PRN221_Secondhand.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index(string id)
        {
            PostRepository postRepository = new PostRepository();
            Post p = (Post)postRepository.GetAll().Include(t => t.Category).Where(a=>a.Id.Equals(id)&&a.Status!=0).FirstOrDefault()  ;
            if(p!= null && p.Status == 1)
                return View(p);
            return View();
        }
    }
}
