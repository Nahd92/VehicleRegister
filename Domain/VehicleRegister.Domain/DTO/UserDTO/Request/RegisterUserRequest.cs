using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleRegister.Domain.DTO.UserDTO.Request
{
   public class RegisterUserRequest
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "UserName")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        public string Password { set; get; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { set; get; }
    }
}
