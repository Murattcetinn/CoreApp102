using AutoMapper;
using CoreApp102.Core.Models;
using CoreApp102.Core.Services;
using CoreApp102.Mvc.ApiService;
using CoreApp102.Mvc.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp102.Mvc.Controllers
{
    public class CategoryApiController : Controller
    {
        //private readonly ICategoryService _categoryService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;
        public CategoryApiController(CategoryApiService categoryApiService, IMapper mapper)
        {
            _categoryApiService = categoryApiService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));

            //IEnumerable<Category> cat = await _categoryService.GetAllAsync();
            //var cat1 = (from s in cat select new { id = s.Id, name = s.Name }).ToList();            
            //return View(cat1);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryApiService.AddAsync(categoryDto);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryApiService.GetByIdAsync(id);
             return View(_mapper.Map<CategoryDto>(category));
          
        }
        //[ServiceFilter(typeof(NotFoundFilter))]
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
           await _categoryApiService.Update(categoryDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
          //  var category = _categoryService.GetByIdAsync(id).Result;
          await _categoryApiService.Remove(id);
            return RedirectToAction("Index");

        }
    }
}
