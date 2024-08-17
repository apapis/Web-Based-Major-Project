using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.DTO;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Controllers
{
    [Authorize]
    [Route("api/meals")]
    public class MealController : ControllerBase
    {
        private readonly MealService _mealService;

        public MealController(MealService mealService)
        {
            _mealService = mealService;
        }

        /*[HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> CreateMeal([FromForm] CreateMealDto mealDto)
        {
            try
            {
                var createdMeal = await _mealService.CreateMealAsync(mealDto);
                return CreatedAtAction(nameof(GetMealById), new { id = createdMeal.Id }, createdMeal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        /*[HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeal(int id, [FromForm] UpdateMealDto mealDto)
        {
            var updatedMeal = await _mealService.UpdateMealAsync(id, mealDto);
            if (updatedMeal == null)
            {
                return NotFound();
            }
            return Ok(updatedMeal);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetAllMeals()
        {
            var meals = await _mealService.GetAllMealsAsync();
            return Ok(meals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetMealById(int id)
        {
            var mealDto = await _mealService.GetMealByIdAsync(id);
            if (mealDto == null)
            {
                return NotFound();
            }
            return Ok(mealDto);
        }*/

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(int id)
        {
            var success = await _mealService.DeleteMealAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
