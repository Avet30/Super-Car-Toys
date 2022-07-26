using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int EAN { get; set; }
        [Required]
        public string Maker { get; set; }
        [Required]
        [Range(1,10000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 1 - 49")]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 50 - 100")]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 100+")]
        public double Price100 { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }
        [Required]
        [Display(Name="Category")]
        public int CategoryId { get; set; } //Entity va savoir que je vais utiliser Category comme FK de mon Product
        [ForeignKey("CategoryId")] //Pas obligatoire ici car en donnant le même nom + Id Entity va automatiqument connaître la liason à créer
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        [Display(Name = "Toy type")]
        public int CarTypeId { get; set; }
        [ValidateNever]
        public CarType CarType { get; set; }

    }
}
