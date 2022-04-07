using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepAPI.Models;
using SklepAPI.Services;
using System.Security.Claims;

namespace SklepAPI.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = "User")]
        public ActionResult AddProduct([FromBody] ProductDto dto)
        {
            _productService.AddProduct(dto);
            return Ok();
        }

        [HttpPut("{ProductId}")]
        public ActionResult UpdateProduct([FromRoute] int ProductId, [FromBody] ProductDto dto)
        {
            _productService.UpdateProduct(ProductId, dto);
            return Ok();
        }

        [HttpGet("GetListOfProducts")]
        public ActionResult <IEnumerable<ProductDto>> GetListOfProducts()
        {
            var products = _productService.GetListOfProducts();
            return Ok(products);
        }
    }
}
