using System.Linq.Expressions;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IRepository
    {
        void Create(Blog item);
        void Delete(Blog item);
        bool EntityExists(int id);
        Task<IEnumerable<Blog>> GetAll();
        Task<IEnumerable<Blog>> GetByCondition(Expression<Func<Blog, bool>> expression);
        Task<Blog> GetById(int id);
        void SaveChanges();
        Task SaveChangesAsync();
        void Update(Blog item);
    }
}