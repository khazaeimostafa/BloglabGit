using Core.Entities;

namespace Core.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> UpsertAsync(BlogCreate blogCreate, string applicationUserId);

        Task<PagedResults<Blog>> GetAllAsync(BlogPaging blogPaging);

        Task<List<Blog>> GetAllByUserIdAsync(string applicationUserId);

        Task<List<Blog>> GetAllFamousAsync();

        Task<Blog> GetAsync(int blogId);

        Task<int> DeleteAsync(int blogId);
    }
}
