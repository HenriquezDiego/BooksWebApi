using System.Collections.Generic;
using BooksWebApi.DataAccess.Core;
using BooksWebApi.Models.Entities;
using BooksWebApi.Models.Models;

namespace BooksWebApi.DataAccess.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        PedidoDto GetDto(int id);
        IEnumerable<PedidoDto> GetAllDtos();
    }
}
