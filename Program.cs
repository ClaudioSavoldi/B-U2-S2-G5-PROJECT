using B_U2_S2_G5_PROJECT.Models.Entity;
using B_U2_S2_G5_PROJECT.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.SignIn.RequireConfirmedPhoneNumber = false;
    option.SignIn.RequireConfirmedEmail = false;
    option.SignIn.RequireConfirmedAccount = false;
    option.Password.RequiredLength = 8;
    option.Password.RequireDigit = false;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireNonAlphanumeric = false;




}).
AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

builder.Services.AddAuthentication("Cookies")
.AddCookie("Cookies");

builder.Services.ConfigureApplicationCookie(options => {
options.LoginPath = "/AspNetUser/Login"; 
options.AccessDeniedPath = "/AspNetUser/AccessDenied"; 
options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
});

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CameraService>();
builder.Services.AddScoped<PrenotazioneService>();
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
