using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _services;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public ProductsController(IProductService services, ICategoryService categoryService, IMapper mapper)
        {
            _services = services;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _services.GetProductsWitCategory());
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)

        {


            if (ModelState.IsValid)
            {
                await _services.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name");
            return View();
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _services.GetByIdAsync(id);


            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name",product.CategoryId);

            return View(_mapper.Map<ProductDto>(product));

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if(ModelState.IsValid)
            {

                await _services.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));

            }

            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);

        }


        public async Task<IActionResult>  Remove(int id)
        {
            var product = await _services.GetByIdAsync(id);


             await _services.RemoveAsync(product);

            return RedirectToAction(nameof(Index));
        }

    }
}
