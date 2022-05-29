using System.ComponentModel.DataAnnotations;
namespace Laba2.Models
{
    public class Plane
    {
        public Plane()
        {
            Flights = new List<Flight>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Максимальна кількість пасажирів")]
        public int MaxPassAmount { get; set; }

        public virtual ICollection<Flight>? Flights { get; set; }
    }

    public class PlaneShort
    {
        public PlaneShort()
        {
            Flights = new List<int>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Максимальна кількість пасажирів")]
        public int MaxPassAmount { get; set; }

        public virtual ICollection<int>? Flights { get; set; }

    }
    public class PlaneWrite
    {
        [Required]
        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Максимальна кількість пасажирів")]
        public int MaxPassAmount { get; set; }
    }
}
