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
    [RoutePrefix("api/endereco")]
    public class EnderecoController : ApiController
    {
        // GET: api/endereco/recuperar
        [HttpGet]
        [Route("recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Endereco endereco = new Endereco();
                return Ok(endereco.ListarEnderecos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/endereco/recuperarporid
        [HttpGet]
        [Route("recuperarporid/{id:int}")]
        public Endereco RecuperarPorId(int id)
        {
            Endereco endereco = new Endereco();
            return endereco.ListarEnderecos(id).FirstOrDefault();
        }

        // POST: api/endereco
        [HttpPost]
        public IHttpActionResult Post(Endereco endereco)
        {
            try
            {
                Endereco _endereco = new Endereco();
                _endereco.Inserir(endereco);
                return Ok(_endereco.ListarEnderecos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/endereco/1
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Endereco endereco)
        {
            try
            {
                Endereco _endereco = new Endereco();
                endereco.id = id;
                _endereco.Atualizar(endereco);
                return Ok(_endereco.ListarEnderecos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/endereco/1
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Endereco _endereco = new Endereco();
                _endereco.Deletar(id);
                return Ok("Deletado com sucesso");
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}