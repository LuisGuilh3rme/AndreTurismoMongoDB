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

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
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
            if (cliente.Id == "string") cliente.Id = String.Empty;
            if (cliente.endereco.Id == "string" || cliente.endereco.Id == String.Empty) cliente.endereco.Id = BsonObjectId.GenerateNewId().ToString();
            if (cliente.endereco.cidade.Id == "string" || cliente.endereco.cidade.Id == String.Empty) cliente.endereco.cidade.Id = BsonObjectId.GenerateNewId().ToString();

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
