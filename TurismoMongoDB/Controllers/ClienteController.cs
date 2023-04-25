using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TurismoMongoDB.Models;
using TurismoMongoDB.Services;

namespace TurismoMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly EnderecoService _enderecoService;
        private readonly CidadeService _cidadeService;

        public ClienteController(ClienteService clienteService, EnderecoService enderecoService, CidadeService cidadeService)
        {
            _clienteService = clienteService;
            _enderecoService = enderecoService;
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public ActionResult<List<Cliente>> Get() => _clienteService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public ActionResult<Cliente> Get(string id)
        {
            if (id == null) return NotFound();

            Cliente cliente = _clienteService.Get(id);
            if (cliente == null) return NotFound();

            return cliente;
        }

        [HttpPost]
        public ActionResult<Cliente> Post(Cliente cliente)
        {
            cliente.endereco.cidade = _cidadeService.Post(cliente.endereco.cidade);
            cliente.endereco = _enderecoService.Post(cliente.endereco);
            _clienteService.Post(cliente);

            return CreatedAtRoute("GetCliente", new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<Cliente> Update(Cliente cliente)
        {
            var c = _clienteService.Get(cliente.Id);
            if (c == null) return NotFound();

            _clienteService.Update(cliente);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            if (id == null) return NotFound();

            _clienteService.Delete(id);
            return Ok();
        }
    }
}
