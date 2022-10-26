using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDto : IServiceWithDto<Product, ProductDto>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWitCategory();

        Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto);

        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto);
    }
}