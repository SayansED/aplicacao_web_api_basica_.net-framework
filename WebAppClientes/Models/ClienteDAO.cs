using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAppClientes.Models
{
    public class ClienteDAO
    {
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection conexao;

        public ClienteDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<Cliente> ListarClientesDB(int? id = null)
        {
            var listaClientes = new List<Cliente>();
            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();
                if (id == null)
                {
                    selectCmd.CommandText = "select * from Cliente";
                }
                else
                {
                    selectCmd.CommandText = $"select * from Cliente where id = {id}";
                }
                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var cl = new Cliente // injeta diretamente
                    {
                        id = Convert.ToInt32(resultado["Id"]),
                        nomeCompleto = Convert.ToString(resultado["nomeCompleto"]),
                        cpf = Convert.ToString(resultado["cpf"]),                        
                    };
                    listaClientes.Add(cl);
                }
                return listaClientes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirClienteDB(Cliente cliente)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "insert into Cliente (nomeCompleto, cpf) values (@nomeCompleto, @cpf)";

                IDbDataParameter paramNomeCompleto = new SqlParameter("nomeCompleto", cliente.nomeCompleto);
                IDbDataParameter paramCpf = new SqlParameter("cpf", cliente.cpf);             

                insertCmd.Parameters.Add(paramNomeCompleto);
                insertCmd.Parameters.Add(paramCpf);                

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void AtualizarClienteDB(Cliente cliente)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "update Cliente set nomeCompleto = @nomeCompleto, cpf = @cpf where id = @id";

                IDbDataParameter paramNomeCompleto = new SqlParameter("nomeCompleto", cliente.nomeCompleto);
                IDbDataParameter paramCpf = new SqlParameter("cpf", cliente.cpf);                

                updateCmd.Parameters.Add(paramNomeCompleto);
                updateCmd.Parameters.Add(paramCpf);                

                IDbDataParameter paramID = new SqlParameter("id", cliente.id);
                updateCmd.Parameters.Add(paramID);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void DeletarClienteDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "delete from Cliente where id = @id";

                IDbDataParameter paramID = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramID);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}