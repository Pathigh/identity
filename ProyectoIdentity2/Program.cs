using ProyectoIdentity2.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//Configuramos la conexión a sql server
builder.Services.AddDbContext<ApplicationDbContext>(optiones =>
optiones.UseOracle(builder.Configuration.GetConnectionString("ConexionOracle")));

// agregar el servicio Identity a la aplicacion
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Esta Linea es para la Url de retorno al acceder
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Cuentas/Acceso");
    options.AccessDeniedPath = new PathString("Cuentas/Bloqueado");
});

//Estas son opciones de configuracion del Identity
builder.Services.Configure<IdentityOptions>(
    options =>
    {
        options.Password.RequiredLength = 5; // maximo de caracteres en la pass
        options.Password.RequireLowercase = true; // se deben ingresar minusculas
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // se bloquea la cuenta
        options.Lockout.MaxFailedAccessAttempts = 3; // maximos intentos malos 
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//Se agrega la Autenticacion
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
