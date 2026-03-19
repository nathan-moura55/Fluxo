using Microsoft.AspNetCore.Mvc;
using Estoque.Dominio.Interfaces;
using Estoque.Dominio.Models;
using Microsoft.AspNetCore.Authorization;

namespace Estoque.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IControleDeEstoque _servico;

    public ProdutoController(IControleDeEstoque servico)
    {
        _servico = servico;
    }

    
    [HttpGet("todos")]
    public IActionResult Get() => Ok(_servico.ListarTodos());

    
    [HttpGet("{id}/buscar")]
    public IActionResult Buscar(int id)
    {
        try {
            var resultado = _servico.BuscarProduto(id);
            return Ok(resultado);
        } catch (Exception ex) {
            return NotFound(new { erro = ex.Message });
        }
    }

    
    [HttpPost("adicionar")]
    public IActionResult Post([FromQuery] string nome, [FromQuery] int quantidade, [FromQuery] int estoqueMinimo)
    {
        try {
            _servico.AdicionarProduto(nome, quantidade, estoqueMinimo);
            return Ok(new { mensagem = "Produto criado!" });
        } catch (Exception ex) {
            return BadRequest(new { erro = ex.Message });
        }
    }

    
    [HttpPost("{id}/entrada")]
    public IActionResult Entrada(int id, [FromQuery] int quantidade)
    {
        try {
            _servico.EntradaEstoque(id, quantidade);
            return Ok(new { mensagem = "Quantidade somada!" });
        } catch (Exception ex) {
            return BadRequest(new { erro = ex.Message });
        }
    }

    
    [HttpPost("{id}/retirar")]
    public IActionResult Retirar(int id, [FromQuery] int quantidade)
    {
        try {
            _servico.RegistrarSaidaEstoque(id, quantidade);
            return Ok(new { mensagem = "Retirada realizada!" });
        } catch (Exception ex) {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("{id}/deletar")]
    public IActionResult Deletar(int id)
    {
        try {
            _servico.RemoverProduto(id);
            return Ok(new { mensagem = "Produto excluído!" });
        } catch (Exception ex) {
            return BadRequest(new { erro = ex.Message });
        }
    }
}