using train1.Models;

namespace train1.Repository
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Delete(int id);
        List<Product> GetAll();
        Product GetById(int id);
        int Save();
        void Update(Product product);
    }
}