using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ApplicationUserLogin
    {


        [Required(ErrorMessage = "username is required")]
        [MinLength(10, ErrorMessage = "Must be at least 5-20 characters")]
        [MaxLength(10, ErrorMessage = "Must be at least 5-20 characters")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(10, ErrorMessage = "Must be at least 5-20 characters")]
        [MaxLength(10, ErrorMessage = "Must be at least 5-20 characters")]

        public string Password { get; set; }
    }
}
