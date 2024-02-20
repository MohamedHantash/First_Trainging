using Microsoft.AspNetCore.Mvc;
using train1.Repository;
using train1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace train1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository,
                 ICategoryRepository categoryRepository,
                 Microsoft.AspNetCore.Hosting.IHostingEnvironment environment )
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
          
        }
        public IActionResult GetAll()
        {
            List<Product> products = _productRepository.GetAll();
            return View(products);
        }
        public IActionResult Details(int id)
        {
            Product product= _productRepository.GetById(id);
            return View(product);
        }
        public IActionResult Add()
        {
            var list = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            ViewData["listCategory"] = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product product)
        {
            var file = HttpContext.Request.Form.Files;
            if(file.Count()>0)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "images", imageName),FileMode.Create);
                file[0].CopyTo(filestream);
                // To Save In DB . 
                product.ImageURl = imageName; 
            }
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                _productRepository.Save();
                return RedirectToAction("GetAll");
            }
            var list = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            ViewData["listCategory"] = list;
            return View(product);
        }
        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetById(id);
            if(product != null)
            {
                var list = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
                ViewData["listCategory"] = list;
                return View(product);
            }
            return RedirectToAction("GetAll");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            var file=HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream =new FileStream(Path.Combine(@"wwwroot/","images",imageName), FileMode.Create);
                file[0].CopyTo(filestream);
                product.ImageURl = imageName;
            }
            if(ModelState.IsValid)
            {
                _productRepository.Update(product);
                _productRepository.Save();
                return RedirectToAction("GetAll");
            }
            var list = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            ViewData["listCategory"] = list;
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.GetById(id);
            if(product != null)
            {
                _productRepository.Delete(id);
                _productRepository.Save();
            }
            return RedirectToAction("GetAll");
        }
    }
}
