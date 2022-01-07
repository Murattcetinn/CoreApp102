using AutoMapper;
using CoreApp102.Api.Filters;
using CoreApp102.Core.Models;
using CoreApp102.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp102.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IService<Person> _perService;
        private IMapper _mapper;
        public PersonsController(IService<Person> perService, IMapper mapper)
        {
            _perService = perService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var per = await _perService.GetAllAsync();
            return Ok
                (_mapper.Map<IEnumerable<Person>>(per));

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {

            var per = await _perService.GetByIdAsync(id);
            return Ok(_mapper.Map<Person>(per));
        }
        [HttpPost]
        public async Task<IActionResult> Save(Person per)
        {
            var newPro = await _perService.AddAsync(_mapper.Map<Person>(per));
            return Created(string.Empty, _mapper.Map<Person>(newPro));
        }

        [HttpPut]
        public IActionResult Update(Person per)
        {
            if (string.IsNullOrEmpty(per.Id.ToString()) || per.Id == 0)
            {
                throw new Exception("Id alani zorunludur.");
            }
            var pro = _perService.Update(_mapper.Map<Person>(per));
            return Ok(_mapper.Map<Person>(pro));
            // return NoContent();  //ikiside kullanilabilir
        }

        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id) //asenkron olmayan bir yapida asenkronu kullanmak icin result kullandik.
        {
            var pro = _perService.GetByIdAsync(id).Result;
            _perService.Remove(pro);
            return NoContent();
        }
    }
}
