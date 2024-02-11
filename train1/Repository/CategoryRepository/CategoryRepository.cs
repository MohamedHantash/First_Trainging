using train1.Models;

namespace train1.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;
        public CategoryRepository(ApplicationContext context)
        {
            this._context = context;
        }
        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }
        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
        public void Delete(int id)
        {
            _context.Categories.Remove(GetById(id));
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
       
    }
}
