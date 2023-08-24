
using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BulkyBookWeb.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly ApplicationDbContext _db;

        public CategoryController (ApplicationDbContext db, IHostingEnvironment host)
        {
            _db = db;
            _host = host;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objectCategory = _db.Categories;
            return View(objectCategory);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            
            if (ModelState.IsValid) 
            {
                string fileName = string.Empty;
                if( obj.clientFile != null)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "Images");
                    fileName = obj.clientFile.FileName;
                    string fullpath = Path.Combine(myUpload, fileName);
                    obj.clientFile.CopyTo(new FileStream(fullpath, FileMode.Create));
                    obj.imagePath = fileName;
                }
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "category created successfuly";
            return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
                
            }
            var categoryFormDb = _db.Categories.Find(id);
            if(categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                TempData["success"] = "category updated successfuly";
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var categoryFormDb = _db.Categories.Find(id);
            if (categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
                _db.Categories.Remove(obj);
                TempData["success"] = "category deleted successfuly";
               _db.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
