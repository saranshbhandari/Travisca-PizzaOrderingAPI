using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaOrderingSystem.Ordering.API.Models
{
    public class CustomerDetails
    {

        public int Id { get; set; }
       
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AddressFirstLine { get; set; }
        
        [MaxLength(100)]
        public string AddressSecondLine { get; set; }

        [MaxLength(100)]
        public string AddressLandmark { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AddressCity { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string State { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
        
        [Required]
        [MaxLength(30)]
        public int PinCode { get; set; }
        
        [Required]
        [MaxLength(4)]
        public string CountryCode { get; set; }
        
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        
        [JsonIgnore]
        public string FullAddress {
            get
            {
                return $"{AddressFirstLine},{Environment.NewLine}" +
                    $"{AddressSecondLine},{Environment.NewLine}" +
                    $"{AddressLandmark},{Environment.NewLine}" +
                    $"{AddressCity},{Environment.NewLine}" +
                    $"{State},{Environment.NewLine}" +
                    $"{Country},{Environment.NewLine}" +
                     $"{PinCode},{Environment.NewLine}";
            }
        }



    }
}
