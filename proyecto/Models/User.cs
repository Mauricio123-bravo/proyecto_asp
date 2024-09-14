using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Full name lenght must be between 3 and 40", MinimumLength = 3)]
        [Display(Name = "Full name")]
        public string Fullname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //RelationShips
        public List<Pqrs> Pqrs { get; set; }
    }
}
