using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;

namespace PRN221_Secondhand.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index(string id)
        {
            PostRepository postRepository = new PostRepository();
            Post p = (Post)postRepository.Get(id)  ;
            if(p!= null && p.Status == 1)
                return View(p);
            return View();
        }
    }
}
