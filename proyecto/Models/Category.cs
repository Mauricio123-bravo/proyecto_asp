using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length must be between 1 and 50", MinimumLength =1)]
        [Display(Name = "Name")]
        public string Name { get; set; }


        public List<Pqrs> Pqrs { get; set; }
        
    }
}
