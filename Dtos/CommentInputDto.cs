using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Dtos
{
    public class CommentInputDto
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
