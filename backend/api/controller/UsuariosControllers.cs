using Microsoft.AspNetCore.Mvc;
using Estoque.Dominio.Interfaces;
using Estoque.Dominio.Models;
using Estoque.Servicos;

namespace Estoque.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repo;
        private readonly Authservicos _authServico;

        public UsuarioController(IUsuarioRepositorio repo, Authservicos authServico)
        {
            _repo = repo;
            _authServico = authServico;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _repo.ObterTodos();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var usuario = _repo.ObterPorId(id);
            if (usuario == null) return NotFound("Usuário não encontrado.");
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Usuario usuario)
        {
            try 
            {
                _authServico.Registrar(usuario);
                return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var token = _authServico.Login(login.Email, login.Senha);
            
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { mensagem = "E-mail ou senha incorretos." });
            }

            var usuario = _repo.ObterPorEmail(login.Email);

            return Ok(new { 
                mensagem = "Login bem-sucedido!", 
                token = token,
                usuario = new { 
                    id = usuario?.Id, 
                    nome = usuario?.Nome, 
                    email = usuario?.Email, 
                    tipo = usuario?.Tipo 
                } 
            });
        } 

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuario = _repo.ObterPorId(id);
            if (usuario == null) return NotFound("Usuário não existe.");

            _repo.Deletar(id);
            return Ok("Usuário removido com sucesso.");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}