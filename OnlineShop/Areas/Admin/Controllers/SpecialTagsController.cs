using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Super user")]
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var data = _db.SpecialTags.ToList();
            return View(data);
        }

        // Create Get Action
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTag);
                await _db.SaveChangesAsync();

                TempData["save"] = "Special Tag has been saved successfully!";
                TempData["typOperation"] = "save";

                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        // Edit Get Action
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.Update(specialTag);
                await _db.SaveChangesAsync();

                TempData["update"] = "Special Tag has been updated successfully!";
                TempData["typOperation"] = "update";

                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        // Details Get Action
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag specialTag)
        {

            return RedirectToAction(nameof(Index));
        }

        // Delete Get Action
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTag specialTags)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != specialTags.Id)
            {
                return NotFound();
            }

            var specialTag = _db.SpecialTags.Find(id);

            if (specialTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(specialTags);
                await _db.SaveChangesAsync();

                TempData["message"] = "Special tag has been deleted successfully!";
                TempData["typOperation"] = "delete";

                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

    }
}
