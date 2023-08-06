using System.ComponentModel.DataAnnotations;

namespace ProniaTask.Models
{
    public class Category:BaseEntity
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }
        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
