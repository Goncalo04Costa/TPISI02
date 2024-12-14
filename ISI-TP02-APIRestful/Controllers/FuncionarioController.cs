using ISITP02.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ISI_TP02_APIRestful.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
       
        private static List<Funcionario> Funcionarios = new List<Funcionario>();

        // GET: api/funcionario
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Funcionarios);
        }

        // GET: api/funcionario/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var funcionario = Funcionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            return Ok(funcionario);
        }

        // POST: api/funcionario
        [HttpPost]
        public IActionResult Create([FromBody] Funcionario funcionario)
        {
            if (funcionario == null)
                return BadRequest("Dados inválidos.");

            funcionario.Id = Funcionarios.Count + 1; // Simulação de ID único
            Funcionarios.Add(funcionario);

            return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, funcionario);
        }

        // PUT: api/funcionario/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Funcionario funcionario)
        {
            var existingFuncionario = Funcionarios.FirstOrDefault(f => f.Id == id);
            if (existingFuncionario == null)
                return NotFound("Funcionário não encontrado.");

            existingFuncionario.Nome = funcionario.Nome;
            existingFuncionario.Contacto = funcionario.Contacto;
            existingFuncionario.NIF = funcionario.NIF;
            existingFuncionario.TipoFuncionarioId = funcionario.TipoFuncionarioId;

            return NoContent();
        }

        // DELETE: api/funcionario/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var funcionario = Funcionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            Funcionarios.Remove(funcionario);
            return NoContent();
        }

  
    }

}
