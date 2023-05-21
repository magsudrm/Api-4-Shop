using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Repositories
{
	public interface IRepository<TEntity>
	{
		public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp,params string[] includes);
		public Task<List<TEntity>> GetAllAsync(params string[] includes);
		public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp,params string[] includes);
		public void Remove(TEntity brand);
		public Task AddAsync(TEntity brand);
		public Task SaveChangesAsync();
		public IQueryable<TEntity> GetQuery(Expression<Func<TEntity,bool>> exp,params string[] includes);
	}
}
