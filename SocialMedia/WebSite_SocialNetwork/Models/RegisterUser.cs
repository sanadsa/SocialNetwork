using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite_SocialNetwork.Models
{
    public class RegisterUser
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        [Compare("Password", ErrorMessage = "The passwords are unmatched")]
        public string PasswordConfirmation { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        
        [DefaultValue(false)]
        public bool IsAvilable { get; set; }
    }
}