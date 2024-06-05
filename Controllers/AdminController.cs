using Knjiznica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Data;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Knjiznica.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly MyLists myLists;
        public MySqlConnection connection;
        private readonly string connectionString;
        private readonly UserManager<IdentityUser> userManager;
        public AdminController(string connectionString,MyLists myLists, UserManager<IdentityUser> userManager)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(connectionString);
            this.myLists = myLists;
            this.userManager = userManager; 
        }
        public IActionResult CreateZanr()
        {
            return View();
        }
        public IActionResult CreateUzrast()
        {
            return View();
        }
        public IActionResult CreateJezik()
        {
            return View();
        }
        public IActionResult CreateAutor()
        {
            return View();
        }
        public IActionResult CreateKnjiga()
        {
            KnjigaViewModel model = new KnjigaViewModel();
            model.Autori=myLists.Autori;
            model.Zanri=myLists.Zanri;  
            model.Uzrasti=myLists.Uzrasti;
            model.Jezici=myLists.Jezici;
            return View(model);
        }
        public IActionResult AddLager()
        { 
            LagerViewModel model = new LagerViewModel();
            try
            {
                connection.Open();
                var command = new MySqlCommand("SELECT knjiga.naslov FROM lager INNER JOIN knjiga ON lager.knjigaID = knjiga.ID WHERE lager.aktivan = 1;", connection);
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Nazivi.Add(reader.GetString("naslov"));
                    }
                }
                

            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
            return View(model);
        }
        [HttpPost]
        public IActionResult AddLager(string Knjiga)
        {
            int knjigaID = 0;
            try
            {
                connection.Open();
                var command = new MySqlCommand("select ID from knjiga where lower(naslov)=lower(@naslov)", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@naslov", Knjiga);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        TempData["ErrorMessage"] = $"{Knjiga} ne postoji.";
                        return RedirectToAction("AddLager");
                    }
                    while (reader.Read())
                    {
                        knjigaID = reader.GetInt32("ID");
                    }
                }
                command = new MySqlCommand("insert into lager (knjigaID, aktivan) values (@knjigaID, @aktivan)", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@knjigaID", knjigaID);
                command.Parameters.AddWithValue("@aktivan", 1);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
           
            return RedirectToAction("AddLager");
        }
        [HttpPost]
        public IActionResult CreateZanr(string zanr)
        {
            bool ImaReader = true;
            if (string.IsNullOrEmpty(zanr))
            {
                ViewBag.ErrorMessage = ($"{zanr}", "The value cannot be empty.");
                return View("CreateZanr");
            }
            try
            {
                connection.Open();
                var command = new MySqlCommand("select naziv from zanr where lower(naziv) =lower(@naziv)", connection);
                command.Parameters.AddWithValue("@naziv", zanr);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        ImaReader = false;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"{zanr} već postoji.";
                        return View("CreateZanr");
                    }
                }
                if (!ImaReader)
                {
                    command = new MySqlCommand("insert into zanr (naziv) values (@naziv)", connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naziv", zanr);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult CreateUzrast(string uzrast)
        {
            bool ImaReader = true;
            if (string.IsNullOrEmpty(uzrast))
            {
                ViewBag.ErrorMessage = ($"{uzrast}", "The value cannot be empty.");
                return View("CreateUzrast");
            }
            try
            {
                connection.Open();
                var command = new MySqlCommand("select naziv from uzrast where lower(naziv) = lower(@naziv)", connection);
                command.Parameters.AddWithValue("@naziv", uzrast);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        ImaReader = false;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"{uzrast} već postoji.";
                        return View("CreateUzrast");
                    }
                }
                if (!ImaReader)
                {
                    command = new MySqlCommand("insert into uzrast (naziv) values (@naziv)", connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naziv", uzrast);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult CreateJezik(string jezik)
        {
            bool ImaReader = true;
            if (string.IsNullOrEmpty(jezik))
            {
                ViewBag.ErrorMessage = ($"{jezik}", "The value cannot be empty.");
                return View("CreateJezik");
            }
            try
            {
                connection.Open();
                var command = new MySqlCommand("select naziv from jezik where lower(naziv) = lower(@naziv)", connection);
                command.Parameters.AddWithValue("@naziv", jezik);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        ImaReader = false;
                    }
                    else
                    {
                        ViewBag.ErrorMessage=$"{jezik} jezik već postoji.";
                        return View("CreateJezik");
                    }
                }
                if (!ImaReader)
                {
                    command = new MySqlCommand("insert into jezik (naziv) values (@naziv)", connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naziv", jezik);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult CreateAutor(string autor)
        {
            bool ImaReader = true;
            if (string.IsNullOrEmpty(autor))
            {
                ViewBag.ErrorMessage = ($"{autor}", "The value cannot be empty.");
                return View("CreateAutor");
            }
            try
            {
                connection.Open();
                var command=new MySqlCommand("select ime_prezime from autor where lower(ime_prezime) =lower(@naziv)",connection);
               command.Parameters.AddWithValue("@naziv", autor);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        ImaReader = false;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"{autor} već postoji.";
                        return View("CreateAutor");
                    }
                }
                if (!ImaReader) {
                    command = new MySqlCommand("insert into autor (ime_prezime) values (@naziv)",connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naziv", autor);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult CreateKnjiga(KnjigaViewModel model,string SelectedZanr,string SelectedAutor,string SelectedJezik, string SelectedUzrast)
        {
            bool ImaReader = true;
            int zanrID = 0;
            int jezikID=0;
            int autorID=0;
            int uzrastID=0;
            if (ModelState.IsValid) { 
            try
            {
                connection.Open();
                var command = new MySqlCommand("select naslov from knjiga where lower(naslov) =lower(@naslov)", connection);
                command.Parameters.AddWithValue("@naslov", model.Naslov);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        ImaReader = false;
                    }
                    else
                    {

                        TempData["ErrorMessage"] = $"{model.Naslov} već postoji.";
                        return RedirectToAction("CreateKnjiga");
                    }
                }
                if (!ImaReader)
                {
                    command= new MySqlCommand("select ID from zanr where lower(naziv)=lower(@naziv)",connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naziv", SelectedZanr);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zanrID = reader.GetInt32("ID");
                        }
                    }
                    command = new MySqlCommand("select ID from jezik where lower(naziv)=lower(@naziv)",connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naziv", SelectedJezik);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jezikID = reader.GetInt32("ID");
                        }
                    }
                    command = new MySqlCommand("select ID from autor where lower(ime_prezime)=lower(@ime_prezime)", connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ime_prezime", SelectedAutor);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            autorID = reader.GetInt32("ID");
                        }
                    }
                    command = new MySqlCommand("select ID from uzrast where lower(naziv)=lower(@naziv)", connection );
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naziv", SelectedUzrast);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            uzrastID = reader.GetInt32("ID");
                        }
                    }
                    command = new MySqlCommand("insert into knjiga (naslov, opis, broj_stranica, godina_izdavanja, autorID, jezikID, zanrID, uzrastID, aktivan) values (@naslov, @opis, @broj_stranica, @god_izdavanja, @autorID, @jezikID, @zanrID, @uzrastID, @aktivan)", connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@naslov", model.Naslov);
                    command.Parameters.AddWithValue("@opis", model.Opis);
                    command.Parameters.AddWithValue("@broj_stranica", model.Broj_stranica);
                    command.Parameters.AddWithValue("@god_izdavanja", model.God_izdavanja);
                    command.Parameters.AddWithValue("@autorID", autorID);
                    command.Parameters.AddWithValue("@jezikID", jezikID);
                    command.Parameters.AddWithValue("@zanrID", zanrID);
                    command.Parameters.AddWithValue("@uzrastID", uzrastID);
                    command.Parameters.AddWithValue("@aktivan", 1);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        

       
    }
}
