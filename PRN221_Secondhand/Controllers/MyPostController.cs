using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Models;
using Repository.Repository;
using System;
using System.Dynamic;
using System.Linq;
using System.Text;
using X.PagedList;
namespace PRN221_Secondhand.Controllers
{
    public class MyPostController : Controller
    {
        PostRepository postRepository = new PostRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        private readonly ILogger<MyPostController> _logger;
        public MyPostController(ILogger<MyPostController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var listPost = postRepository.GetAll().Include(p => p.Category);
            return View(listPost.ToPagedList(pageNumber,pageSize));
        }
        public IActionResult createPost()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Posts = new Post();
            mymodel.Categories = categoryRepository.GetAll();
            return View(mymodel);
        }
        [HttpGet]
        public IActionResult editPost(string id)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Post = postRepository.GetAll()
                    .Where(p => p.Id == id).Include(p => p.Category).ToList(); 
            mymodel.Categories = categoryRepository.GetAll();
            //var post = postRepository.GetAll()
                    //.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
            return View(mymodel);
        }
        [HttpPost]
        public IActionResult createPost(Post post)
        {
            Guid myuuid = Guid.NewGuid();
            DateTime today = DateTime.Today;
            post.Id = myuuid.ToString();
            post.Created = today;
            post.Status = 1;
            postRepository.Create(post);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult editPost(Post post)
        {
            DateTime today = DateTime.Today;
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
                obj.Updated = today;
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
