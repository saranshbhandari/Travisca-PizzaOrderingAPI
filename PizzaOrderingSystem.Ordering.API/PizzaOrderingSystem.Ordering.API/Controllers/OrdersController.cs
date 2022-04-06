using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Ordering.API.Models;
using PizzaOrderingSystem.Ordering.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaOrderingSystem.Ordering.API.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace PizzaOrderingSystem.Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService OrderService)
        {
            _orderService = OrderService;
        }


        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _orderService.GetAsync();
        }

        [HttpGet("GetTodaysOrder")]
        public async Task<ActionResult<IEnumerable<Order>>> GetTodaysOrder()
        {
            return await _orderService.GetTodaysOrderAsync();
        }

        [HttpGet("GetTodaysIncompleteOrder")]
        public async Task<ActionResult<IEnumerable<Order>>> GetTodaysIncompleteOrder()
        {
            return await _orderService.GetTodaysInCompleteOrderAsync();
        }

        // GET api/Order/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Order>> Get(string id)
        {
            var Order = await _orderService.GetAsync(id);

            if (Order == null)
            {
                return NotFound();
            }

            return Order;
        }

        // POST api/Order
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Order>> Post([FromBody] Order value)
        {
            await _orderService.CreateAsync(value);
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }


        // PUT api/Order/5
       
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Order value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            try
            {
                await _orderService.UpdateAsync(id, value);
            }
            catch (Exception)
            {
                var temp = await _orderService.GetAsync(id);
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

        // DELETE api/Order/5
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _orderService.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderService.RemoveAsync(id);
            return NoContent();
        }
    }
}
