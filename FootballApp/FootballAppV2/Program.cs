using BusinessLogicFootballApp.Interfaces;
using BusinessLogicFootballApp.Entities;
using BusinessLogicFootballApp.Services;
using FootballAppV2.Identity;
using InfrastructureFootballApp;
using InfrastructureFootballApp.Repository;
using InfrastructureFootballApp.ExternalServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.SignIn.RequireConfirmedEmail = true;
    }
    ).AddErrorDescriber<SpanishErrors>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddHttpClient<IServicio_API, Servicio_API>();

builder.Services.AddScoped<IAdmLeague, AdmLeague>();
builder.Services.AddScoped<IAdmMatchdays, AdmMatchdays>();
builder.Services.AddScoped<ICreateListMatchdays, CreateListMatchdays>();
builder.Services.AddScoped<IServicio_API, Servicio_API>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAccount/Login");


builder.Logging.AddNLog();
builder.Services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(1));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseExceptionHandler("/Error");
app.UseStaticFiles();
app.UseAuthentication();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
