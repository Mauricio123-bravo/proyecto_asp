using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class Followup
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Display(Name = "Care Staff")]
        public int Idcarestaff { get; set; }
        [ForeignKey("Idcarestaff")]
        public CareStaff CareStaff { get; set; }


        [Display(Name = "State")]
        public int Idstate{ get; set; }
        [ForeignKey("Idstate")]
        public State State { get; set; }


        [Display(Name = "PQRS")]
        public int Idpqrs { get; set; }
        [ForeignKey("Idpqrs")]
        public Pqrs Pqrs { get; set; }

    }
}
