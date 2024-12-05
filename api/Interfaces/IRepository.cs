using System.Linq.Expressions;

namespace api.Interfaces;

public interface IRepository<T, TCreateDto, TUpdateDto>
{
     Task<IEnumerable<T>>  GetAllAsync(Expression<Func<T, bool>> filter);
     Task<T?> GetAsync(int id);

     Task<T> CreateAsync(TCreateDto entity, string userId);
     Task DeleteAsync(int it);
     Task<T?> UpdateEntity(int id, TUpdateDto entity);
}