using DotNetHtmxTypescriptTemplate.Data;
using DotNetHtmxTypescriptTemplate.Services;
using System.Text;
using WebMarkupMin.AspNetCore8;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddControllers();

builder.Services.AddTransient<IDatabaseService, DatabaseService>();

builder.Services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true; 
    options.AllowCompressionInDevelopmentEnvironment = true;
})
.AddHtmlMinification(options =>
{
    options.MinificationSettings.RemoveOptionalEndTags = true;
    options.MinificationSettings.MinifyInlineCssCode = true;
    options.MinificationSettings.MinifyInlineJsCode = true;
    options.MinificationSettings.PreserveNewLines = false;
    options.MinificationSettings.WhitespaceMinificationMode = WebMarkupMin.Core.WhitespaceMinificationMode.Aggressive; // Removes extra spaces
    options.MinificationSettings.RemoveHtmlComments = true; // Remove comments from output
})
.AddHttpCompression(options => { 
    
});

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

// Use WebMarkupMin for minifying HTML
app.UseWebMarkupMin();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapRazorPages();

app.Run();
