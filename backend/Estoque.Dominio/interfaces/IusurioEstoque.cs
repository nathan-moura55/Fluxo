using Estoque.Dominio.Models;

public interface IUsuarioRepositorio
{
    Usuario? ObterPorId(int id);
    Usuario? ObterPorEmail(string email); 
    IEnumerable<Usuario> ObterTodos();
    void Cadastrar(Usuario usuario); 
    bool EmailJaExiste(string email); 
    void Deletar(int id);
}