using System.Collections;
using SoftwareEngineerProject.Components;
using SoftwareEngineerProject.DataStructures;
using SoftwareEngineerProject.Database;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using SoftwareEngineerProject.Components.Middleware;
using SoftwareEngineerProject.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new NullReferenceException("No connection string found");

// Add services to the container.
builder.Services.AddRazorComponents() 
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

// Add Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILockerService, LockerService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<IMerchandiseService, MerchandiseService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPoolService, PoolService>();
builder.Services.AddScoped<ICustomerLessonService, CustomerLessonService>();

// Add Data Sharing Service
builder.Services.AddSingleton<DataService>();

// Add authentication services to the project
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Allow app to use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Allow the app to use the middleware created
app.UseMiddleware<AuthenticationMiddleware>();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// TODO:
// Re-write testing so that it conforms with the DB models, not entities, then delete entities

// Reduce the number of times the DB is called, save models to a hashtable and call from that until value is updated

// Must add checks so that no two facility have the same name and address

// Add error handling for all new methods

app.Run();