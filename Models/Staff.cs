using System.ComponentModel.DataAnnotations;
namespace A1.Models {
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Tel { get; set; }
        public string Url { get; set; }
        public string Research { get; set; }
    }
}
