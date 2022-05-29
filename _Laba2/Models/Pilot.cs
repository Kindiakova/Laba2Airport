using System.ComponentModel.DataAnnotations;
namespace Laba2.Models
{
    public class Pilot
    {
        public Pilot()
        {
            Flights = new List<Flight>();
          
            Licences = new List<Licence>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Дата народження")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Стаж")]
        public int Experience { get; set; }

        public virtual ICollection<Licence>? Licences { get; set; }
        public virtual ICollection<Flight>? Flights { get; set; }
    }
}

namespace Laba2.Models
{
    public class PilotShort
    {
        public PilotShort()
        {
            Flights = new List<int>();

            Licences = new List<LicenceRead>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Дата народження")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Стаж")]
        public int Experience { get; set; }

        public virtual ICollection<LicenceRead>? Licences { get; set; }
        public virtual ICollection<int>? Flights { get; set; }
    }
}

namespace Laba2.Models
{
    public class PilotWrite
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Дата народження")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Стаж")]
        public int Experience { get; set; }
    }
}

