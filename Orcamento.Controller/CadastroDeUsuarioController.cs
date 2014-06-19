using System.Collections.Generic;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Controller
{
    public class CadastroDeUsuarioController
    {
        private Usuarios _usuarios;
        private Departamentos _departamentos;
        private TipoUsuarios _tipoUsuarios;

        public CadastroDeUsuarioController()
        {
            _usuarios = new Usuarios();
            _departamentos = new Departamentos();
            _tipoUsuarios = new TipoUsuarios();
        }

        public void Salvar(Usuario usuario)
        {
            _usuarios.Salvar(usuario);
        }

        public IList<Usuario> TodosUsuarios()
        {
            return _usuarios.Todos<Usuario>();
        }

        public Departamento ObterDepartamento(int id)
        {
            return _departamentos.Obter(id);
        }

        public IList<Departamento> CarregarTodosDepartamentos()
        {
            return _departamentos.Todos();
        }

        public IList<TipoUsuario> CarregarTodosTipoUsuario()
        {
            return _tipoUsuarios.Todos<TipoUsuario>();
        }

        public TipoUsuario ObterTipoUsuario(int Id)
        {
            return _tipoUsuarios.Obter<TipoUsuario>(Id);
        }



        public Usuario ObterUsuario(string id)
        {
            return _usuarios.ObterUsuarioPorId(int.Parse(id));
        }
    }
}
