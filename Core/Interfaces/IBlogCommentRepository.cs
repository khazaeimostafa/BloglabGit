using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBlogCommentRepository
    {
  
     public   Task<BlogComment> UpsertAsync(BlogCommentCreate blogCreate, string applicationUserId);

     public   Task<List<BlogComment>> GetAllAsync(int blogId);
 
     public   Task<BlogComment> GetAsync(int blogCommentId);

      public  Task<int> DeleteAsync(int blogCommentId);

    }
}
