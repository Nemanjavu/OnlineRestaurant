using OnlineRestaurant.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurant.Models
{
    public class MenuItem:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        //public virtual MenuCategory MenuCategory { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey ("CategoryId")]
        public MenuCategory MenuCategory { get; set; }
    }
}