using ApiServices.Services.IService;
using ApiServices.Services;
using ApiServices.Utility;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.Cookies;

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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Account/Login";
        //options.AccessDeniedPath = "/Auth/AccessDenied";
    });

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
