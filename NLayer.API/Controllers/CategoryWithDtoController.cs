using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryWithDtoController : CustomBaseController
    {
        private readonly IServiceWithDto<Category, CategoryDto> _categoryServiceWithDto;

        public CategoryWithDtoController(IServiceWithDto<Category, CategoryDto> service)
        {
            _categoryServiceWithDto = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _categoryServiceWithDto.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Get(CategoryDto category)
        {
            return CreateActionResult(await _categoryServiceWithDto.AddAsync(category));
        }
    }
}