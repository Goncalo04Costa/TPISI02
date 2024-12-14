using Microsoft.AspNetCore.Mvc;
using ISITP02.Models;
using System.Collections.Generic;
using System.Linq;

namespace ISITP02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private static List<Hospital> _hospitais = new List<Hospital>
        {
            new Hospital { Id = 1, Nome = "Hospital Central", Localizacao = "Lisboa", Utentes = new List<Utente>(), Consultas = new List<Consulta>() },
            new Hospital { Id = 2, Nome = "Hospital Norte", Localizacao = "Porto", Utentes = new List<Utente>(), Consultas = new List<Consulta>() }
        };

        // GET: api/Hospital
        [HttpGet]
        public ActionResult<IEnumerable<Hospital>> GetHospitais()
        {
            return Ok(_hospitais);
        }

        // GET: api/Hospital/{id}
        [HttpGet("{id}")]
        public ActionResult<Hospital> GetHospital(int id)
        {
            var hospital = _hospitais.FirstOrDefault(h => h.Id == id);
            if (hospital == null)
                return NotFound();

            return Ok(hospital);
        }

        // POST: api/Hospital
        [HttpPost]
        public ActionResult<Hospital> CreateHospital(Hospital hospital)
        {
            if (hospital == null || string.IsNullOrEmpty(hospital.Nome) || string.IsNullOrEmpty(hospital.Localizacao))
                return BadRequest("Dados do hospital são inválidos.");

            hospital.Id = _hospitais.Max(h => h.Id) + 1;
            _hospitais.Add(hospital);

            return CreatedAtAction(nameof(GetHospital), new { id = hospital.Id }, hospital);
        }

        // PUT: api/Hospital/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateHospital(int id, Hospital updatedHospital)
        {
            var hospital = _hospitais.FirstOrDefault(h => h.Id == id);
            if (hospital == null)
                return NotFound();

            if (updatedHospital == null || string.IsNullOrEmpty(updatedHospital.Nome) || string.IsNullOrEmpty(updatedHospital.Localizacao))
                return BadRequest("Dados do hospital são inválidos.");

            hospital.Nome = updatedHospital.Nome;
            hospital.Localizacao = updatedHospital.Localizacao;
            hospital.Utentes = updatedHospital.Utentes;
            hospital.Consultas = updatedHospital.Consultas;

            return NoContent();
        }

        // DELETE: api/Hospital/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteHospital(int id)
        {
            var hospital = _hospitais.FirstOrDefault(h => h.Id == id);
            if (hospital == null)
                return NotFound();

            _hospitais.Remove(hospital);

            return NoContent();
        }
    }
}