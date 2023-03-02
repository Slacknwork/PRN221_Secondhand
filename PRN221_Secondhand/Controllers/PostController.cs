using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Models;
using Repository.Repository;
using System;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace PRN221_Secondhand.Controllers
{
    public class PostController : Controller
    {
        PostRepository postRepository = new PostRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        private readonly ILogger<PostController> _logger;
        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var listPost = postRepository.GetAll().Include(p => p.Category);
            return View(listPost);
        }
        public IActionResult createPost()
        {
            var category = categoryRepository.GetAll();
            return View(new Post());
        }
        [HttpGet]
        public IActionResult editPost(string id)
        {
            var post = postRepository.GetAll()
                    .Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
            return View(post);
        }
        [HttpPost]
        public IActionResult createPost(Post post)
        {
            postRepository.Create(post);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult editPost(Post post)
        {
            var obj = postRepository.GetAll()
                    .Where(p => p.Id == post.Id).FirstOrDefault();
            if (obj != null)
            {
                obj.Id = post.Id;
                obj.Name = post.Name;
                obj.Image = post.Image;
                obj.Description = post.Description;
                obj.CategoryId = post.CategoryId;
                obj.UserId = post.UserId;
                obj.Price = post.Price;
                obj.Created = post.Created;
                obj.Updated = post.Updated;
                obj.Status = post.Status;
                postRepository.Update(obj);
            }
            return RedirectToAction("Index");
        }
        public IActionResult deletePost(string id)
        {
            var obj = postRepository.Get(id);
            if (obj != null)
            {
                obj.Status = 0;
                postRepository.Update(obj);
            }
            return RedirectToAction("Index");
        }
    }
}
