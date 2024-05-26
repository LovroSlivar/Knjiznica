namespace Knjiznica.Models
{
    public class Rezervacije
    {
        public bool trebaRecenziju {  get; set; }   
        public int bodovi {  get; set; }    

        public int Id { get; set; }
        public int lagerID {  get; set; }  
        public string posudba_od {  get; set; } 
        public string posudba_do { get; set; }  
        public bool trebaVratiti {  get; set; } 
        public string Naziv {  get; set; }
        public int knjigaID { get; internal set; }
    }
}
