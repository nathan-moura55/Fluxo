using Estoque.Dominio.Interfaces;
using Estoque.Dominio.Models;
using Estoque.Repositorio.Data;
using Microsoft.EntityFrameworkCore; 

namespace Estoque.Repositorio;

public class ProdutoRepositorioSql : IProdutoRepositorio
{
    private readonly AppDbContext _context;

    public ProdutoRepositorioSql(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> ObterTodos()
    {
        return _context
        .Produtos
        .AsNoTracking();
    }

    public Produto? ObterPorId(int id) => _context.Produtos.Find(id);

    public void Adicionar(Produto produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();
    }

    public void Atualizar(Produto produto)
    {
        _context.Produtos.Update(produto);

        _context.SaveChanges();
    }
    public void Remover(int id)
    {
        var produto = _context.Produtos.Find(id);

        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
    }
}
