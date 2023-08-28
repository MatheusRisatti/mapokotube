using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapokoTube.Models
{
    [Table("Video")]
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O Nome deve possuir no máximo 100 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }
        
        [Display(Name = "Data de Upload")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Duração (em minutos)")]
        public Int16 Duration { get; set; }

        [StringLength(200)]
        [Display(Name = "Thumbnail")]
        public string Thumbnail { get; set; }


        [Required(ErrorMessage = "O caminho do arquivo de vídeo é obrigatório")]
        [StringLength(200)]
        [Display(Name = "Arquivo de Vídeo")]
        public string VideoFile { get; set; }

        [Display(Name = "Tags")]
        public List<Tag> Tags { get; set; }
    }
}
