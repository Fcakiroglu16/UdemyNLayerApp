using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IServiceWithDto<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);

        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();

        Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);

        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);

        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);

        Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);

        Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto dto);

        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
    }
}