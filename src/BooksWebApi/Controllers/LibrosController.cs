using System.Net;
using AutoMapper;
using BooksWebApi.DataAccess.Repositories;
using BooksWebApi.Inputs;
using BooksWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroRepository _repository;
        private readonly IMapper _mapper;

        public LibrosController(ILibroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _repository.Get(id);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(LibroInput libro)
        {
            var newLibro = _mapper.Map<Libro>(libro);
            var (result, lastId) = _repository.Insert(newLibro);
            if (!result) return BadRequest();
            newLibro.LibroId = lastId;
            return new CreatedAtRouteResult(new {id = lastId}, newLibro);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, LibroInput libro)
        {
            var newLibro = _mapper.Map<Libro>(libro);
            if (_repository.Update(id, newLibro))
            {
                return Ok(newLibro);

            }
            return StatusCode((int)HttpStatusCode.NotModified);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _repository.Delete(id) ? NoContent() : StatusCode((int)HttpStatusCode.NotModified);
        }
        
    }
}
