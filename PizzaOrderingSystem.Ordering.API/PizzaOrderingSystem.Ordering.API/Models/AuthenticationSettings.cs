namespace PizzaOrderingSystem.Ordering.API.Models
{
    public class AuthenticationSettings
    {
        public string hashSecret { get; set; } = null!;

        public string Issuer { get; set; } = null!;

    }
}
