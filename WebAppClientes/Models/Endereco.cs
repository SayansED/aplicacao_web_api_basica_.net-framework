using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebAppClientes.Models
{
    public class Endereco
    {
        private object listaClientes;

        public int id { get; set; }
        public int idCliente { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }

        public List<Endereco> ListarEnderecos(int? id = null)
        {
            try
            {
                var enderecoDB = new EnderecoDAO();
                return enderecoDB.ListarEnderecosDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Enderecos: Erro => {ex.Message}");
            }
        }

        public void Inserir(Endereco endereco)
        {
            try
            {
                var enderecoDB = new EnderecoDAO();
                enderecoDB.InserirEnderecoDB(endereco);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir Endereco: Erro => {ex.Message}");
            }
        }

        public void Atualizar(Endereco endereco)
        {
            try
            {
                var enderecoDB = new EnderecoDAO();
                enderecoDB.AtualizarEnderecoDB(endereco);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar Endereco: Erro => {ex.Message}");
            }
        }

        public Endereco Atualizar(int id, Endereco Endereco)
        {
            var listaEnderecos = this.ListarEnderecos();
            var itemIndex = listaEnderecos.FindIndex(p => p.id == id);

            if (itemIndex >= 0)
            {
                Endereco.id = id;
                listaEnderecos[itemIndex] = Endereco;
            }
            else
            {
                return null;
            }

            RescreverArquivo(listaEnderecos);
            return Endereco;
        }

        public void Deletar(int id)
        {
            try
            {
                var enderecoDB = new EnderecoDAO();
                enderecoDB.DeletarEnderecoDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar Endereco: Erro => {ex.Message}");
            }
        }

        public bool RescreverArquivo(List<Endereco> listaEnderecos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaClientes, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
            return true;
        }
    }
}