using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Core.Models
{
    public class AppUser : IdentityUser
    {

        public AppUser()
        {
            Photos = new List<PhotoEntity>();
            Blogs = new List<BlogEntity>();
            BlogComments = new List<BlogCommentEntity>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

       

        public List<PhotoEntity> Photos { get; set; }
        public List<BlogEntity> Blogs { get; set; }
        public List<BlogCommentEntity> BlogComments { get; set; }
    }
}
