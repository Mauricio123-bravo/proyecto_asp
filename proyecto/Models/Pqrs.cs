using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto.Models
{
    public class Pqrs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime Creation_date { get; set; }

        [Required]
        [StringLength(10000, ErrorMessage = "Description length must be between 1 and 10000", MinimumLength =1)]
        [Display(Name = "Description")]
        public string Description  { get; set; }

 
        [Display(Name = "Code")]
        public String Code { get; set; }


        [Display(Name = "Type")]
        public int Idcategory { get; set; }
        [ForeignKey("Idcategory")]
        public Category Category { get; set; }


        [Display(Name = "User")]
        public int Iduser { get; set; }
        [ForeignKey("Iduser")]
        public User User { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
