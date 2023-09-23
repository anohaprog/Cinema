using Cinema.Binders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Models
{
    //[ModelBinder(typeof(LoginBinder))]
    public class LoginModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "Login should be more than 4 symbols")]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

    }
}