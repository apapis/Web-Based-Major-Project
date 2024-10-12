using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.DTO;
using Web_Based_Major_Project___API.Entities.Meal;
using Web_Based_Major_Project___API.Services;

namespace Web_Based_Major_Project___API.Controllers
{
    [ApiController]
    [Route("api/meals")]
    public class MealController : ControllerBase
    {
        private readonly MealService _mealService;

        public MealController(MealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetAllMeals()
        {
            try
            {
                var meals = await _mealService.GetAllMealsAsync();
                return Ok(meals);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while retrieving meals.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMealById(int id)
        {
            try
            {
                var meal = await _mealService.GetMealByIdAsync(id);
                if (meal == null)
                {
                    return NotFound();
                }
                return Ok(meal);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while retrieving the meal.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Meal>> CreateMeal([FromBody] MealDTO mealDto)
        {
            Console.WriteLine("Hello");
            try
            {
                var createdMealId = await _mealService.CreateMealAsync(mealDto);
                var createdMeal = await _mealService.GetMealByIdAsync(createdMealId);
                return CreatedAtAction(nameof(GetMealById), new { id = createdMealId }, createdMeal);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, $"An error occurred while creating the meal. Exception: {ex.Message} Stack Trace: {ex.StackTrace}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Meal>> UpdateMeal(int id, [FromBody] MealDTO mealDto)
        {
            try
            {
                if (id != mealDto.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the meal data.");
                }
                var success = await _mealService.UpdateMealAsync(id, mealDto);
                if (!success)
                {
                    return NotFound();
                }
                var updatedMeal = await _mealService.GetMealByIdAsync(id);
                return Ok(updatedMeal);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred while creating the meal. Exception: {ex.Message} Stack Trace: {ex.StackTrace}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(int id)
        {
            try
            {
                var success = await _mealService.DeleteMealAsync(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred while creating the meal. Exception: {ex.Message} Stack Trace: {ex.StackTrace}");
            }
        }
    }
}