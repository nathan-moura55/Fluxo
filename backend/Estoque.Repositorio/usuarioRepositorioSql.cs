using Estoque.Dominio.Models;
using Estoque.Dominio.Interfaces;
using Estoque.Repositorio.Data;
using System.Linq; 

namespace Estoque.Repositorio
{
    public class UsuarioRepositorioSql : IUsuarioRepositorio
    {
        private readonly EstoqueDbContext _context;

        public UsuarioRepositorioSql(EstoqueDbContext context)
        {
            _context = context;
        }

        public Usuario? ObterPorId(int id) => _context.Usuarios.Find(id);

        public IEnumerable<Usuario> ObterTodos() => _context.Usuarios.ToList();

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public bool EmailJaExiste(string email)
        {
            return _context.Usuarios.Any(u => u.Email == email);
        }
        
        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public void Deletar(int id)
{
        var usuario = _context.Usuarios.Find(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
}
    }
}