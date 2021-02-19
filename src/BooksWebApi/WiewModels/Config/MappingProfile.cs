using System;
using AutoMapper;
using BooksWebApi.Inputs;
using BooksWebApi.Models.Entities;

namespace BooksWebApi.WiewModels.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>()
                .AfterMap((x,y)=> x??= string.Empty)
                .ConstructUsing(x => x.Trim());
            CreateMap<DateTime, string>()
                .ConvertUsing(x => x.ToString(@"dd/MM/yyyy"));
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<ClienteInput, Cliente>();
            CreateMap<LibroInput, Libro>();
            CreateMap<PedidoInput, Pedido>();
        }
    }
}
