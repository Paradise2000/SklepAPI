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
        public ActionResult AddProduct([FromBody] AddProductDto dto)
        {  
            _productService.AddProduct(dto);
            return Ok();
        }


    }
}
