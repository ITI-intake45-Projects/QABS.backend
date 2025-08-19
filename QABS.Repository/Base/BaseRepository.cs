
using Microsoft.EntityFrameworkCore;
using QABS.Infrastructure;

namespace QABS.Repository
{
    public class BaseRepository<T> where T : class
    {

        protected readonly QABSDbContext dbcontext;
        protected readonly DbSet<T> Table;

        public BaseRepository(QABSDbContext context)
        {
            dbcontext = context;
            Table = context.Set<T>();
        }

        public async Task AddAsync (T entity)
        {
            await Table.AddAsync(entity);
            await SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
            await SaveAsync();
        }

        public async Task Delete(T entity)
        {
            Table.Remove(entity);
            await SaveAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }
        public async Task<T?> GetByIdAsync(string id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await dbcontext.SaveChangesAsync();
        }




       

    }
}
