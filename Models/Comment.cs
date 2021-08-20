using System.ComponentModel.DataAnnotations;
namespace A1.Models {
    public class SiteComments
    {
        [Key]
        public int Id { get; set; }
        public string Time { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string Name { get; set; }
        public string IP { get; set; }
    }
}
