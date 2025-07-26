using ITSenseAPI.Context;

namespace ITSenseAPI.Repositories;
public abstract class BaseRepository<TEntity>(ApplicationDbContext context) where TEntity : class
{
   protected readonly ApplicationDbContext _context = context;

   public async Task<bool> SoftDeleteAsync(int id)
   {
      var entity = await _context.Set<TEntity>().FindAsync(id);
      if (entity == null) return false;

      var prop = typeof(TEntity).GetProperty("IsActive");
      if (prop == null) return false;

      prop.SetValue(entity, 0);
      await _context.SaveChangesAsync();
      return true;
   }
}
