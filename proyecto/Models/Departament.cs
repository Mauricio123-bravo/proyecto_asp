using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class Departament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //RelationShips
        public List<CareStaff> CareStaffs { get; set; }
    }
}
