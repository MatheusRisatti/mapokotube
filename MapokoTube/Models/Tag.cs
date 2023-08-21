using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapokoTube.Models
{
    [Table("Tag")] // Change "Genre" to "Tag"
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Change byte to int

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome da Tag é obrigatório")]
        [StringLength(30, ErrorMessage = "O Nome deve possuir no máximo 30 caracteres")]
        public string Name { get; set; }
        
        public ICollection<VideoTag> Videos { get; set; } // Change "Movies" to "Videos"
    }
}
