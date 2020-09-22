using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WishList.Data;
using WishList.Models;

namespace WishList.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Index", _context.Items.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", item);
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var item = _context.Items.SingleOrDefault(i => i.Id == Id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
