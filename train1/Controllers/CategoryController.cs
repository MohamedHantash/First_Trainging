using Microsoft.AspNetCore.Mvc;
using train1.Models;
using train1.Repository;

namespace train1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public IActionResult GetAll()
        {
            List<Category> categories = _categoryRepository.GetAll();
            return View(categories);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                return RedirectToAction("GetAll");
            }
            return View(category);
        }
        public IActionResult Edit(int id)
        {
            Category category= _categoryRepository.GetById(id);
            if(category != null)
            {
                return View(category);
            }
            return RedirectToAction("GetAll");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category) 
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                return RedirectToAction("GetAll");
            }
            return View(category);
        }
        public IActionResult Delete(int id) 
        {
            Category category = _categoryRepository.GetById(id);
            if(category != null)
            {
                _categoryRepository.Delete(id);
                _categoryRepository.Save();
            }
            return RedirectToAction("GetAll");
        }
    }
}
