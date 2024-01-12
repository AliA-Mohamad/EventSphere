using EventSphere.Data;
using EventSphere.Interfaces;
using EventSphere.Models;
using EventSphere.Models.DTOs.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventSphere.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;

    public UsuariosController(ApplicationDbContext db, IPasswordService passwordService, ITokenService tokenService)
    {
        _db = db;
        _passwordService = passwordService;
        _tokenService = tokenService;
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

    [HttpPost("login")]
    public IActionResult login([FromBody] LoginDto loginDto)
    {
        var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == loginDto.Email);

        if (usuario == null || !_passwordService.VerificarSenha(loginDto.Senha, usuario.Senha))
        {
            return Unauthorized("Credencais invalidas.");
        }

        var token = _tokenService.GerarToken(usuario);
        return Ok(new { Token = token });
    }

    [Authorize]
    [HttpPost("AlterarSenha")]
    public IActionResult AlterarSenha([FromBody] AlterarSenhaDto alterarSenhaDto)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var usuario = _db.Usuarios.FirstOrDefault(u => u.Email ==  userEmail);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        if (!_passwordService.VerificarSenha(alterarSenhaDto.SenhaAtual, usuario.Senha))
        {
            return BadRequest("Senha atual incorreta.");
        }

        usuario.Senha = _passwordService.GerarHashSenha(alterarSenhaDto.NovaSenha);
        _db.SaveChanges();

        return Ok("Senha alterada com sucesso.");
    }
}
