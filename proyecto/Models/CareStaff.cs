using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class CareStaff
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(60, ErrorMessage = "Full name lenght must be between 3 and 60", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        //RelationShips
        //get in
        public int Iddepartament { get; set; }
        [ForeignKey("Iddepartament")]
        public Departament Departament { get; set; }

        //go out
        public List<Followup> Followups { get; set; }

    }
}
