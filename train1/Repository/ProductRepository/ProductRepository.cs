using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using train1.Models;

namespace train1.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            this._context = context;
        }
        public List<Product> GetAll()
        {
            return _context.Products.Include(p=>p.Category).ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products.Include(p=>p.Category).FirstOrDefault(p => p.Id == id);
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public void Delete(int id)
        {
            _context.Products.Remove(GetById(id));
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
