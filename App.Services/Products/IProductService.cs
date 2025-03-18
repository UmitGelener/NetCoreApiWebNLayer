using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;

namespace App.Services.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>>  GetTopPriceProductAsync(int count);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
}
