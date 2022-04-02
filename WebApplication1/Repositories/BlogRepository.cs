#nullable disable
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class BlogRepository : IRepository
    {
        private readonly EFContext _context;

        public BlogRepository(EFContext context)
        {
            _context = context;
        }

        public bool EntityExists(int id)
        {
            return _context.Set<Blog>().Any(p => p.Id == id);
        }
        public async Task<Blog> GetById(int id)
        {
            return await _context.Set<Blog>()
                .Include(b => b.Posts)
                .SingleOrDefaultAsync(b => b.Id == id);
        }
        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _context.Set<Blog>()
                .AsNoTracking()
                .Include(b => b.Posts)
                .OrderBy(p => p.Url)
                .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetByCondition(Expression<Func<Blog, bool>> expression)
        {
            return await _context.Set<Blog>().Where(expression)
                .Include(b => b.Posts)
                .OrderBy(p => p.Url)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Create(Blog item)
        {
            _context.Set<Blog>().Add(item);
        }

        public void Update(Blog item)
        {
            _context.Set<Blog>().Update(item);
        }

        public void Delete(Blog item)
        {
            _context.Set<Blog>().Remove(item);
        }
    }
}
