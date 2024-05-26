using Knjiznica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Knjiznica.Controllers
{
    public class KnjigeController : Controller
    {
        // Deklaracija variabli
        public MySqlConnection connection;
        private readonly string connectionString;
        private readonly UserManager<IdentityUser> userManager;

        // Inicijalizacija variabli
        public KnjigeController(string connectionString, UserManager<IdentityUser> userManager)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(connectionString);
            this.userManager = userManager;
        }

        // Metoda za prikazivanje knjiga na lageru
        public IActionResult Index()
        {
            //Lista u kojoj su dostupne knjige na lageru
            List<Knjiga> knjige = new List<Knjiga>();

            // Try catch za hvatanje nepodrzanih gresaka
            try
            {
                //Otvaranje konekcije na bazu
                connection.Open();
                // Upit za hvatanje svih potrebnih podataka za dostupne knjige na lageru koje su aktivne
                string query = @"SELECT 
                                    knjiga.naslov AS Naslov,
                                    lager.ID as ID,
                                    knjiga.broj_stranica AS BrojStranica,
                                    knjiga.godina_izdavanja AS GodIzdavanja,
                                    zanr.naziv AS ZanrNaziv,
                                    jezik.naziv AS JezikNaziv,
                                    autor.ime_prezime AS AutorImePrezime
                                FROM 
                                    knjiga
                                    JOIN lager ON knjiga.ID = lager.knjigaID
                                    JOIN zanr ON knjiga.zanrID = zanr.ID
                                    JOIN jezik ON knjiga.jezikID = jezik.ID
                                    JOIN autor ON knjiga.autorID = autor.ID
                                WHERE 
                                    lager.aktivan = 1;";
                // Pokretanje upita sa deklariranom konekcijom
                MySqlCommand command = new MySqlCommand(query, connection);

                //Blok za čitanje dobivenog rezultata
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Za svaki pročitani red popuniti novi objekt knjiga
                    while (reader.Read())
                    {
                        Knjiga knjiga = new Knjiga();
                        knjiga.Naslov = reader.GetString("Naslov");
                        knjiga.Jezik = reader.GetString("JezikNaziv");
                        knjiga.Autor = reader.GetString("AutorImePrezime");
                        knjiga.Zanr = reader.GetString("ZanrNaziv");
                        knjiga.Broj_stranica = reader.GetInt32("BrojStranica");
                        knjiga.God_izdavanja = reader.GetInt32("GodIzdavanja");
                        knjiga.ID = reader.GetInt32("ID");

                        //Dodati taj navedeni objekt u proslo-navedenu listu
                        knjige.Add(knjiga);
                    }

                }

            }
            //Hvatanje nepodrzane greske
            catch (Exception)
            {
                return RedirectToAction("Greska", "Home");
            }
            //Uvijek se na kraju zatvara konekcija na bazu
            finally
            {
                connection.Close();
            }
            //Vracanje listu knijga na lageru na view
            return View(knjige);
        }
        //Autorizacija ove metode samo za usere
        [Authorize(Roles = "User")]
        //Metoda za recenziju odredene knjige
        public async Task<IActionResult> Recenzija(int bodovi, int knjigaID)
        {
            //Trazenje logiranog korisnika
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            //Provjera jesu li uneseni bodovi unutar korektnog ranga
            if (bodovi < 1 || bodovi > 5)
            {
                return RedirectToAction("Greska", "Home");
            }
            // Try catch za hvatanje nepodrzanih gresaka
            try
            {
                //Otvaranje konekcije na bazu
                connection.Open();
                //Upit
                var query = "insert into recenzija (knjigaID, bodovi, userID) values (@KnjigaID, @Bodovi, @UserID);";
                //Pokretanje upita
                var command = new MySqlCommand(query, connection);
                //Parametri
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", user.Id);
                command.Parameters.AddWithValue("@KnjigaID", knjigaID);
                command.Parameters.AddWithValue("@Bodovi", bodovi);

                //Execute tog upita
                command.ExecuteNonQuery();

            }
            //Hvatanje greske
            catch (Exception)
            {
                return RedirectToAction("Greska", "Home");
            }
            // Zatvaranje konekcije
            finally { connection.Close(); }
            //Vracanje na View Posudenih knjiga
            return RedirectToAction("PosudeneKnjige", "Knjige");
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PosudiKnjigu(int lagerID)
        {
            //Deklariranje varijabli
            bool mozeSePosuditi = false;
            bool imaPosudenu = false;
            //Hvatanje logiranog korisnika
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            //Provjera je li taj korisnik uopce logiran
            if (user != null)
            {
                ///Provjera broj posudenih knjiga
                mozeSePosuditi = ProvjeriBrojPosudenih(user.Id);
                imaPosudenu = ProvjeriPosudenu(user.Id, lagerID);
            }
            //Ako nije logiran, greška
            else
            {
                return RedirectToAction("Greska", "Home");
            }
            //Ako se moze posuditi
            if (mozeSePosuditi && !imaPosudenu)
            {
                //Posudi knjigu
                Posudi(user.Id, lagerID);
            }
            //Vracanje na view index
            return RedirectToAction("Index", "Knjige");
        }
        [Authorize(Roles = "User")]
        //Metoda za posudivanje knjige i upis u bazu da je knjiga posudena
        private void Posudi(string userID, int lagerID)
        {
            //Varijable
            DateTime timeNow = DateTime.Now;
            DateTime timeTomorrow = timeNow.AddDays(30);
            string formatedTimeNow = timeNow.ToString("yyyy-MM-dd");
            string formatedTimeTomorrow = timeTomorrow.ToString("yyyy-MM-dd");
            //Try catch za hvatanje gresaka
            try
            {
                //Otvaranje konekcije
                connection.Open();
                //upit
                string query = "insert into rezervacija (lagerID, userID, posudba_od, posudba_do, aktivan) values (@LagerID, @UserID, @Posudba_od, @Posudba_do, @Aktivan)";
                //Pokretanje upita
                MySqlCommand command = new MySqlCommand(query, connection);
                //Parametri
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@LagerID", lagerID);
                command.Parameters.AddWithValue("@Posudba_od", formatedTimeNow);
                command.Parameters.AddWithValue("@Posudba_do", formatedTimeTomorrow);
                command.Parameters.AddWithValue("@Aktivan", 1);
                //Execute upita
                command.ExecuteNonQuery();
                //Novi upit
                query = "update lager set aktivan = 0 where lager.ID = (@LagerID)";
                //Novo pokretanje upita
                command = new MySqlCommand(query, connection);
                //Novi parametri
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@LagerID", lagerID);
                //Execute novog upita
                command.ExecuteNonQuery();

            }
            //Hvatanje greska
            catch (Exception)
            {
                RedirectToAction("Greska", "Home");
            }
            //Zatvaranje konekcije
            finally
            {
                connection.Close();
            }
        }
        //Metoda za provjeru posude
        [Authorize(Roles = "User")]
        private bool ProvjeriPosudenu(string userID, int lagerID)
        {
            try
            {
                //Otvaranje konekcije
                connection.Open();
                //Upit ima li rezevacija vec zapisan zapis te knjige
                string query = "select ID from rezervacija where rezervacija.userID = (@UserID) and rezervacija.aktivan = 1 and rezervacija.lagerID = (@LagerID)";
                //Pokretanje upita
                MySqlCommand command = new MySqlCommand(query, connection);
                //Parametri
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@LagerID", lagerID);

                //Execute upita i dobivanje readera
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Provjera ima li reader vraceni rezultat
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            //Hvatanje greska
            catch (Exception)
            {
                throw;
            }
            //Zatvaranje konekcije
            finally
            {
                connection.Close();
            }
        }
        [Authorize(Roles = "User")]
        //Metoda za provjeru broj posudenih knijga
        private bool ProvjeriBrojPosudenih(string userID)
        {
            //Varijabla
            int brojPosudenih = 0;
            try
            {
                //Otvaranje konekcije
                connection.Open();
                //Upit za zbroj posudenih knijga u rezervacije tog korisnika
                string query = "select COUNT(ID) as BrojPosudenih from rezervacija where rezervacija.userID = (@UserID) and rezervacija.aktivan = 1";
                //Pokretanje upita
                MySqlCommand command = new MySqlCommand(query, connection);
                //Parametri
                command.Parameters.AddWithValue("@UserID", userID);
                //Execute upita i vracanje readera
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Čitanje kroz reader i zapis u varijablu broj posudenih knjiga
                    while (reader.Read())
                    {
                        brojPosudenih = reader.GetInt32("BrojPosudenih");

                    }
                    //Ako je broj posudenih manji od 4 moze se posuditi
                    if (brojPosudenih < 4)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            //Hvatanje greska
            catch (Exception)
            {
                throw;
            }
            //Zatvaranje konekcije
            finally
            {
                connection.Close();
            }
        }
        [Authorize(Roles = "User")]
        //Glavana metoda za vracanje knjige
        public async Task<IActionResult> VratiKnjigu(int lagerID)
        {
            //Varijable
            bool kasni = false;
            //Hvatanje logiranog korisnika
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            //provjera ima li logiran korisnik
            if (user != null)
            {
                //Provjera kasnjenja knjige
                kasni = ProvjeraKasnjenja(user.Id, lagerID);
            }
            else
            {
                RedirectToAction("Greska", "Home");
            }
            //Ako kasni, naplatiti kasninu
            if (kasni)
            {
                //Vratiti knjigu
                Vrati(user.Id, lagerID);
                return RedirectToAction("Kasnjenje", "Knjige");
            }
            else
            {
                //Vratiti knjigu
                Vrati(user.Id, lagerID);
                return RedirectToAction("PosudeneKnjige", "Knjige");

            }
        }
        [Authorize(Roles = "User")]
        //Vracanje knjige
        private void Vrati(string userID, int lagerID)
        {

            try
            {
                //Otvaranje konekcije
                connection.Open();
                //Upit promjene da vise rezervacije knjige nije aktivna za tog korisnika
                string query = "update rezervacija set aktivan = (@Aktivan) where rezervacija.lagerID = (@LagerID) and rezervacija.userID = (@UserID)";
                //Pokretanje upita
                MySqlCommand command = new MySqlCommand(query, connection);
                //Parametri
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@LagerID", lagerID);
                command.Parameters.AddWithValue("@Aktivan", 0);
                //Execute upita
                command.ExecuteNonQuery();
                //Novi upit 
                query = "update lager set aktivan = 1 where lager.ID = (@LagerID)";
                command = new MySqlCommand(query, connection);
                //Novi parametri
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@LagerID", lagerID);
                // Execute novog upita
                command.ExecuteNonQuery();

            }
            //Hvatanje greska
            catch (Exception)
            {

                throw;
            }
            //Zatvaranje konekcije
            finally
            {
                connection.Close();
            }
        }
        [Authorize(Roles = "User")]
        //Metoda provjere kasnjenja vracanje knijge
        private bool ProvjeraKasnjenja(string userID, int lagerID)
        {
            //Variable
            DateTime timeNow = DateTime.Now;
            string formatedTimeNow = timeNow.ToString("yyyy-MM-dd");
            //Novi objekt knjige
            Knjiga knjiga = new Knjiga();
            try
            {
                //Otvaranje konekcije
                connection.Open();
                //Upit za dohvacivanja datuma posudbe te knjige toga korisnika
                string query = "select posudba_do from rezervacija where rezervacija.userID = (@UserID) and rezervacija.aktivan = 1 and rezervacija.lagerID = (@LagerID)";
                //Pokretanje upita
                MySqlCommand command = new MySqlCommand(query, connection);
                //Parametri
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@LagerID", lagerID);
                //Execute upita i reader
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Čitanje kroz reader
                    while (reader.Read())
                    {
                        //Dohvacivanje iz reader posudbe do
                        DateTime dateTimeValue = reader.GetDateTime(reader.GetOrdinal("posudba_do"));
                        knjiga.Posudba_do = dateTimeValue.ToString("yyyy-MM-dd");
                    }
                    //Formatiranje posudbe do
                    DateTime posudeno_do = DateTime.Parse(knjiga.Posudba_do);
                    DateTime formatedTime = DateTime.Parse(formatedTimeNow);
                    //Provjera kasni li vracanje od danasjeg datuma
                    if (posudeno_do < formatedTime)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            //Hvatanje greska
            catch (Exception)
            {

                throw;
            }
            //Zatvaranje konekcije
            finally
            {
                connection.Close();
            }
        }
        [Authorize(Roles = "User")]
        //Metoda za dohvacanje korisnikovih posudenih knjiga
        public async Task<IActionResult> PosudeneKnjige()
        {
            //Lista posudenih knijga
            List<Rezervacije> reservations = new List<Rezervacije>();
            //Hvatanje logiranog korisnika
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            try
            {
                //Otvaranje konekcije
                connection.Open();
                //Upit za dohvacanje posudenih knjiga
                string query = @"SELECT
                                   rezervacije.ID,
                                    knjiga.ID AS knjigaID,
                                    rezervacije.lagerID,
                                    rezervacije.posudba_od,
                                    rezervacije.posudba_do,
                                    knjiga.Naslov AS NaslovKnjige,
                                    CASE
                                        WHEN rezervacije.aktivan = 1 THEN 1
                                        ELSE 0
                                    END AS TrebaVratiti,
                                    CASE 
                                        WHEN recenzija.userID IS NULL THEN 1
                                        ELSE 0
                                    END AS TrebaRecenziju,
                                    CASE 
                                        WHEN recenzija.bodovi IS NULL THEN 0
                                        ELSE recenzija.bodovi
                                    END AS Bodovi
                                FROM
                                    rezervacija rezervacije
                                JOIN
                                    lager ON rezervacije.lagerID = lager.ID
                                JOIN
                                    knjiga ON lager.knjigaID = knjiga.ID
                                LEFT JOIN
                                    recenzija ON rezervacije.userID = recenzija.userID AND knjiga.ID = recenzija.knjigaID
                                    AND recenzija.userID = @UserID
                                WHERE
                                    rezervacije.userID = @UserID;";
                //Pokretanje upita
                MySqlCommand command = new MySqlCommand(query, connection);
                //Parametri
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", user.Id);
                //Execute upita i reader
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //Čitanje iz reader i upisivanje u novi objekt posudene knjige
                    while (reader.Read())
                    {
                        Rezervacije reservation = new Rezervacije
                        {
                            Id = reader.GetInt32("ID"),
                            lagerID = reader.GetInt32("lagerID"),
                            Naziv = reader.GetString("NaslovKnjige"),
                            posudba_od = reader.GetDateTime(reader.GetOrdinal("posudba_od")).ToString("yyyy-MM-dd"),
                            posudba_do = reader.GetDateTime(reader.GetOrdinal("posudba_do")).ToString("yyyy-MM-dd"),
                            trebaVratiti = reader.GetInt32("TrebaVratiti") == 1,
                            trebaRecenziju = reader.GetInt32("TrebaRecenziju") == 1,
                            bodovi = reader.GetInt32("Bodovi"),
                            knjigaID = reader.GetInt32("knjigaID")
                        };
                        //Dodavanje u prijasnje nevedenu listu
                        reservations.Add(reservation);
                    }
                }
            }
            //Hvatanje greska
            catch (Exception ex)
            {
                throw;
            }
            //Vracanje view sa listom
            return View(reservations);


        }

        [Authorize(Roles = "User")]

        public IActionResult Opis(int id, DateTime date)
        {
            KnjigaViewModel knjiga = new KnjigaViewModel();

            try
            {
                connection.Open();
                string query = @"SELECT 
                    knjiga.naslov,
                    knjiga.opis,
                    knjiga.broj_stranica,
                    knjiga.godina_izdavanja,
                    zanr.naziv AS ZanrNaziv,
                    autor.ime_prezime AS AutorImePrezime,
                    jezik.naziv AS JezikNaziv,
                    uzrast.naziv AS UzrastNaziv
                FROM 
                    knjiga
                    JOIN zanr ON knjiga.zanrID = zanr.ID
                    JOIN autor ON knjiga.autorID = autor.ID
                    JOIN jezik ON knjiga.jezikID = jezik.ID
                    JOIN uzrast ON knjiga.uzrastID = uzrast.ID
                    JOIN dogadanje ON knjiga.ID = dogadanje.knjigaID
                WHERE 
                    knjiga.ID = @KnjigaID
                    AND @date BETWEEN dogadanje.prikaz_od AND dogadanje.prokaz_do
                    AND dogadanje.aktivan = 1;";
            
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@KnjigaID", id);
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        knjiga = new KnjigaViewModel
                        {
                            Naslov = reader.GetString("Naslov"),
                            Opis = reader.GetString("Opis"),
                            Broj_stranica = reader.GetInt32("Broj_stranica"),
                            God_izdavanja = reader.GetInt32("Godina_izdavanja"),
                            Zanri = new List<string> { reader.GetString("ZanrNaziv") },
                            Autori = new List<string> { reader.GetString("AutorImePrezime") },
                            Jezici = new List<string> { reader.GetString("JezikNaziv") },
                            Uzrasti = new List<string> { reader.GetString("UzrastNaziv") }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return View(knjiga);
        }
    }
}