using System;
using Xunit;
using PizzaOrderingSystem.Ordering.API.Models;
using PizzaOrderingSystem.Ordering.API.Controllers;
using PizzaOrderingSystem.Ordering.API.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PizzaOrderingSystem.Ordering.Test
{
    public class OrderControllerTests
    {
        public IOrderService GetOrderServiceMock()
        {
            Mock<IOrderService> orderservice = new Mock<IOrderService>();
            orderservice.Setup(m => m.GetAsync()).Returns(() => Task.FromResult(
                new List<Order>()
                {
                     new Order()
                    {
                        OrderStatus="Complete",Id="12345",CustomerDetails=new CustomerDetails(){FirstName="DummyUser" }
                    },
                      new Order()
                    {
                        OrderStatus="Complete",Id="343225",CustomerDetails=new CustomerDetails(){FirstName="DummyUser" }
                    }
                }
            ));
            return orderservice.Object;
        }

        [Fact]
        public async Task Should_Get_All_Orders()
        {
            
            OrdersController ordersController = new OrdersController(GetOrderServiceMock());
            ActionResult<IEnumerable<Order>> result=await ordersController.Get();
            Assert.IsAssignableFrom<IEnumerable<Order>>(result.Value);
            Assert.NotEmpty(result.Value);
            Assert.Equal(2,result.Value.ToList().Count);
            Assert.All(result.Value, x=>Assert.Equal ("Complete",x.OrderStatus));
            
        }
    }
}
