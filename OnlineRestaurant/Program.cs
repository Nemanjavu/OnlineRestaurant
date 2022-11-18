using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineRestaurant.Data;
using OnlineRestaurant.Data.Cart;
using OnlineRestaurant.Data.Services;
using OnlineRestaurant.Helpers;
using OnlineRestaurant.Interfaces;
using OnlineRestaurant.Models;
using OnlineRestaurant.Services;
using Stripe;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

StripeConfiguration.ApiKey = "sk_test_51LyHc5BJNHNsIDyulrUild7XxhHCKyxSogo8Xn1cNjjC6M1pVXagYdBmDlsawgs68IKWu9Z5b5sTGp2M4KL5oeTp00BCXjXYTE";

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(e =>
    e.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); 
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ISendGridEmail, SendGridEmail>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendGrid"));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(options =>
    {
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddFacebook(options =>
    {
        options.AppId = "1150053208921200";
        options.AppSecret = "a4d62b6d8a961c7de9657223c506d2a7";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "191139091889-dkh50182oo7d4iuoce65aijitnpka1f3.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(20);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});


builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    opt.Lockout.MaxFailedAccessAttempts = 5;
    //opt.SignIn.RequireConfirmedAccount = true;


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();


app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//Seed database
ApplicationDBInitializer.Seed(app);
ApplicationDBInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();




