using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using WebAppClientes.Models;

namespace WebAppClientes.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        // GET: api/cliente/recuperar
        [HttpGet]
        [Route("recuperarcliente")]
        public IHttpActionResult RecuperarCliente()
        {
            try
            {
                Cliente cliente = new Cliente();
                return Ok(cliente.ListarClientes());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/cliente/recuperarporid
        [HttpGet]
        [Route("recuperarporid/{id:int}")]
        public Cliente RecuperarPorId(int id)
        {
            Cliente cliente = new Cliente();
            return cliente.ListarClientes(id).FirstOrDefault();
        }

        // POST: api/cliente
        [HttpPost]
        public IHttpActionResult Post(Cliente cliente)
        {
            try
            {
                Cliente _cliente = new Cliente();
                _cliente.Inserir(cliente);
                return Ok(_cliente.ListarClientes());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/cliente/1
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Cliente cliente)
        {
            try
            {
                Cliente _cliente = new Cliente();
                cliente.id = id;
                _cliente.Atualizar(cliente);
                return Ok(_cliente.ListarClientes(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/cliente/1
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Cliente _cliente = new Cliente();
                _cliente.Deletar(id);
                return Ok("Deletado com sucesso");
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}