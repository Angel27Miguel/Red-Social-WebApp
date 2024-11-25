using RedSocialWebApp.Infrastructure.Shared;
using RedSocialWebApp.Infrastucture.Persistence;
using RedSocialWebApp.Middlewares;
using RedSocialWebApp.Core.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar infraestructura y otras dependencias
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);

// Registrar IHttpContextAccessor y configurar la sesi�n
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configura el tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // Asegura que las cookies sean accesibles solo por el servidor
    options.Cookie.IsEssential = true; // Asegura que la cookie sea necesaria para la funcionalidad de la aplicaci�n
});

// Agregar el middleware de sesi�n
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();

var app = builder.Build();

// Middleware para manejar excepciones y HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar el uso de sesiones y autorizaci�n
app.UseSession(); // Aseg�rate de agregar esta l�nea antes de UseAuthorization()
app.UseAuthorization();

// Configurar las rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();