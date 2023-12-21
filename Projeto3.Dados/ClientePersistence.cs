using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Projeto3.Dados.Interface;
using Projeto3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto3.Dados
{
    public class ClientePersistence : IClientePersistence
    {
        private readonly IConfiguration _configuration;
        protected        SqlConnection  Con { get; set; }
        protected        SqlCommand     Cmd { get; set; }
        protected        SqlDataReader  Dr  { get; set; }
        protected        SqlTransaction Tr  { get; set; }

        public ClientePersistence(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        private void AbrirConexao()
        {
            if (Con == null)
            {
                Con = new SqlConnection(this._configuration.GetConnectionString("Conexao"));
                Con.Open();
            }
            else
            {
                Con.Open();
            }
        }

        private void FecharConexao()
        {
            if (Con != null)
            {
                Con.Close();
                Con = null;
            }           
        }


        public void AtualizarCliente(Cliente cliente)
        {
            try
            {
                AbrirConexao();

                string query = "UPDATE TB_CLIENTES_MENTORIA_MARCO " +
                               "  SET NOME       = @NOME          " +
                               "     ,EMAIL      = @EMAIL         " +
                               "     ,NASCIMENTO = @NASCIMENTO    " +
                               "     ,CPF        = @CPF           " +
                               "     ,ENDERECO   = @ENDERECO      " +
                               "WHERE ID = @ID";


                this.Cmd = new SqlCommand(query, Con);
                this.Cmd.Parameters.AddWithValue("@ID"        , cliente.ID);
                this.Cmd.Parameters.AddWithValue("@NOME"      , cliente.NOME);
                this.Cmd.Parameters.AddWithValue("@EMAIL"     , cliente.EMAIL);
                this.Cmd.Parameters.AddWithValue("@NASCIMENTO", cliente.NASCIMENTO);
                this.Cmd.Parameters.AddWithValue("@CPF"       , cliente.CPF);
                this.Cmd.Parameters.AddWithValue("@ENDERECO"  , cliente.ENDERECO);
                this.Cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void ExcluirCliente(Cliente cliente)
        {
            try
            {
                AbrirConexao();

                string query = "DELETE FROM TB_CLIENTES_MENTORIA_MARCO WHERE ID = @ID";

                this.Cmd = new SqlCommand(query, Con);
                this.Cmd.Parameters.AddWithValue("@ID", cliente.ID);
                this.Cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void InserirCliente(Cliente cliente)
        {
            try
            {
                AbrirConexao();

                string query = "INSERT INTO [dbo].[TB_CLIENTES_MENTORIA_MARCO] " +
                               "       (NOME                                   " +
                               "       ,EMAIL                                  " +
                               "       ,NASCIMENTO                             " +
                               "       ,CPF                                    " +
                               "       ,ENDERECO)                              " +
                               " VALUES                                        " +
                               "       (@NOME                                  " +
                               "       ,@EMAIL                                 " +
                               "       ,@NASCIMENTO                            " +
                               "       ,@CPF                                   " +
                               "       ,@ENDERECO)";         


                this.Cmd = new SqlCommand (query, Con);
                this.Cmd.Parameters.AddWithValue("@NOME"      , cliente.NOME      );
                this.Cmd.Parameters.AddWithValue("@EMAIL"     , cliente.EMAIL     );
                this.Cmd.Parameters.AddWithValue("@NASCIMENTO", cliente.NASCIMENTO);
                this.Cmd.Parameters.AddWithValue("@CPF"       , cliente.CPF       );
                this.Cmd.Parameters.AddWithValue("@ENDERECO"  , cliente.ENDERECO  );
                this.Cmd.ExecuteNonQuery();               
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public Cliente ObterClientePorId(int id)
        {
            try
            {
                AbrirConexao();

                Cliente cliente = null;

                string query = "SELECT * FROM TB_CLIENTES_MENTORIA_MARCO WHERE ID = @ID";

                this.Cmd = new SqlCommand(query, Con);
                this.Cmd.Parameters.AddWithValue("@ID", id);
                this.Dr  = Cmd.ExecuteReader();

                while (Dr.Read())
                {
                    cliente            = new Cliente();
                    cliente.ID         = (int)Dr["ID"];
                    cliente.NOME       = (string)Dr["NOME"];
                    cliente.EMAIL      = (string)Dr["EMAIL"];
                    cliente.NASCIMENTO = (DateTime)Dr["NASCIMENTO"];
                    cliente.CPF        = (string)Dr["CPF"];
                    cliente.ENDERECO   = (string)Dr["ENDERECO"];                    
                }

                Dr.Close();

                return cliente;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<Cliente> ObterListaClientes()
        {
            try
            {
                AbrirConexao();

                List<Cliente> lista = new List<Cliente>();

                string query = "SELECT * FROM TB_CLIENTES_MENTORIA_MARCO";

                this.Cmd = new SqlCommand(query, Con);
                this.Dr  = Cmd.ExecuteReader();

                while (Dr.Read()) 
                {
                    Cliente cliente    = new Cliente();
                    cliente.ID         = (int)Dr["ID"];
                    cliente.NOME       = (string)Dr["NOME"];
                    cliente.EMAIL      = (string)Dr["EMAIL"];
                    cliente.NASCIMENTO = (DateTime)Dr["NASCIMENTO"];
                    cliente.CPF        = (string)Dr["CPF"];
                    cliente.ENDERECO   = (string)Dr["ENDERECO"];

                    lista.Add(cliente);
                }

                Dr.Close();

                return lista;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}
