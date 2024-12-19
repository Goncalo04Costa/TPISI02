using AsmxService;
using ISI_TP02_APIRestful.Models;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using WCFHospitalService;

namespace ISI_TP02_APIRestful.Controllers
{
    public class ConsultaController : Controller
    {
        // If you need the SOAP client, uncomment and initialize it
        // private readonly HospitalServiceSoapClient _asmxClient;

        private readonly HospitalServiceClient _wcfClient;

        public ConsultaController()
        {
            // Initialize WCF client
            _wcfClient = new HospitalServiceClient();

            // If using the SOAP client, ensure it's correctly initialized:
            // var endpointAddress = new EndpointAddress("https://localhost:44347/Services/HospitalService.asmx");
            // _asmxClient = new HospitalServiceSoapClient(HospitalServiceSoapClient.EndpointConfiguration.HospitalServiceSoap, endpointAddress);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var request = await _wcfClient.deleteConsultaAsync(id);
                if (request)
                    return Ok(request);
                else
                    return NotFound("Consulta not found.");
            }
            catch (Exception ex)
            {
                // Log exception if necessary
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        /// <summary>
        /// Regista uma consulta.
        /// </summary>
        /// <param name="registo">The registo.</param>
        /// <returns></returns>
        [HttpPost("RegistarConsulta")]
        public async Task<IActionResult> RegistarConsulta([FromBody] RegistarConsultaReq registo)
        {
            try
            {

                var consulta = new WCFHospitalService.Consulta
                {
                    UtenteId = registo.UtenteId,
                    FuncionarioId = registo.FuncionarioId,
                    HospitalId = registo.HospitalId,
                    MedicoId = registo.MedicoId,
                    Data = registo.Data,
                    Hora = registo.Hora,
                    Descricao = registo.Descricao
                };

                
                var result = await _wcfClient.CreateConsultaAsync(
                    consulta.UtenteId, consulta.FuncionarioId, consulta.HospitalId,
                    consulta.MedicoId, consulta.Data, consulta.Hora, consulta.Descricao);

                if (result)
                {
                    return Ok("Consulta registered successfully.");
                }
                else
                {
                    return BadRequest("Failed to register consulta.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}