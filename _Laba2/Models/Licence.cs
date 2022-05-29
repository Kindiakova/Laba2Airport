using System.ComponentModel.DataAnnotations;
namespace Laba2.Models
{
    public class Licence
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Дата закінчення терміну дії ліцензії")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Пілот")]
        public int? PilotId { get; set; }

        public virtual Pilot? Pilot { get; set; }
    }
    public class LicenceShort
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Тип")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Дата закінчення терміну дії ліцензії")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Пілот")]
        public int? PilotId { get; set; }
        public virtual PilotWrite? Pilot { get; set; }

    }

    public class LicenceRead
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Дата закінчення терміну дії ліцензії")]
        public DateTime EndDate { get; set; }

    }
    public class LicenceWrite
    {
        [Required]
        [Display(Name = "Тип")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Дата закінчення терміну дії ліцензії")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Пілот")]
        public int? PilotId { get; set; }

    }
}
