using Projeto3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto3.Dados.Interface
{
    public interface IClientePersistence
    {
        List<Cliente> ObterListaClientes();
        Cliente ObterClientePorId(int id);
        void InserirCliente(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
        void ExcluirCliente(Cliente cliente);
    }
}
