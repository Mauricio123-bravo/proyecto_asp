using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Content length must be between 1 and 1000", MinimumLength =1)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Answer Date")]
        [DataType(DataType.Date)]
        public DateTime Answer_date { get; set; }


        [Display(Name = "Pqrs")]
        public int Idpqrs { get; set; }
        [ForeignKey("Idpqrs")]
        public Pqrs Pqrs { get; set; }

    }
}
