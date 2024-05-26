namespace Knjiznica.Models
{
    public class Knjiga
    {
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public int Broj_stranica { get; set; }
        public int God_izdavanja { get; set; }
        public string Autor { get; set; }
        public string Zanr { get; set; }
        public string Uzrast { get; set; }
        public string Jezik { get; set; }

        public string Posudba_do { get; set; }

    }
}
