using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAppTattoo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebAppTattooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppTattooContext") 
    ?? throw new InvalidOperationException("Connection string 'WebAppTattooContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SeedingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Ações para o ambiente de desenvolvimento
    app.UseDeveloperExceptionPage(); 

    // Configurações para o seeding
    using (var scope = app.Services.CreateScope())
    {
        var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
        seedingService.Seed();
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
