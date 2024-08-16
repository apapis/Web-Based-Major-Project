using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.Services;

namespace Web_Based_Major_Project___API.Controllers
{
    
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
        public async Task<ActionResult> GetAllergens()
        {
            var allergens = await _allergenService.GetAllergensAsync();
            return Ok(allergens);
        }

        // GET: api/allergens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Allergen>> GetAllergen(int id)
        {
            var allergen = await _allergenService.GetAllergenByIdAsync(id);
            if (allergen == null)
            {
                return NotFound();
            }
            return Ok(allergen);
        }

        // POST: api/allergens
        [HttpPost]
        public async Task<ActionResult<Allergen>> CreateAllergen([FromBody] Allergen allergen)
        {
            try
            {
                await _allergenService.AddAllergenAsync(allergen);
                return CreatedAtAction("GetAllergen", new { id = allergen.Id }, allergen);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/allergens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAllergen(int id, [FromBody] Allergen allergen)
        {
            try
            {
                var existingAllergen = await _allergenService.GetAllergenByIdAsync(id);
                if (existingAllergen == null)
                {
                    return NotFound($"Allergen with ID {id} not found.");
                }

                // Upewnij się, że ID w ścieżce zgadza się z ID w ciele żądania
                if (id != allergen.Id)
                {
                    return BadRequest("ID in the URL does not match the ID in the request body.");
                }

                // Aktualizacja istniejącego alergenu
                existingAllergen.Name = allergen.Name;
                await _allergenService.UpdateAllergenAsync(existingAllergen);

                return NoContent();
            }
            catch (ValidationException ex)
            {
                // Obsługa błędów walidacji
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/allergens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllergen(int id)
        {
            var allergen = await _allergenService.GetAllergenByIdAsync(id);
            if (allergen == null)
            {
                return NotFound();
            }

            await _allergenService.DeleteAllergenAsync(id);
            return NoContent();
        }
    }
}
