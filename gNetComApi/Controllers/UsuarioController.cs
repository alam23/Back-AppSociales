﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gNetComApi.Models;
using Microsoft.EntityFrameworkCore;
using gNetComApi.Dto;

namespace gNetComApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly josericardo3_Context _db;

        public UsuarioController(josericardo3_Context db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("listUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _db.Users.ToListAsync();

            if (lista.Count > 0)
            {
                var result = new List<UserDto>();
                foreach (var item in lista)
                {
                    var userDto = new UserDto();
                    userDto.CellNumber = item.CellNumber;
                    userDto.Name = item.Name;
                    userDto.UserId = item.UserId;
                    userDto.UserName = item.UserName;
                    userDto.LastName = item.LastName;
                    userDto.Password = item.Password;
                    result.Add(userDto);
                }
                return Ok(result);
            }
            else
            {
                return Ok("No hay Usuarios");
            }

            
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] newUserDto usuario)
        {
            if (usuario == null)
            {
                return BadRequest("ingrese datos correctos");
            }
            var validar = await _db.Users.OrderByDescending(t => t.UserId).FirstOrDefaultAsync();
            if (validar.UserName == usuario.UserName)
            {
                return BadRequest("Usuario ya existente, por favor cambiar UserName");
            }

            if (validar != null)
            {
                var nuevoUsuarioBD = new User();
                nuevoUsuarioBD.UserId = (Convert.ToInt64(validar.UserId) + 1).ToString();
                nuevoUsuarioBD.UserName = usuario.UserName;
                nuevoUsuarioBD.Password = usuario.Password;
                nuevoUsuarioBD.Name = usuario.Name;
                nuevoUsuarioBD.LastName = usuario.LastName;
                nuevoUsuarioBD.CellNumber = usuario.CellNumber;

                await _db.AddAsync(nuevoUsuarioBD);
                await _db.SaveChangesAsync();

                var newUser = await _db.Users.FirstOrDefaultAsync(x => x.UserId == nuevoUsuarioBD.UserId);

                return Ok(newUser);
            }
            else
            {
                var nuevoUsuarioBD = new User();
                nuevoUsuarioBD.UserId = "1";
                nuevoUsuarioBD.UserName = usuario.UserName;
                nuevoUsuarioBD.Password = usuario.Password;
                nuevoUsuarioBD.Name = usuario.Name;
                nuevoUsuarioBD.LastName = usuario.LastName;
                nuevoUsuarioBD.CellNumber = nuevoUsuarioBD.CellNumber;

                await _db.AddAsync(nuevoUsuarioBD);
                await _db.SaveChangesAsync();

                var newUser = await _db.Users.FirstAsync(x => x.UserId == nuevoUsuarioBD.UserId);

                return Ok(newUser);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] userLoginDto usuario)
        {
            if (usuario == null)
            {
                return BadRequest("ingrese datos correctos");
            }
            var validar = await _db.Users.Where(t => t.UserName == t.UserName && t.Password == usuario.Password).FirstOrDefaultAsync();

            if (validar != null)
            {
                var userDto = new UserDto();
                userDto.UserId = validar.UserId;
                userDto.CellNumber = validar.CellNumber;
                userDto.Name = validar.Name;
                userDto.UserId = validar.UserId;
                userDto.UserName = validar.UserName;
                userDto.LastName = validar.LastName;
                userDto.Password = validar.Password;

                return Ok(userDto);
            }
            else
            {
               return BadRequest("Usuario y/o contraseña inválido");
            }
        }

        //    [HttpGet]
        //    [Route("listClientes")]
        //    public async Task<IActionResult> GetClientes()
        //    {
        //        var lista = await _db.Clients.ToListAsync();

        //        return Ok(lista);
        //    }
        //    [HttpGet]
        //    [Route("listAreas")]
        //    public async Task<IActionResult> getAreas()
        //    {
        //        var lista = await _db.Areas.ToListAsync();

        //        return Ok(lista);
        //    }

        //    [HttpGet("/{id}")]
        //    public async Task<IActionResult> GetUsuarioId(int id)
        //    {
        //        Usuario usuario = new Usuario();
        //        usuario.Id = id;
        //        var validar = await _db.Usuarios.Where(x => x.Id == usuario.Id).ToListAsync();
        //        if (validar.Count() > 0)
        //        {
        //            return Ok(validar.FirstOrDefault());
        //        }
        //        return BadRequest(ModelState);
        //    }
    }
}