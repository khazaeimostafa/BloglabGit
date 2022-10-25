 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{

    public class BlogEntity
    {


        public BlogEntity()
        {
            BlogComments = new List<BlogCommentEntity> ();
        }

        public int BlogId { get; set; }


        public string ApplicationUserId { get; set; }


        public int PhotoId { get; set; }


        public string Title { get; set; }


        public string Content { get; set; }


        public DateTime PublishDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public bool ActiveInd { get; set; }

        public AppUser user { get; set; }

        public PhotoEntity photo { get; set; }
        public List<BlogCommentEntity> BlogComments { get; set; }





        #region TSQL
        //         [BlogId] INT NOT NULL IDENTITY(1,1),
        // -- Primary Key column
        // [ApplicationUserId] INT NOT NULL,
        // [PhotoId] INT NULL,
        // [Title] VARCHAR(50) NOT NULL,
        // Content VARCHAR(MAX) NOT NULL,
        // PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
        // UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
        // ActiveInd BIT NOT NULL DEFAULT CONVERT(BIT ,1)

        //     -- Specify more columns here
        //  PRIMARY KEY(BlogId),
        // FOREIGN KEY(ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId),
        // FOREIGN KEY(photoId) REFERENCES Photo(photoId)

        #endregion

    }
}
