using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class ContactViewModel
    {

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adresa de email este obligatorie.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Numarul de telefon este obligatoriu.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Mesajul este obligatoriu.")]
        public string Message { get; set; }

    }
}
