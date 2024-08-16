using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.Services;

namespace Web_Based_Major_Project___API.Controllers
{
    [ApiController]
    [Route("api/unit")]
    public class UnitController : ControllerBase
    {
        private readonly UnitService _unitService;

        public UnitController(UnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUnits()
        {
            var units = await _unitService.GetUnitsAsync();
            return Ok(units);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            var unit = await _unitService.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit);
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateUnit([FromBody] Unit unit)
        {
            try
            {
                await _unitService.AddUnitAsync(unit);
                return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, [FromBody] Unit unit)
        {
            try
            {
                var existingUnit = await _unitService.GetUnitByIdAsync(id);
                if (existingUnit == null)
                {
                    return NotFound($"Unit with ID {id} not found.");
                }

                if (id != unit.Id)
                {
                    return BadRequest("ID in the URL does not match the ID in the request body.");
                }

                existingUnit.Name = unit.Name;
                existingUnit.Abbreviation = unit.Abbreviation;
                await _unitService.UpdateUnitAsync(existingUnit);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var unit = await _unitService.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            await _unitService.DeleteUnitAsync(id);
            return NoContent();
        }
    }
}