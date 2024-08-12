using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.Services;

namespace Web_Based_Major_Project___API.Controllers
{
    [Authorize]
    [Route("api/allergens")]
    public class AllergensController : ControllerBase
    {
        private readonly AllergenService _allergenService;

        public AllergensController(AllergenService allergenService)
        {
            _allergenService = allergenService;
        }

        // GET: api/allergens
        [HttpGet]
        public ActionResult GetAllergens()
        {
            var allergens = _allergenService.GetAllergens();
            return Ok(allergens);
        }

        // GET: api/allergens/5
        [HttpGet("{id}")]
        public ActionResult<Allergen> GetAllergen(int id)
        {
            var allergen = _allergenService.GetAllergenById(id);
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
            _allergenService.AddAllergen(allergen);
            return CreatedAtAction("GetAllergen", new { id = allergen.Id }, allergen);
        }

        // PUT: api/allergens/5
        [HttpPut("{id}")]
        public IActionResult UpdateAllergen(int id, [FromBody] Allergen allergen)
        {
            var existingAllergen = _allergenService.GetAllergenById(id);
            if (existingAllergen == null)
            {
                return NotFound();
            }
            _allergenService.UpdateAllergen(existingAllergen, allergen);
            return NoContent();
        }

        // DELETE: api/allergens/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAllergen(int id)
        {
            var allergen = _allergenService.GetAllergenById(id);
            if (allergen == null)
            {
                return NotFound();
            }
            _allergenService.DeleteAllergen(allergen);
            return NoContent();
        }
    }
}
