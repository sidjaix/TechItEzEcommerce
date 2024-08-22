namespace ApiServices.Utility;

public class ApplicationData
{
    public static string JwtTokenCookie { get; set; } = "JwtAuthToken";
    public static string CartDetail { get; set; } = "CartDetail";
    public static string WishlistItemCount { get; set; } = "WishlistItemCount";
    public static string AuthApiBaseAddress { get; set; }
    public static string ProductApiBaseAddress { get; set; }
    public static string CartApiBaseAddress { get; set; }
    public static string OrderApiBaseAddress { get; set; }
}
