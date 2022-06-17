using blzrwasm_d.Server.Models;
using blzrwasm_d.Shared;
using MongoDB.Driver;

namespace blzrwasm_d.Server.Services
{
    public interface IProductService
    {
        List<Product> Get();
        Product? Get(string id);
        Product Create(Product Product);
        void Replace(string id, Product Product);
        void Update(string id, Product Product);
        void Remove(string id);
    }
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _Products;
        public static readonly List<Product> Products = new();
        public ProductService(IProductStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _Products = database.GetCollection<Product>(settings.CollectionName);
            foreach (var Product in _Products.Find(Product => true).ToList()) { Products.Add(Product); }
        }
        public Product Create(Product Product)
        {
            _Products.InsertOne(Product);
            return Product;
        }
        public List<Product> Get() { return _Products.Find(Product => true).ToList(); }
        public Product? Get(string id)
        {
            List<Product> Products = Get();
            Product? Product = Products.Where(e => e.Id == id).FirstOrDefault();
            return Product;
        }
        public void Remove(string id) { _Products.DeleteOne(Product => Product.Id == id); }
        public void Replace(string id, Product Product) { _Products.ReplaceOne(usr => usr.Id == id, Product); }
        public void Update(string id, Product Product) { _Products.ReplaceOne(usr => usr.Id == id, Product); }
    }
}
