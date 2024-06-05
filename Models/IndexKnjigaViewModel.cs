namespace Knjiznica.Models
{
    public class IndexKnjigaViewModel
    {
      public List<Knjiga> knjige {  get; set; }=new List<Knjiga>(); 
        public List<string> Zanri { get; set; } = new List<string>();
        public List<string> Autori { get; set; } = new List<string>();
        public List<string> Jezici { get; set; } = new List<string>();
        public List<string> Uzrasti { get; set; } = new List<string>();
    }
}
