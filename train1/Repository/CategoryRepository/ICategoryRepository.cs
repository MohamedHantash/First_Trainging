using train1.Models;

namespace train1.Repository
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Delete(int id);
        List<Category> GetAll();
        Category GetById(int id);
        int Save();
        void Update(Category category);
    }
}