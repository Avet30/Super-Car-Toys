using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuxiPress.Models
{
    public class Category
    {
        [Key] // Id sera clé primaire et aura Identity (auto-incrémenté)
        public int Id { get; set; }
        [Required] // Name ne sera pas nullable     
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
