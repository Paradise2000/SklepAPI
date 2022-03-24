using SklepAPI.Entities;
using SklepAPI.Models;

namespace SklepAPI.Services
{
    public interface IProductService
    {
        void AddProduct(AddProductDto dto);
    }

    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddProduct(AddProductDto dto)
        {
            var Product = new Product()
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description,
                ImagePath = dto.ImagePath
            };

            _context.Products.Add(Product);
            _context.SaveChanges();
        }
    }
}
