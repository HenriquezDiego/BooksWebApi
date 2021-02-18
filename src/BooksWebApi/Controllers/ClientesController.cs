﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using BooksWebApi.DataAccess.Data;
using BooksWebApi.Inputs;
using BooksWebApi.Models.Entities;
using BooksWebApi.WiewModels;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IConnectionService _connectionService;
        private readonly IMapper _mapper;

        public ClientesController(IConnectionService connectionService,
            IMapper mapper)
        {
            _connectionService = connectionService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var sqlCommand = new SqlCommand("Select * from Clientes", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var clientes = new List<Cliente>();

            using(var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        ClienteId = int.Parse(reader[0]?.ToString() ?? "0"),
                        Nombres = reader[1].ToString(),
                        Apellidos =  reader[2].ToString(),
                        FechaNacimiento = DateTime.Parse(reader[3].ToString() ?? "2021-2-18"),
                        Telefono = reader[4].ToString(),
                        Email = reader[5].ToString()
                    });
                }
            }

            _connectionService.Close();
            return Ok(_mapper.Map<IEnumerable<ClienteViewModel>>(clientes));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClienteInput cliente)
        {
            var command = new SqlCommand
            {
                CommandText = "INSERT INTO Clientes VALUES(@clienteId,@nombres,@apellidos,@fecha,@telefono,@email)"
            };

            var sqlCommand = new SqlCommand("select top(1) clienteId from Clientes order by clienteId DESC", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();
            var lastId = 0;
            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    lastId = int.Parse(reader[0].ToString()??"0");
                    lastId++;
                }
            }


            command.Parameters.AddWithValue("@clienteId",lastId);
            command.Parameters.AddWithValue("@nombres", cliente.Nombres);
            command.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
            command.Parameters.AddWithValue("@fecha", cliente.FechaNacimiento);
            command.Parameters.AddWithValue("@telefono", cliente.Telefono);
            command.Parameters.AddWithValue("@email", cliente.Email);
            command.Connection = _connectionService.GetConnection();


            var executeNonQuery = command.ExecuteNonQuery();
            if (executeNonQuery <= 0) return BadRequest();
            _connectionService.Close();
            return Ok();

        }
    }
}
