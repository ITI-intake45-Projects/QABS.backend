
using Microsoft.EntityFrameworkCore;
using QABS.Infrastructure;
using System.Linq.Expressions;
using QABS.ViewModels;

namespace QABS.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    public class BaseRepository<T> where T : class
    {
        protected readonly QABSDbContext _dbContext;
        protected readonly DbSet<T> Table;
        

        public BaseRepository(QABSDbContext context)
        {
            _dbContext = context;
            Table = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await Table.AddAsync(entity);
                
                
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                Table.Update(entity);
                
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                Table.Remove(entity);
               
            }
            catch
            {
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await Table.FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            try
            {
                return await Table.FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                return filter != null ? Table.Where(filter) : Table;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await Table.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

 

        public async Task<PaginationVM<TViewModel>> SearchAsync<TViewModel, TKey>(
            Expression<Func<T, bool>>? filterPredicate,
            Expression<Func<T, TKey>>? orderBy,
            Expression<Func<T, TViewModel>> selector,
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {
                var query = GetSortedFilter(orderBy, filterPredicate, !descending);

                var totalCount = await query.CountAsync();

                var data = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(selector)
                    .ToListAsync();

                return new PaginationVM<TViewModel>
                {
                    Data = data,
                    TotalCount = totalCount,
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
            }
            catch
            {
                throw;
            }
        }

        protected IQueryable<T> GetSortedFilter<TKey>(
            Expression<Func<T, TKey>>? orderBy,
            Expression<Func<T, bool>>? filter,
            bool ascending = true)
        {
            try
            {
                var query = Table.AsQueryable();

                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

                return query;
            }
            catch
            {
                throw;
            }
        }
    }

}
