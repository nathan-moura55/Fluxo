namespace Estoque.Dominio.Models
{
    public enum TipoUsuario 
    { 
        Pessoa = 0, 
        Empresa = 1 
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty; 
        public TipoUsuario Tipo { get; set; }

        public string? CPF { get; set; }
        public string? CNPJ { get; set; }
        public string? NomeFantasia { get; set; }
    }
}