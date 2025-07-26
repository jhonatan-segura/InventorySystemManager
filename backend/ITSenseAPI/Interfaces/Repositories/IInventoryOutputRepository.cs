using ITSenseAPI.DTOs;

namespace ITSenseAPI.Interfaces.Repositories
{
   public interface IInventoryOutputRepository : IBaseRepository<InventoryOutputDto, GetInventoryOutputDto>
   {
      new Task<IEnumerable<GetInventoryOutputDto>> GetAllActiveAsync();
      new Task<GetInventoryOutputDto?> GetByIdAsync(int id);
      new Task<GetInventoryOutputDto> CreateAsync(InventoryOutputDto dto);
      new Task<GetInventoryOutputDto?> UpdateAsync(int id, InventoryOutputDto dto);
      new Task<bool> SoftDeleteAsync(int id);
   }
}