using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using Repository.Models;
using System;
using System.Linq;

namespace PRN221_Secondhand.Controllers
{
    public class TagController : Controller
    {
        readonly TagRepository tagRepository = new();
        // GET: TagController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login","Admin");
            var list = tagRepository.GetAll().Where(tag => tag.Status != 0).ToList();
            return View(list);
        }

        // GET: TagController/Details/5
        public ActionResult Details(string id)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            var tag = tagRepository.Get(id);
            return View(tag);
        }

        // GET: TagController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            try
            {
                tag.Id = Guid.NewGuid().ToString();
                tagRepository.Create(tag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagController/Edit/5
        public ActionResult Edit(string id)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            var tag = tagRepository.Get(id);
            return View(tag);
        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Tag tag)
        {
            try
            {
                if (HttpContext.Session.GetString("adminid") == null)
                    return RedirectToAction("Login", "Admin");
                var tagEntity = tagRepository.Get(id);
                tagEntity.Value = tag.Value;
                tagEntity.Type = tag.Type;

                tagRepository.Update(tagEntity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagController/Delete/5
        public ActionResult Delete(string id)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            var tag = tagRepository.Get(id);
            return View(tag);
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            if (HttpContext.Session.GetString("adminid") == null)
                return RedirectToAction("Login", "Admin");
            try
            {
                var tagEntity = tagRepository.Get(id);
                tagEntity.Status = 0;
                tagRepository.Update(tagEntity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
