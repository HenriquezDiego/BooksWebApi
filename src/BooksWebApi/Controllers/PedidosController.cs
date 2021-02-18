using AutoMapper;
using BooksWebApi.DataAccess.Repositories;
using BooksWebApi.Inputs;
using BooksWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BooksWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _repository;
        private readonly IMapper _mapper;

        public PedidosController(IPedidoRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _repository.GetAllDtos();
            if (!result.Any()) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _repository.GetDto(id);
            if (result == null) return BadRequest();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(PedidoInput pedido)
        {
            var newPedido = _mapper.Map<Pedido>(pedido);
            var (result, lastId) = _repository.Insert(newPedido);
            if (!result) return BadRequest();
            newPedido.PedidoId = lastId;
            return new CreatedAtRouteResult(new {id = lastId}, newPedido);
        }
    }
}
