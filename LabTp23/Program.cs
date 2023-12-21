using LabTp23.DAL;
using LabTp23.DAL.Repositories.Implementations;
using LabTp23.DAL.Repositories.Interfaces;
using LabTp23.Services.Implementations;
using LabTp23.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContext<ShopDbContext>(opt => opt.UseSqlite("Filename=Shop.db"))
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IPurchaseRepository, PurchaseRepository>()
    .AddScoped<ICartRepository, CartRepository>()
    .AddScoped<ICartService, CartService>()
    .AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// INIT DATABASE
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    ctx.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
