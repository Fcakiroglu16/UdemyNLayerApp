using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class Service<TEntity, TDto> : IService<TEntity, TDto> where TDto : class where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public Service(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<TDto>> AddAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(StatusCodes.Status201Created, newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> dto)
        {
            List<TEntity> entities = _mapper.Map<List<TEntity>>(dto);

            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();

            var newDtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(StatusCodes.Status201Created, newDtos);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await _repository.AnyAsync(expression);

            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, result);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await _repository.GetAll().ToListAsync();

            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);

            return CustomResponseDto<IEnumerable<TDto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<CustomResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            var dto = _mapper.Map<TDto>(entity);

            return CustomResponseDto<TDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _repository.Where(x => ids.Contains(x.Id)).ToListAsync();

            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var entities = await _repository.Where(expression).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(StatusCodes.Status200OK, dtos);
        }
    }
}