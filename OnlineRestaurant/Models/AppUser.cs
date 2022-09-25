using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string? FullName { get; set; }
    }
}
