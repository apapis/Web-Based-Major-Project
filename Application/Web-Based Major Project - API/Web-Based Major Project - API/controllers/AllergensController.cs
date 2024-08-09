using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.controllers
{
    [Authorize]
    [Route("api/allergens")]
    public class AllergensController : ControllerBase
    {
        private readonly RestaurantContext _dbContext;

        public AllergensController(RestaurantContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/allergens
        [HttpGet]
        public ActionResult GetAllergens()
        {
            var allergens = _dbContext.Allergens.ToList();
            return Ok(allergens);
        }

        // GET: api/allergens/5
        [HttpGet("{id}")]
        public ActionResult<Allergen> GetAllergen(int id)
        {
            var allergen = _dbContext.Allergens.FirstOrDefault(a => a.Id == id);
            if (allergen == null)
            {
                return NotFound();
            }
            return Ok(allergen);
        }

        // POST: api/allergens
        [HttpPost]
        public ActionResult<Allergen> CreateAllergen([FromBody] Allergen allergen)
        {
            _dbContext.Allergens.Add(allergen);
            _dbContext.SaveChanges();
            return CreatedAtAction("GetAllergen", new { id = allergen.Id }, allergen);
        }

        // PUT: api/allergens/5
        [HttpPut("{id}")]
        public IActionResult UpdateAllergen(int id, [FromBody] Allergen allergen)
        {
            var existingAllergen = _dbContext.Allergens.FirstOrDefault(a => a.Id == id);
            if (existingAllergen == null)
            {
                return NotFound();
            }
            existingAllergen.Name = allergen.Name;
            _dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE: api/allergens/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAllergen(int id)
        {
            var allergen = _dbContext.Allergens.FirstOrDefault(a => a.Id == id);
            if (allergen == null)
            {
                return NotFound();
            }
            _dbContext.Allergens.Remove(allergen);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
