using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    internal class ApplicationUserCreate : ApplicationUserLogin
    {

        [MinLength(10, ErrorMessage = "Must be at least 5-20 characters")]
        [MaxLength(10, ErrorMessage = "Must be at least 5-20 characters")]

        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MinLength(10, ErrorMessage = "Must be at least 5-20 characters")]
        [MaxLength(10, ErrorMessage = "Must be at least 5-20 characters")]

        public string Email { get; set; }


    }
}
