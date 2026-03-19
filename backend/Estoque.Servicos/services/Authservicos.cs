using Estoque.Dominio.Models;
using Estoque.Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Estoque.Servicos
{
    public class Authservicos 
    {
        private readonly IUsuarioRepositorio _repo;
        private readonly IConfiguration _config;

        public Authservicos(IUsuarioRepositorio repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public string Login(string email, string senha)
        {
            var usuario = _repo.ObterPorEmail(email);

            if (usuario == null || usuario.Senha != senha) 
                return null;

            return GerarTokenJwt(usuario);
        }

        private string GerarTokenJwt(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("id", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Tipo.ToString()) 
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void Registrar(Usuario novoUsuario)
        {
            if (_repo.EmailJaExiste(novoUsuario.Email))
                throw new Exception("Este e-mail já está em uso.");

            _repo.Cadastrar(novoUsuario);
        }
    }
}