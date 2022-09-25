using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
