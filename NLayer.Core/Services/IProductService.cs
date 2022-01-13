using NLayer.Core.DTOs;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<List<ProductWithCategoryDto>> GetProductsWitCategory();


    }
}
