using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Ordering.API.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public CustomerDetails CustomerDetails { get; set; }


        public List<MenuItem> ItemsOrdered { get; set; }

        public DateTime OrderedPlacedTime { get; set; }

        public string OrderStatus { get; set; } 
        
        public float TotalPrice { get; set; }





    }
}
