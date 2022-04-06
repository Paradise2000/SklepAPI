using SklepAPI.Entities;
using SklepAPI.Models;

namespace SklepAPI.Services
{
    public interface IProductService
    {
        void AddProduct(ProductDto dto);
        void UpdateProduct(int ProductId, ProductDto dto);
        IEnumerable<ProductDto> GetListOfProducts();
    }

    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddProduct(ProductDto dto)
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

        public void UpdateProduct(int ProductId, ProductDto dto)
        {
            var product = _context
                .Products
                .FirstOrDefault(r => r.Id == ProductId);

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.Description = dto.Description;
            product.ImagePath = dto.ImagePath;

            _context.SaveChanges();
        }

        public IEnumerable<ProductDto> GetListOfProducts()
        {
            var Products = _context.
                Products.
                OrderByDescending(r => r.Name).
                ToList();

            var ProductDto = Products.Select(r => new ProductDto()
            {
                Name=r.Name,
                Price=r.Price,
                Stock=r.Stock,
                Description=r.Description,
                ImagePath=r.ImagePath
            });

            return ProductDto;
        }
    }
}
