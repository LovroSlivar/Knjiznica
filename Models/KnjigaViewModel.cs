using System.ComponentModel.DataAnnotations;

namespace Knjiznica.Models
{
    public class KnjigaViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "The title must not exceed {1} characters.")]

        public string Naslov { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(200, ErrorMessage = "The title must not exceed {1} characters.")]
        public string Opis { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Broj stranica mora biti pozitivan cijeli broj")]
        public int Broj_stranica { get; set; } = 1;

        [Range(0, 2024, ErrorMessage = "Godina izdavanja mora biti između 0 i 2024 godine")]
        public int God_izdavanja { get; set; } = 2024;
        public List<string> Zanri { get; set; } = new List<string>();
        public List<string> Autori { get; set; } = new List<string>();
        public List<string> Jezici { get; set; } = new List<string>();
        public List<string> Uzrasti { get; set; } = new List<string>();
    }
}
