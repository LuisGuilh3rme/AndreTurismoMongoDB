using Microsoft.AspNetCore.Mvc;
using TurismoMongoDB.Models;
using TurismoMongoDB.Services;

namespace TurismoMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _enderecoService;
        private readonly CidadeService _cidadeService;

        public EnderecoController(EnderecoService enderecoService, CidadeService cidadeService)
        {
            _enderecoService = enderecoService;
            _cidadeService = cidadeService;
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

        [HttpPost(Name = "PostCidade")]
        public ActionResult<Endereco> Post(Endereco endereco)
        {
            endereco.cidade = _cidadeService.Post(endereco.cidade);

            _enderecoService.Post(endereco);
            return CreatedAtRoute("GetEndereco", new { id = endereco.Id }, endereco);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<Endereco> Update(string id, Endereco endereco)
        {
            if (id == null) return NotFound();
            endereco.Id = id;

            if (_enderecoService.Get(endereco.Id) == null) return NotFound();

            if (endereco.cidade.Id != "string")
                endereco.cidade = _cidadeService.Get(endereco.cidade.Id);
            
            if (endereco.cidade == null) return NotFound();

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
