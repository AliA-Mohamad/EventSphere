using EventSphere.Data;
using EventSphere.Models;
using EventSphere.Models.DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace EventSphere.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public UsuariosController(ApplicationDbContext db)
    {
        _db = db;
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
            Senha = dto.Senha
        };

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();

        return Ok("Usuario Registrado com sucesso");
    }

}
