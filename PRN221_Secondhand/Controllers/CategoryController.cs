using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using Repository.Models;
using System;
using System.Linq;

namespace PRN221_Secondhand.Controllers
{
    public class CategoryController : Controller
    {
        readonly CategoryRepository categoryRepository = new();
        // GET: CategoryController
        public ActionResult Index()
        {
            var list = categoryRepository.GetAll().Where(category => category.Status != 0).ToList();
            return View(list);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(string id)
        {
            var category = categoryRepository.Get(id);
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                category.Id = Guid.NewGuid().ToString();

                categoryRepository.Create(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(string id)
        {
            var category = categoryRepository.Get(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Category category)
        {
            try
            {
                var categoryEntity = categoryRepository.Get(id);
                categoryEntity.Image = category.Image;
                categoryEntity.Description = category.Description;
                categoryEntity.Name = category.Name;

                categoryRepository.Update(categoryEntity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(string id)
        {
            var category = categoryRepository.Get(id);
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var categoryEntity = categoryRepository.Get(id);

                categoryEntity.Status = 0;
                categoryRepository.Update(categoryEntity);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
