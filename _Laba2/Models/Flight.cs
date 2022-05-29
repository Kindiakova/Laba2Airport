using System.ComponentModel.DataAnnotations;
namespace Laba2.Models
{
    public class Flight
    {
       
        public int Id { get; set; }
        
        [Display(Name = "Напрям")]
        public int? DirectionId { get; set; }

        [Display(Name = "Дата вильоту")]
        public DateTime? TakeOffTime { get; set; }

        [Display(Name = "Час польоту")]
        public int? FlightLenghtInMinutes { get; set; }
        
        [Display(Name = "Пілот")]
        public int? PilotId { get; set; }        

        [Display(Name = "Літак")]
        public int? PlaneId { get; set; }

        public virtual Pilot? Pilot { get; set; }
        public virtual Direction? Direction { get; set; }
        public virtual Plane? Plane { get; set; }
    }

    public class FlightShort
    {

        public int Id { get; set; }

        [Display(Name = "Напрям")]
        public int? DirectionId { get; set; }

        [Display(Name = "Дата вильоту")]
        public DateTime? TakeOffTime { get; set; }

        [Display(Name = "Час польоту")]
        public int? FlightLenghtInMinutes { get; set; }

        [Display(Name = "Пілот")]
        public int? PilotId { get; set; }

        [Display(Name = "Літак")]
        public int? PlaneId { get; set; }

        public virtual PilotWrite? Pilot { get; set; }
        public virtual DirectionWrite? Direction { get; set; }
        public virtual PlaneWrite? Plane { get; set; }
    }

    public class FlightWrite
    {
        [Display(Name = "Напрям")]
        public int? DirectionId { get; set; }

        [Display(Name = "Дата вильоту")]
        public DateTime? TakeOffTime { get; set; }

        [Display(Name = "Час польоту")]
        public int? FlightLenghtInMinutes { get; set; }

        [Display(Name = "Пілот")]
        public int? PilotId { get; set; }

        [Display(Name = "Літак")]
        public int? PlaneId { get; set; }

    }
}
