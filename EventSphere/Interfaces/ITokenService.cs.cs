using EventSphere.Models;

namespace EventSphere.Interfaces;

public interface ITokenService
{
    string GerarToken(UsuariosModel usuario);
}
