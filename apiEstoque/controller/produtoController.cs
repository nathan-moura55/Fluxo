using Microsoft.AspNetCore.Mvc;
using Estoque.Dominio.Models;
using Estoque.Servicos;
using Estoque.Dominio.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using SQLitePCL;

namespace Estoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IControleDeEstoque _estoqueService;

    public ProdutoController(IControleDeEstoque estoqueService)
    {
        _estoqueService = estoqueService;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_estoqueService.ListarTodos());


    [HttpPost("adicionar")]
    public IActionResult Post(string nome, int quantidade, int estoqueMinimo)
    {
        try
        {
            _estoqueService.AdicionarProduto(nome, quantidade, estoqueMinimo);

            return Ok(new { mensagem = "Produto cadastrado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet("{id}/buscar")]
    public IActionResult Buscar(int id)
    {
        try
        {
            var produto = _estoqueService.BuscarProduto(id);

            if (produto == null)
                return NotFound(new { Mensagem = "produto não encontrado no sistema" });
            return Ok(produto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }

    }
    [HttpPut("{id}/atualizar")]
    public IActionResult Atualizar(int id, string? nome = null, int? estoqueMinimo = null)
    {
        try
        {
            _estoqueService.AtualizarProduto(id, nome, estoqueMinimo);

            return Ok(new { mensagem = "Produto atualizado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPatch("{id}/entrada")]
    public IActionResult Entrada(int id, [FromBody] int qtd)
    {
        _estoqueService.EntradaEstoque(id, qtd);
        return Ok();
    }

    [HttpPatch("{id}/saida")]
    public IActionResult saida(int id, [FromBody] int qtd)
    {
        _estoqueService.RegistrarSaidaEstoque(id, qtd);
        return Ok();
    }

    [HttpDelete("{id}/deletar")]
    public IActionResult deletar(int id)
    {
        _estoqueService.RemoverProduto(id);
        return Ok();
    }
}