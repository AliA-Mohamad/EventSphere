using EventSphere.Data;
using EventSphere.Interfaces;
using EventSphere.Models;
using EventSphere.Models.DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace EventSphere.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IPasswordService _passwordService;

    public UsuariosController(ApplicationDbContext db, IPasswordService passwordService)
    {
        _db = db;
        _passwordService = passwordService;
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarDto dto)
    {
        if (_db.Usuarios.Any(u => u.Email == dto.Email))
        {
            return BadRequest("Usuario ja existe");

        }

        var usuario = new UsuariosModel
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = _passwordService.GerarHashSenha(dto.Senha)
        };

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();

        return Ok("Usuario Registrado com sucesso");
    }

}
