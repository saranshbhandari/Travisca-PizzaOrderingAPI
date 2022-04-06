using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Ordering.API.Models;
using PizzaOrderingSystem.Ordering.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaOrderingSystem.Ordering.API.Authentication;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Microsoft.AspNetCore.Authorization;
namespace PizzaOrderingSystem.Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuService;
        public MenuItemController(IMenuItemService MenuService)
        {
            _menuService = MenuService;
        }



        // GET: api/MenuItem
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> Get()
        {
            return await _menuService.GetAsync();
        }

        // GET api/MenuItem/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> Get(string id)
        {
            var menuitem = await _menuService.GetAsync(id);

            if (menuitem == null)
            {
                return NotFound();
            }

            return menuitem;
        }

        // POST api/MenuItem

        [HttpPost]
        public async Task<ActionResult<MenuItem>> Post([FromBody] MenuItem value)
        {
           await _menuService.CreateAsync(value);
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }


        // PUT api/MenuItem/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] MenuItem value)
        {
            if (id !=value.Id)
            {
                return BadRequest();
            }
            try
            {
              await _menuService.UpdateAsync(id, value);
            }
            catch (Exception)
            {
               var temp=await _menuService.GetAsync(id);
                if (temp==null)
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

        // DELETE api/MenuItem/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _menuService.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _menuService.RemoveAsync(id);
            return NoContent();
        }
    }
}
