using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurismoMongoDB.Models;
using TurismoMongoDB.Services;

namespace TurismoMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _cidadeService;

        public CidadeController(CidadeService cidadeService)
        {
            _cidadeService = cidadeService;   
        }

        [HttpGet]
        public ActionResult<List<Cidade>> Get() => _cidadeService.Get();


        [HttpGet("{id:length(24)}", Name = "GetCidade")]
        public ActionResult<Cidade> Get(string id)
        {
            if (id == null) return NotFound();

            Cidade cidade = _cidadeService.Get(id);
            if (cidade == null) return NotFound();

            return cidade;
        }

        [HttpPost]
        public ActionResult<Cidade> Post(Cidade cidade)
        {
            if (cidade.Id == "string") cidade.Id = String.Empty;

            _cidadeService.Post(cidade);
            return CreatedAtRoute("GetCidade", new { id = cidade.Id }, cidade);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<Cidade> Update(Cidade cidade)
        {
            if (_cidadeService.Get(cidade.Id) == null) return NotFound();

            _cidadeService.Update(cidade);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            if (id == null) return NotFound();

            _cidadeService.Delete(id);
            return Ok();
        }

    }
}
