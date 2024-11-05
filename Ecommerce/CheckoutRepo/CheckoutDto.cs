using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.CheckoutRepo
{

    public class CheckoutDto
    {

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FName is required")]
        [StringLength(100, ErrorMessage = "FName can't be longer than 100 characters")]
        public string FName { get; set; }

        [Required(ErrorMessage = "LName is required")]
        [StringLength(100, ErrorMessage = " LName can't be longer than 100 characters")]
        public string LName { get; set; }


        public Countries Country { get; set; }

        [Required(ErrorMessage = "Street number and name are required.")]
        [StringLength(100, ErrorMessage = "Street number and name cannot exceed 100 characters.")]
        public string StreetNumberandName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(20, ErrorMessage = "State name cannot exceed 20 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        [Range(10000, 99999, ErrorMessage = "Zip code must be a 5-digit number.")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a 10-digit number.")]
        public int Phone { get; set; }

        [StringLength(500, ErrorMessage = "Additional info cannot exceed 500 characters.")]
        public string Info { get; set; }
    }
}
