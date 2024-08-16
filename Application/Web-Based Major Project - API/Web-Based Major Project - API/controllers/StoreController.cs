using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.Services;

namespace Web_Based_Major_Project___API.Controllers
{
    
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStores()
        {
            var stores = await _storeService.GetStoresAsync();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        [HttpPost]
        public async Task<ActionResult<Store>> CreateStore([FromBody] Store store)
        {
            try
            {
                await _storeService.AddStoreAsync(store);
                return CreatedAtAction("GetStore", new { id = store.Id }, store);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] Store store)
        {
            try
            {
                var existingStore = await _storeService.GetStoreByIdAsync(id);
                if (existingStore == null)
                {
                    return NotFound($"Allergen with ID {id} not found.");
                }

                // Upewnij się, że ID w ścieżce zgadza się z ID w ciele żądania
                if (id != store.Id)
                {
                    return BadRequest("ID in the URL does not match the ID in the request body.");
                }

                // Aktualizacja istniejącego alergenu
                existingStore.Name = store.Name;
                await _storeService.UpdateStoreAsync(existingStore);

                return NoContent();
            }
            catch (ValidationException ex)
            {
                // Obsługa błędów walidacji
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var allergen = await _storeService.GetStoreByIdAsync(id);
            if (allergen == null)
            {
                return NotFound();
            }

            await _storeService.DeleteStoreAsync(id);
            return NoContent();
        }
    }
}
