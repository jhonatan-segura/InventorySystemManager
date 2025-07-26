using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ITSenseAPI.Context;
using ITSenseAPI.DTOs;
using ITSenseAPI.Entities;
using ITSenseAPI.Interfaces.Repositories;

namespace ITSenseAPI.Controllers
{

   [ApiController]
   [Route("api/inventory-outputs")]
   [Authorize]
   public class InventoryOutputController(IInventoryOutputRepository productRepository) : ControllerBase
   {
      private readonly IInventoryOutputRepository _productRepository = productRepository;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<GetInventoryOutputDto>>> GetAll()
      {
         var inventoryOutputs = await _productRepository.GetAllActiveAsync();
         return Ok(inventoryOutputs);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<GetInventoryOutputDto>> GetById(int id)
      {
         var inventoryOutput = await _productRepository.GetByIdAsync(id);

         if (inventoryOutput == null)
            return NotFound();

         return Ok(inventoryOutput);
      }

      [HttpPost]
      public async Task<IActionResult> Create(InventoryOutputDto dto)
      {
         var id = await _productRepository.CreateAsync(dto);

         return CreatedAtAction(nameof(GetById), new { id }, new { id });
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update(int id, InventoryOutputDto dto)
      {
         var product = await _productRepository.UpdateAsync(id, dto);

         if (product is null)
         {
            return NotFound();
         }
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         var deleted = await _productRepository.SoftDeleteAsync(id);

         if (!deleted)
            return NotFound();

         return NoContent();
      }

   }
}