using EventSphere.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EventSphere.Services;

public class PasswordService : IPasswordService
{
    public string GerarHashSenha(string senha)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public bool VerificarSenha(string senhaDigitada, string senhaHashArmazenada)
    {
        string hashSenhaDigitada = GerarHashSenha(senhaDigitada);
        return hashSenhaDigitada == senhaHashArmazenada;
    }
}
