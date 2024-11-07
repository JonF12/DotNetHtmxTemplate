using DotNetHtmxTemplate.Repository.Data;
using DotNetHtmxTemplate.Repository.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
AddServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=MoviesPages}/{action=/}/");
app.Run();


void AddServices() 
{
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddDbContext<AppDbContext>();
    builder.Services.AddControllers();
    builder.Services.AddTransient<IDatabaseService, DatabaseService>();
    builder.Services.Configure<FormOptions>(options =>
    {
        options.ValueLengthLimit = int.MaxValue;
        options.MultipartBodyLengthLimit = long.MaxValue; // In bytes
        options.MultipartHeadersLengthLimit = int.MaxValue;
        options.ValueCountLimit = int.MaxValue;
        options.MemoryBufferThreshold = int.MaxValue;
    });
    builder.Services.AddMvc();
    builder.Services.AddMvcCore();

    // Configure IIS
    builder.Services.Configure<IISServerOptions>(options =>
    {
        options.MaxRequestBodySize = int.MaxValue;
        options.MaxRequestBodyBufferSize = int.MaxValue;
    });
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/authentication";
        options.AccessDeniedPath = "/authentication";
        options.Cookie.Name = "DotNetHtmxTemplate";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
    });


}