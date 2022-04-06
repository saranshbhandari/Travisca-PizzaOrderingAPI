using System;
namespace PizzaOrderingSystem.Ordering.API.Authentication
{


    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }

}
