namespace ITSenseAPI.Interfaces.Repositories;

public interface IBaseRepository<TEntity, TDto>
{
   Task<IEnumerable<TDto>> GetAllActiveAsync();
   Task<TDto?> GetByIdAsync(int id);
   Task<TDto> CreateAsync(TEntity dto);
   Task<TDto?> UpdateAsync(int id, TEntity dto);
   Task<bool> SoftDeleteAsync(int id);
}
