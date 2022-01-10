using AutoMapper;
using CoreApp102.Core.Models;
using CoreApp102.Core.Services;
using CoreApp102.Mvc.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp102.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<ProductDto>>(products));

           
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            await _productService.AddAsync(_mapper.Map<Product>(productDto));
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return View(_mapper.Map<ProductDto>(product));

        }
        //[ServiceFilter(typeof(NotFoundFilter))]
        [HttpPost]
        public IActionResult Update(ProductDto productDto)
        {
            _productService.Update(_mapper.Map<Product>(productDto));
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;
            _productService.Remove(product);
            return RedirectToAction("Index");

        }
    }
}
