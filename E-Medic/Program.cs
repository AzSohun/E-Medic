using DinkToPdf;
using DinkToPdf.Contracts;
using E_Medic.Data;
using E_Medic.Models;
using E_Medic.Services;
using E_Medic.Services.Interfaces;
using E_Medic.Validators;
using FluentValidation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 1. User Secrets (Only for local development, must be added BEFORE builder.Build())
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddControllersWithViews();

// 3. Database Context Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 4. Identity Architecture Configuration
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    // Password strength settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 5. Data Protection Configuration
var keysFolder = Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys");
if (!Directory.Exists(keysFolder))
{
    Directory.CreateDirectory(keysFolder);
}
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder));


// 6. Application Cookie Policies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie.Name = "EMedicAuthCookie";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});



// 7. Custom Application Services Injection (DI)
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IPatientService, PatientService>();

// 8. Fluent Validation Assembly Registration
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

var app = builder.Build();

// 9. HTTP Request Pipeline (Middleware execution order)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// 10. Database Automations (Migrations & Seeding)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Handle Automatic Pendings Migrations
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        // Handle Database Seeding
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        await DbInitializer.SeedDataAsync(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while preparing or seeding the database.");
    }
}

app.Run();