using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> productRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand,string? type,
        string? sort)
        {
           var spec=new ProductSpecification(brand,type,sort);
           var products=await productRepository.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            productRepository.Add(product);
            if (await productRepository.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if(product.Id!=id || !ProductExists(id))
            {
                return BadRequest();
            }
             productRepository.Update(product);
            if (await productRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest();

        }   

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            productRepository.Remove(product);

           if (await productRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest();
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            var spec=new BrandListSpecification();
            var brands=await productRepository.ListAsync(spec);
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            var spec=new TypeListSpecification();
            var types=await productRepository.ListAsync(spec);
            return Ok(types);
        }

        private bool ProductExists(int id)
        {
            return productRepository.Exists(id);
        }        

    }
}
