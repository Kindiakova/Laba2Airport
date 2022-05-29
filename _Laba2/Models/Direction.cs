using System.ComponentModel.DataAnnotations;
namespace Laba2.Models
{
    public class Direction
    {
        public Direction()
        {
            Flights = new List<Flight>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Країна")]
        public string CountryFrom { get; set; }

        [Required]
        [Display(Name = "Місто")]
        public string CityFrom { get; set; }

        [Required]
        [Display(Name = "Термінал")]
        public string TerminalFrom { get; set; }

        [Required]
        [Display(Name = "Злітна смуга")]
        public string RunwayFrom { get; set; }

        [Required]
        [Display(Name = "Країна")]
        public string CountryTo { get; set; }

        [Required]
        [Display(Name = "Місто")]
        public string CityTo { get; set; }

        [Required]
        [Display(Name = "Термінал")]
        public string TerminalTo { get; set; }

        [Required]
        [Display(Name = "Злітна смуга")]
        public string RunwayTo { get; set; }

        public virtual ICollection<Flight>? Flights { get; set; }
    }

    public class DirectionShort
    {
        public DirectionShort()
        {
            Flights = new List<int>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Країна")]
        public string CountryFrom { get; set; }

        [Required]
        [Display(Name = "Місто")]
        public string CityFrom { get; set; }

        [Required]
        [Display(Name = "Термінал")]
        public string TerminalFrom { get; set; }

        [Required]
        [Display(Name = "Злітна смуга")]
        public string RunwayFrom { get; set; }

        [Required]
        [Display(Name = "Країна")]
        public string CountryTo { get; set; }

        [Required]
        [Display(Name = "Місто")]
        public string CityTo { get; set; }

        [Required]
        [Display(Name = "Термінал")]
        public string TerminalTo { get; set; }

        [Required]
        [Display(Name = "Злітна смуга")]
        public string RunwayTo { get; set; }

        public virtual ICollection<int>? Flights { get; set; }
    }

    public class DirectionWrite
    {
        [Required]
        [Display(Name = "Країна")]
        public string CountryFrom { get; set; }

        [Required]
        [Display(Name = "Місто")]
        public string CityFrom { get; set; }

        [Required]
        [Display(Name = "Термінал")]
        public string TerminalFrom { get; set; }

        [Required]
        [Display(Name = "Злітна смуга")]
        public string RunwayFrom { get; set; }

        [Required]
        [Display(Name = "Країна")]
        public string CountryTo { get; set; }

        [Required]
        [Display(Name = "Місто")]
        public string CityTo { get; set; }

        [Required]
        [Display(Name = "Термінал")]
        public string TerminalTo { get; set; }

        [Required]
        [Display(Name = "Злітна смуга")]
        public string RunwayTo { get; set; }

    }
}



       


