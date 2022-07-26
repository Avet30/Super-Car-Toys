using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.Models
{
    public class CarType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Car Type")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
