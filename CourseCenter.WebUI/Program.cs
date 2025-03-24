using CourseCenter.WebUI.Filters;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Middlewares;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
//builder.Services.AddHttpClient();
builder.Services.AddHttpClient("HttpClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7092/api/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseCookies = true,
    AllowAutoRedirect = true
});

builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
builder.Services.AddSingleton<IRefreshTokenService, RefreshTokenService>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<UnauthorizedRedirectFilter>();
    options.Filters.Add<ForbiddenRedirectFilter>();
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin", builder =>
//    {
//        builder.WithOrigins("https://localhost:7092/") // Frontend URL'sini buraya yazın
//               .AllowAnyHeader()
//               .AllowAnyMethod()
//               .AllowCredentials(); // Cookie'lerin gönderilmesine izin verir
//    });
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseCors("AllowSpecificOrigin");
app.UseMiddleware<JwtValidationMiddleware>();
app.UseMiddleware<JwtAreaRoleCheckMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Home/NotFound"); // Sayfa yoksa gidilecek

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
