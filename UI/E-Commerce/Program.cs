using ApiServices.Services.IService;
using ApiServices.Services;
using ApiServices.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using E_Commerce.Utility;
using ApiServices.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDataProtection()
//     .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "keys")))
//     .SetApplicationName("web");

// builder.Services.AddDataProtection()
// .PersistKeysToFileSystem(new DirectoryInfo(@"/var/aspnetcore/data-protection-keys"))
// .SetApplicationName("e_commerce_web");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

ApplicationData.AuthApiBaseAddress = builder.Configuration.GetValue<string>("ServiceUrls:AuthApi");
ApplicationData.ProductApiBaseAddress = builder.Configuration.GetValue<string>("ServiceUrls:ProductApi");
ApplicationData.CartApiBaseAddress = builder.Configuration.GetValue<string>("ServiceUrls:CartApi");
ApplicationData.OrderApiBaseAddress = builder.Configuration.GetValue<string>("ServiceUrls:OrderApi");

// Add HttpClient to call apis
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddHttpClient<IBaseService, BaseService>(c =>
{
    c.BaseAddress = new Uri(ApplicationData.AuthApiBaseAddress);
});

builder.Services.AddOptions<JwtOptions>()   // returns an OptionsBuilder<TOptions> that binds to the JwtOptions class
.BindConfiguration("JWT")   // binds the values from the configuration section
.ValidateDataAnnotations()  //enables validation using data annotations
.ValidateOnStart(); // When we start the application, the validation will run on GitHubSettings and an exception is thrown if validation fails. 

// Or alternatively, configure it directly:
// builder.Services
// .Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddAuthorization();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAdminService, AdminService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<TokenValidationMiddleware>();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
