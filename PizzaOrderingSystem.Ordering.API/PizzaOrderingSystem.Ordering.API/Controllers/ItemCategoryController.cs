using PizzaOrderingSystem.Ordering.API.Authentication;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Ordering.API.Models;
using PizzaOrderingSystem.Ordering.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace PizzaOrderingSystem.Ordering.API.Controllers
{
    /// <summary>
    /// Controller to do CRUD on ItemCategory Collection
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   public class ItemCategoryController : ControllerBase
    {
        private IItemCategoryService _itemCategoryService;
        public ItemCategoryController(IItemCategoryService MenuService)
        {
            _itemCategoryService = MenuService;
        }


        // GET: api/ItemCategory
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ItemCategory>>> Get()
        {
            return await _itemCategoryService.GetAsync();
        }

        // GET api/ItemCategory/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ItemCategory>> Get(string id)
        {
            var itemcategory = await _itemCategoryService.GetAsync(id);

            if (itemcategory == null)
            {
                return NotFound();
            }

            return itemcategory;
        }

        // POST api/ItemCategory

        [HttpPost]
        public async Task<ActionResult<ItemCategory>> Post([FromBody] ItemCategory value)
        {
            await _itemCategoryService.CreateAsync(value);
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }


        // PUT api/ItemCategory>/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ItemCategory value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            try
            {
                await _itemCategoryService.UpdateAsync(id, value);
            }
            catch (Exception)
            {
                var temp = await _itemCategoryService.GetAsync(id);
                if (temp == null)
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

        // DELETE api/ItemCategory/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _itemCategoryService.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _itemCategoryService.RemoveAsync(id);
            return NoContent();
        }
    }
}
