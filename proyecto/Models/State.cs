using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class State
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(20, ErrorMessage = "Name lenght must be between 3 and 20", MinimumLength = 3)]
        public string Name { get; set; }

        //RelationShips
        //go out
        public List<Followup> Followups { get; set; }

    }
}
