using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{

    
    public class BlogCommentEntity
    {

         
        public int BlogCommentId { get; set; }
        public int ParentBlogCommentId { get; set; }

        
        public int BlogId { get; set; }

        
        public string ApplicationUserId { get; set; }

        
        public string Content { get; set; }


        
        public DateTime PublishDate { get; set; }

        
        public DateTime UpdateDate { get; set; }

        
        public bool ActiveInd { get; set; }

        public AppUser user { get; set; }

        public BlogEntity Blog { get; set; }

       


    }
}
