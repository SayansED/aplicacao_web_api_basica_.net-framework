using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebAppClientes.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string nomeCompleto { get; set; }
        public string cpf { get; set; }

        public List<Cliente> ListarClientes(int? id = null)
        {
            try
            {
                var clienteDB = new ClienteDAO();
                return clienteDB.ListarClientesDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Clientes: Erro => {ex.Message}");
            }
        }

        public void Inserir(Cliente cliente)
        {
            try
            {
                var clienteDB = new ClienteDAO();
                clienteDB.InserirClienteDB(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir Cliente: Erro => {ex.Message}");
            }
        }

        public void Atualizar(Cliente cliente)
        {
            try
            {
                var clienteDB = new ClienteDAO();
                clienteDB.AtualizarClienteDB(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar Cliente: Erro => {ex.Message}");
            }
        }

        public Cliente Atualizar(int id, Cliente Cliente)
        {
            var listaClientes = this.ListarClientes();
            var itemIndex = listaClientes.FindIndex(p => p.id == id);

            if (itemIndex >= 0)
            {
                Cliente.id = id;
                listaClientes[itemIndex] = Cliente;
            }
            else
            {
                return null;
            }

            RescreverArquivo(listaClientes);
            return Cliente;
        }

        public void Deletar(int id)
        {
            try
            {
                var clienteDB = new ClienteDAO();
                clienteDB.DeletarClienteDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar Cliente: Erro => {ex.Message}");
            }
        }

        public bool RescreverArquivo(List<Cliente> listaClientes)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaClientes, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
            return true;
        }
    }
}