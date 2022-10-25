using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Core.Models
{


    [Table("Photo", Schema = "dbo")]
    public class PhotoEntity
    {


        public int PhotoId { get; set; }


        public string ApplicationUserId { get; set; }

        public string PublicId { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }


        public DateTime UpdateDate { get; set; }

        public AppUser User { get; set; }
        public BlogEntity? Blog { get; set; }

    }
}
