using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAppClientes.Models
{
    public class EnderecoDAO
    {
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection conexao;
        public EnderecoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<Endereco> ListarEnderecosDB(int? id = null)
        {
            var listaEnderecos = new List<Endereco>();
            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();
                if (id == null)
                {
                    selectCmd.CommandText = "select * from Endereco";
                }
                else
                {
                    selectCmd.CommandText = $"select * from Endereco where id = {id}";
                }
                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var en = new Endereco // injeta diretamente
                    {
                        id = Convert.ToInt32(resultado["Id"]),
                        idCliente = Convert.ToInt32(resultado["IdCliente"]),
                        endereco = Convert.ToString(resultado["endereco"]),
                        bairro = Convert.ToString(resultado["bairro"]),
                        cep = Convert.ToString(resultado["cep"]),
                    };
                    listaEnderecos.Add(en);
                }
                return listaEnderecos;
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

        public void InserirEnderecoDB(Endereco endereco)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "insert into Endereco (idCliente, endereco, bairro, cep) values (@idCliente, @endereco, @bairro, @cep)";

                IDbDataParameter paramIdCliente = new SqlParameter("idCliente", endereco.idCliente);
                IDbDataParameter paramEndereco = new SqlParameter("endereco", endereco.endereco);
                IDbDataParameter paramBairro = new SqlParameter("bairro", endereco.bairro);
                IDbDataParameter paramCep = new SqlParameter("cep", endereco.cep);                

                insertCmd.Parameters.Add(paramIdCliente);
                insertCmd.Parameters.Add(paramEndereco);
                insertCmd.Parameters.Add(paramBairro);
                insertCmd.Parameters.Add(paramCep);

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

        public void AtualizarEnderecoDB(Endereco endereco)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "update Endereco set idCliente = @idCliente, endereco = @endereco, bairro = @bairro, cep = @cep where id = @id";

                IDbDataParameter paramIdCliente = new SqlParameter("idCliente", endereco.idCliente);
                IDbDataParameter paramEndereco = new SqlParameter("endereco", endereco.endereco);
                IDbDataParameter paramBairro = new SqlParameter("bairro", endereco.bairro);
                IDbDataParameter paramCep = new SqlParameter("cep", endereco.cep);

                updateCmd.Parameters.Add(paramIdCliente);
                updateCmd.Parameters.Add(paramEndereco);
                updateCmd.Parameters.Add(paramBairro);
                updateCmd.Parameters.Add(paramCep);

                IDbDataParameter paramID = new SqlParameter("id", endereco.id);
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

        public void DeletarEnderecoDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "delete from Endereco where id = @id";

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