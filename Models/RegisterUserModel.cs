using System;
using System.ComponentModel.DataAnnotations;

namespace TaskIt.Web.Models
{
    public class RegisterUserModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = ("Password and Confirmation Password do not match."))]
        public string PasswordConfirmation { get; set; }
    }
}

