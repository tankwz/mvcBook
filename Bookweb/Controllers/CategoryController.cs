using Bookweb.Data;
using Bookweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories;
            return View(objCategoryList);
        }

        //get
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display cant match name.");
            }
            if(ModelState.IsValid) { 
            _context.Categories.Add(obj);
            _context.SaveChanges();
                TempData["success"] = "created good";
            return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if(id == null || id==0)
                return NotFound();
            var categoryFromDb = _context.Categories.Find(id);
            
            if(categoryFromDb == null)
                return NotFound();


            return View(categoryFromDb);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display cant match name.");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();

                TempData["success"] = "edit good";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryFromDb = _context.Categories.Find(id);

            if (categoryFromDb == null)
                return NotFound();


            return View(categoryFromDb);
        }
        //post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
         var obj = _context.Categories.Find(id);
            if (obj == null) return NotFound();
            _context.Categories.Remove(obj);

            TempData["success"] = "remove good";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
         
        

    }
}
 