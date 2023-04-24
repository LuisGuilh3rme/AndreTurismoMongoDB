using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TurismoMongoDB.Models;
using TurismoMongoDB.Services;

namespace TurismoMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public ActionResult<List<Endereco>> Get() => _enderecoService.Get();


        [HttpGet("{id:length(24)}", Name = "GetEndereco")]
        public ActionResult<Endereco> Get(string id)
        {
            if (id == null) return NotFound();

            Endereco endereco = _enderecoService.Get(id);
            if (endereco == null) return NotFound();

            return endereco;
        }

        [HttpPost]
        public ActionResult<Endereco> Post(Endereco endereco)
        {
            if (endereco.Id == "string") endereco.Id = String.Empty;
            if (endereco.cidade.Id == "string" || endereco.cidade.Id == String.Empty) endereco.cidade.Id = BsonObjectId.GenerateNewId().ToString();

            _enderecoService.Post(endereco);
            return CreatedAtRoute("GetCidade", new { id = endereco.Id }, endereco);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<Endereco> Update(Endereco endereco)
        {
            var c = _enderecoService.Get(endereco.Id);
            if (c == null) return NotFound();

            _enderecoService.Update(endereco);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            if (id == null) return NotFound();

            _enderecoService.Delete(id);
            return Ok();
        }

    }
}
