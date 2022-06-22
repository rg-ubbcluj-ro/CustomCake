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
    public class CakesController : ControllerBase
    {
        private readonly CustomCakeContext _context;

        public CakesController(CustomCakeContext context)
        {
            _context = context;
        }

        // GET: api/Cakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CakeDTO>>> GetCakes()
        {
            var query = _context.Cakes.AsQueryable();

            return await query
            .Include(t => t.Ingredients)
            .Select(item => CakeMappers.ItemToDTO(item))
            .ToListAsync();
        }
        
        //filter cakes by customer
        [HttpGet("filter")]
        public async Task<ActionResult> GetCakesByCustomerName(string name)
        {
            var customer = await GetCustomerByName(name);
            var filteredCustomer = _context.Cakes
                .AsQueryable()
                .Include(t => t.Ingredients)
                .Where(x => x.CustomerId == customer.Id)
                .ToList();

            return Ok(filteredCustomer);
        }

        [HttpPost]
        public async Task<ActionResult<CakeDTO>> PostCake(CakeDTO cakeDTO)
        {
            var cake = CakeMappers.DTOToItem(cakeDTO);
            var customer = await _context.Customers.AsQueryable()
                .FirstOrDefaultAsync(customer => customer.Id == cakeDTO.CustomerId);
            var cakePrice = cake.getPrice();
            
            if(cakePrice > customer.Budget)
            {
                return NotFound(); //NotAcceptable
            }

            customer.Budget -= cakePrice;
            
            _context.Cakes.Add(cake);
            await _context.SaveChangesAsync();

            return Ok(CakeMappers.ItemToDTO(cake));
        }

        private async Task<Customer> GetCustomerByName(string name){
            var query = _context.Customers.AsQueryable();

            return await query 
            .FirstOrDefaultAsync(customer => customer.Name == name);
        }

        // // GET: api/Cakes/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<CakeDTO>> GetCake(long id)
        // {
        //     var query = _context.Cakes.AsQueryable();

        //     var cake = await query 
        //     .Include(t => t.Ingredients)
        //     .Include(t => t.Customer)
        //     .FirstOrDefaultAsync(cake => cake.Id == id);

        //     if (cake == null)
        //     {
        //         return NotFound();
        //     }

        //     return CakeMappers.ItemToDTO(cake);
        // }

        // // PUT: api/Cakes/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutCake(long id, CakeDTO cakeDTO)
        // {
        //     if (id != cakeDTO.Id)
        //     {
        //         return BadRequest();
        //     }

        //    // _context.Entry(cake).State = EntityState.Modified;
        //     var cake = await _context.Cakes.FindAsync(id);
        //     if (cake == null)
        //     {
        //         return NotFound();
        //     }

        //     cake.Name = cakeDTO.Name;
        //     cake.Weight= cakeDTO.Weight;
            
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!CakeExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/Cakes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // // DELETE: api/Cakes/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteCake(long id)
        // {
        //     var cake = await _context.Cakes.FindAsync(id);
        //     if (cake == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Cakes.Remove(cake);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool CakeExists(long id)
        // {
        //     return _context.Cakes.Any(e => e.Id == id);
        // }
    }
}
