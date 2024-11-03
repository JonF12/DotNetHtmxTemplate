using DotNetHtmxTemplate.Repository.Data;
using DotNetHtmxTemplate.Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddControllers();

builder.Services.AddTransient<IDatabaseService, DatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseEndpoints(endpoints => //may not be needed, go check
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=MoviesPages}/{action=Index}/");
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();
