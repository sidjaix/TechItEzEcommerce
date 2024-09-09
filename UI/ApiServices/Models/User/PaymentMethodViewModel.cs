namespace ApiServices.Models.User;

public class PaymentMethodViewModel
{
    public int PaymentMethodId { get; set; }
    public string UserId { get; set; }
    public UserViewModel User { get; set; }

    public int MethodTypeId { get; set; }  // Payment method type (e.g., Credit Card, PayPal, etc.)

    // Card details (only if method is Credit/Debit Card)
    public string CardNumber { get; set; }  // Storing this securely, use encryption or tokenization
    public string CardHolderName { get; set; }
    public string ExpiryMonth { get; set; }  // MM format
    public string ExpiryYear { get; set; }   // YYYY format
    public string CVV { get; set; }  // Store securely; consider encryption
}
