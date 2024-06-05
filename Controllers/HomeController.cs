using Knjiznica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Knjiznica.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyLists myLists;
        public MySqlConnection connection;
        private readonly string connectionString;
        private readonly UserManager<IdentityUser> userManager;
        public HomeController(string connectionString, MyLists myLists, UserManager<IdentityUser> userManager)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(connectionString);
            this.myLists = myLists;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            Knjiga knjiga = new Knjiga();
            knjiga = DanasnjiDogadaj();
            return View(knjiga);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Metoda vracanje danasnje knjige 
        public Knjiga DanasnjiDogadaj()
        {
            //Novi model knjige
            Knjiga knjiga = new Knjiga();
            try
            {
                //Otvraranje konekcije
                connection.Open();
                //Upit za dohvat danasnje knjige
                string query = @"SELECT knjiga.ID, knjiga.naslov, knjiga.opis,
                             autor.ime_prezime AS AutorNaziv,
                             zanr.naziv AS ZanrNaziv,
                             jezik.naziv AS JezikNaziv,
                             uzrast.naziv AS UzrastNaziv,
                             knjiga.godina_izdavanja AS GodinaIzdavanja, 
                             knjiga.broj_stranica AS BrojStanica 
                             FROM knjiga
                             JOIN dogadanje ON knjiga.ID = dogadanje.knjigaID
                             JOIN autor ON knjiga.autorID = autor.ID
                             JOIN zanr ON knjiga.zanrID = zanr.ID
                             JOIN jezik ON knjiga.jezikID = jezik.ID
                             JOIN uzrast ON knjiga.uzrastID = uzrast.ID
                             WHERE DATE(dogadanje.prikaz_od) = CURDATE() AND dogadanje.aktivan = 1";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            knjiga = new Knjiga();
                            knjiga.ID = reader.GetInt32("ID");
                            knjiga.Naslov = reader.GetString("naslov");
                            knjiga.Opis = reader.GetString("opis");
                            knjiga.Autor = reader.GetString("AutorNaziv");
                            knjiga.Zanr = reader.GetString("ZanrNaziv");
                            knjiga.Jezik = reader.GetString("JezikNaziv");
                            knjiga.Uzrast = reader.GetString("UzrastNaziv");
                            knjiga.God_izdavanja = reader.GetInt32("GodinaIzdavanja");
                            knjiga.Broj_stranica = reader.GetInt32("BrojStanica");
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
            return knjiga;
        }

    }
}
