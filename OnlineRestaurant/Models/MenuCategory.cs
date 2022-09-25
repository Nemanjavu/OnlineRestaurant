using OnlineRestaurant.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Models
{
    public class MenuCategory:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
        public List<MenuItem> MenuItems { get; set; }

    }
}
    