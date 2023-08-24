using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
        [Range(1,100,ErrorMessage ="Display Order must be between 1 and 100 only")]
        public int DisplayOrder { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        [NotMapped]
        public IFormFile clientFile { get; set; }
        public string? imagePath { get; set; }
    }
}
