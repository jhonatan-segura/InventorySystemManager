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
   [Route("api/products")]
   [Authorize]
   public class ProductsController(IProductRepository productRepository) : ControllerBase
   {
      private readonly IProductRepository _productRepository = productRepository;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAll()
      {
         var products = await _productRepository.GetAllActiveAsync();
         return Ok(products);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<GetProductDto>> GetById(int id)
      {
         var product = await _productRepository.GetByIdAsync(id);

         if (product == null)
            return NotFound();

         return Ok(product);
      }

      [HttpPost]
      public async Task<IActionResult> Create(ProductDto dto)
      {
         var product = await _productRepository.CreateAsync(dto);

         return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update(int id, ProductDto dto)
      {
         var product = await _productRepository.UpdateAsync(id, dto);

         if (product is null)
         {
            return NotFound();
         }

         return NoContent();
      }

      [HttpPatch("set-as-faulty/{id}")]
      public async Task<IActionResult> SetAsFaulty(int id)
      {
         var updated = await _productRepository.SetAsFaultyAsync(id);

         if (!updated)
            return NotFound("No se pudo cambiar el estado del producto a 'Defectuoso'");

         return Ok(new { message = "Estado cambiado a 'Defectuoso'" });
      }

      [HttpPost("move-to-output")]
      public async Task<IActionResult> MoveToOutput([FromBody] MoveProductDto dto)
      {
         var (success, message) = await _productRepository.MoveToOutputAsync(dto);

         if (!success)
            return BadRequest(message);

         return Ok(new { message });
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