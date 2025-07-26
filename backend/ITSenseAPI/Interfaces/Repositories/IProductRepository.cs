using ITSenseAPI.DTOs;

namespace ITSenseAPI.Interfaces.Repositories
{
   public interface IProductRepository : IBaseRepository<ProductDto, GetProductDto>
   {
      new Task<IEnumerable<GetProductDto>> GetAllActiveAsync();
      new Task<GetProductDto?> GetByIdAsync(int id);
      new Task<GetProductDto> CreateAsync(ProductDto dto);
      new Task<GetProductDto?> UpdateAsync(int id, ProductDto dto);
      Task<bool> SetAsFaultyAsync(int id);
      Task<(bool Success, string Message)> MoveToOutputAsync(MoveProductDto dto);
      new Task<bool> SoftDeleteAsync(int id);
   }
}