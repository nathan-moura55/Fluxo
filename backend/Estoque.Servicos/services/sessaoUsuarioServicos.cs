using Estoque.Dominio.Models;
using Estoque.Dominio.Interfaces;
using System;

namespace Estoque.Servicos
{
    public class UsuarioServico
    {
        private readonly IUsuarioRepositorio _repo;

        public UsuarioServico(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }

        public void Registrar(Usuario novoUsuario)
        {
            if (_repo.EmailJaExiste(novoUsuario.Email))
                throw new Exception("Este e-mail já está em uso.");

            if (novoUsuario.Tipo == TipoUsuario.Pessoa && string.IsNullOrEmpty(novoUsuario.CPF))
                throw new Exception("CPF é obrigatório para Pessoa Física.");

            if (novoUsuario.Tipo == TipoUsuario.Empresa && string.IsNullOrEmpty(novoUsuario.CNPJ))
                throw new Exception("CNPJ é obrigatório para Empresas.");

            _repo.Cadastrar(novoUsuario);
        }
    }
}