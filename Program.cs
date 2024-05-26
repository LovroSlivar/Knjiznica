using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Knjiznica.Data;
using Knjiznica.Models;
using MySql.Data.MySqlClient;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("KnjiznicaContextConnection") ?? throw new InvalidOperationException("Connection string 'KnjiznicaContextConnection' not found.");
builder.Services.AddSingleton(connectionString);
builder.Services.AddDbContext<KnjiznicaContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;   

})
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<KnjiznicaContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var MyLists = new MyLists();

using (var connection = new MySqlConnection(connectionString))
{
	try
	{
		connection.Open();
		var command = new MySqlCommand("SELECT naziv from zanr", connection);
		using ( var reader = command.ExecuteReader())
		{
			while (reader.Read())
			{
				MyLists.Zanri.Add(reader.GetString("naziv"));
			}
		}
         command = new MySqlCommand("SELECT naziv from uzrast", connection);
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                MyLists.Uzrasti.Add(reader.GetString("naziv"));
            }
        }
         command = new MySqlCommand("SELECT naziv from jezik", connection);
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                MyLists.Jezici.Add(reader.GetString("naziv"));
            }
        }
         command = new MySqlCommand("SELECT ime_prezime from autor", connection);
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                MyLists.Autori.Add(reader.GetString("ime_prezime"));
            }
        }
        builder.Services.AddSingleton(MyLists);
    }
	catch (Exception)
	{

		throw;
	}
    finally
    {
        connection.Close();
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

