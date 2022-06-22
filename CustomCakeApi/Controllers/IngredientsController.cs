#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomCakeApi.Models;
using CustomCakeApi.DTOs;
using CustomCakeApi.Mappers;

namespace CustomCakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly CustomCakeContext _context;

        public IngredientsController(CustomCakeContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetIngredients()
        {
            var query = _context.Ingredients.AsQueryable();

            return await query
            .Select(item => IngredientMappers.ItemToDTO(item))
            .ToListAsync();
        }

        // GET: api/Ingredients/5
        //get id
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDTO>> GetIngredient(long id)
        {
            var query = _context.Ingredients.AsQueryable();

            var ingredient = await query 
            .FirstOrDefaultAsync(ingredient => ingredient.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return IngredientMappers.ItemToDTO(ingredient);
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(long id, IngredientDTO ingredientDTO)
        {
            if (id != ingredientDTO.Id)
            {
                return BadRequest();
            }

           // _context.Entry(ingredient).State = EntityState.Modified;
              var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.Name = ingredientDTO.Name;
            ingredient.Price= ingredientDTO.Price;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientDTO>> PostIngredient(IngredientDTO ingredientDTO)
        {
            var ingredient = IngredientMappers.DTOToItem(ingredientDTO);
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngredient), new { id = ingredientDTO.Id },
             IngredientMappers.ItemToDTO(ingredient));
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(long id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(long id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
