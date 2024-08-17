using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.Models;
using Web_Based_Major_Project___API.Services;

namespace Web_Based_Major_Project___API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while retrieving products.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] Product product)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the product data.");
            }
            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(product);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok(updatedProduct);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }
    }
}