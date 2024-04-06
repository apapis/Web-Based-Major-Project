using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly RestaurantContext _dbContext;

        public ProductController(RestaurantContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody] Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Created($"/api/products/{product.Id}", product);

        }

        [HttpGet]
        public ActionResult <IEnumerable<Product>> GetAllProducts() 
        {
            var products = _dbContext.Products.ToList();

            return Ok(products);
        }

        [HttpGet ("{id}")]
        public ActionResult<Product> GetProduct([FromRoute]int id) 
        { 
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product is null) 
            { 
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Store = updatedProduct.Store;
            product.weight = updatedProduct.weight;
            product.Price = updatedProduct.Price;
            product.PricePerGram = updatedProduct.PricePerGram;

            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return Ok(product); 
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return Ok(id);
        }
        
    }
}
