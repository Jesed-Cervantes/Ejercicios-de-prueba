﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication2.Context;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] TodoUser user)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "sp_InsertUser";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Name", user.Name ?? (object)DBNull.Value));
            command.Parameters.Add(new SqlParameter("@Email", user.Email ?? (object)DBNull.Value));

            var newUserIdParam = new SqlParameter("@NewUserId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            command.Parameters.Add(newUserIdParam);

            await command.ExecuteNonQueryAsync();
            long newUserId = (long)newUserIdParam.Value;

            return Ok(new { Id = newUserId, user.Name, user.Email });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetAllUsers";
            command.CommandType = CommandType.StoredProcedure;

            var users = new List<TodoUser>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(new TodoUser
                {
                    Id = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                });
            }

            return Ok(users);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetUserById";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", id));

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var user = new TodoUser
                {
                    Id = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                };
                return Ok(user);
            }

            return NotFound("Usuario no encontrado.");
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "sp_DeleteUser";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", id));

            int rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected > 0)
            {
                return Ok("Usuario eliminado.");
            }

            return NotFound("Usuario no encontrado.");
        }
    }
}
