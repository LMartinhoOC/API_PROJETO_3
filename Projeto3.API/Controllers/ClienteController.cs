using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto3.API.Models;
using Projeto3.Dados.Interface;
using Projeto3.Entidades;
using System.Reflection;

namespace Projeto3.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClientePersistence _clientePersistence;

        public ClienteController(IClientePersistence clientePersistence)
        {
            this._clientePersistence = clientePersistence;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetClientes()
        {
            try
            {
                List<Cliente> clientes = this._clientePersistence
                    .ObterListaClientes();

                List<ClienteViewModel> lista = new List<ClienteViewModel>();

                foreach (Cliente cliente in clientes)
                {
                    lista.Add(new ClienteViewModel()
                    {
                        ID         = cliente.ID,
                        NOME       = cliente.NOME,
                        EMAIL      = cliente.EMAIL,
                        NASCIMENTO = cliente.NASCIMENTO.ToString("dd/MM/yyyy"),
                        CPF        = cliente.CPF,
                        ENDERECO   = cliente.ENDERECO
                    });
                }

                return Ok(lista);

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetCliente(int id)
        {
            try
            {
                Cliente cliente = this._clientePersistence
                    .ObterClientePorId(id);
                    
                if (cliente == null)
                {
                    return NotFound();
                }
                else
                {
                    ClienteViewModel model = new ClienteViewModel() 
                    {
                        ID         = cliente.ID,
                        NOME       = cliente.NOME,
                        EMAIL      = cliente.EMAIL,
                        NASCIMENTO = cliente.NASCIMENTO.ToString("dd/MM/yyyy"),
                        CPF        = cliente.CPF,
                        ENDERECO   = cliente.ENDERECO

                    };

                    return Ok(model);
                }              

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }



        [HttpPost]
        [Route("")]
        public ActionResult InserirCliente(ClienteViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    Cliente cliente = new Cliente()
                    {
                        NOME       = model.NOME,
                        EMAIL      = model.EMAIL,
                        NASCIMENTO = Convert.ToDateTime(model.NASCIMENTO),
                        CPF        = model.CPF,
                        ENDERECO   = model.ENDERECO
                    };

                    this._clientePersistence.InserirCliente(cliente);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        [HttpPut]
        [Route("")]
        public ActionResult AtualizarCliente(ClienteViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    Cliente cliente = new Cliente()
                    {
                        ID         = model.ID,
                        NOME       = model.NOME,
                        EMAIL      = model.EMAIL,
                        NASCIMENTO = Convert.ToDateTime(model.NASCIMENTO),
                        CPF        = model.CPF,
                        ENDERECO   = model.ENDERECO
                    };

                    this._clientePersistence.AtualizarCliente(cliente);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult ExcluirCliente(int id)
        {
            try
            {
                Cliente cliente = this._clientePersistence
                    .ObterClientePorId(id);

                if (cliente == null)
                {
                    return NotFound();
                }
                else
                {
                    this._clientePersistence
                        .ExcluirCliente(cliente);

                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
