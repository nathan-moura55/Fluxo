using Estoque.Dominio.Models;
using Estoque.Dominio.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Estoque.Servicos
{
    public class ControleDeEstoque : IControleDeEstoque
    {
        private readonly IProdutoRepositorio _repositorio;

        public ControleDeEstoque(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarProduto(string nome, int quantidade, int estoqueMinimo)
        {
            if (_repositorio.ObterTodos().Any(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Produto com esse nome já existe.");
            }

            var novoProduto = new Produto(0, nome, quantidade, estoqueMinimo);

            _repositorio.Adicionar(novoProduto);
        }

        public void AtualizarProduto(int id, string? novoNome = null, int? novoMinimo = null)
        {
            if (BuscarProduto(id) is Produto produto)
            {
                if (!string.IsNullOrWhiteSpace(novoNome))
                {
                    produto.AtualizarNome(novoNome);
                }

                if (novoMinimo.HasValue)
                {
                    produto.AtualizarEstoqueMinimo(novoMinimo.Value);
                }

                _repositorio.Atualizar(produto);
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }

        public Produto BuscarProduto(int id)
        {
            return _repositorio.ObterPorId(id)!;
        }

        public void EntradaEstoque(int id, int quantidade)
        {
            var produto = BuscarProduto(id);
            if (produto != null)
            {
                produto.AdicionarEstoque(quantidade);
                _repositorio.Atualizar(produto);
            }
        }

        public void RegistrarSaidaEstoque(int id, int quantidade)
        {
            var produto = BuscarProduto(id);
            if (produto != null)
            {
                produto.RemoverEstoque(quantidade);
                if (produto.AbaixoDoMinimo())
                {
                    Console.WriteLine($"[ALERTA] Produto '{produto.Nome}' está abaixo do estoque mínimo.");
                }
                _repositorio.Atualizar(produto);
            }
        }

        public void RemoverProduto(int id)
        {
            var produto = _repositorio.ObterPorId(id);

            if (produto == null)
                throw new Exception("Produto não encontrado.");

            if (produto.Quantidade > 0)
                throw new Exception("Não é possível excluir um produto com estoque.");

            _repositorio.Remover(id);
        }

        public IEnumerable<Produto> ListarTodos()
        {
            return _repositorio.ObterTodos();
        }
    }
}