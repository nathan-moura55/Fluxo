namespace Estoque.Dominio.Interfaces;
using Estoque.Dominio.Models;
using System.Collections.Generic;

public interface IControleDeEstoque
{
    void AdicionarProduto(string nome, int quantidade, int estoqueMinimo);
    Produto BuscarProduto(int id);
    void AtualizarProduto(int id, string? nome = null, int? estoqueMinimo = null);
    void EntradaEstoque(int id, int quantidade);
    void RegistrarSaidaEstoque(int id, int quantidade);
    void RemoverProduto(int id);
    IEnumerable<Produto> ListarTodos(); 
}