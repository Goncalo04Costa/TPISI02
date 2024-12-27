using AsmxService;
using ISI_TP02_APIRestful.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ServiceModel;
using WCFHospitalService;
using Microsoft.Extensions.Logging;

namespace ISI_TP02_APIRestful.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        public HospitalServiceSoapClient asmxClient;
        private readonly ILogger<ConsultaController> _logger;

        public ConsultaController(ILogger<ConsultaController> logger)
        {
            var endpointAddress = new EndpointAddress("https://localhost:44347/Services/HospitalService.asmx");
            asmxClient = new HospitalServiceSoapClient(HospitalServiceSoapClient.EndpointConfiguration.HospitalServiceSoap, endpointAddress);
            _logger = logger;
        }



        [HttpGet("obter")]
        public async Task<IActionResult> Obter()
        {
            try
            {
                var request = await asmxClient.GetAllConsultasAsync();
                _logger.LogInformation("Resposta da API ASMX: {Response}", request?.Body);

                if (request != null && request.Body != null && request.Body.GetAllConsultasResult != null)
                {
                    _logger.LogInformation("Consultas obtidas com sucesso.");
                    return Ok(request.Body.GetAllConsultasResult.ToList());
                }

                _logger.LogWarning("Nenhuma consulta encontrada ou resposta vazia.");
                return NotFound();
            }
            catch (Exception ex)
            {
                // Logando a exceção detalhadamente para entender melhor o erro
                _logger.LogError(ex, "Erro ao obter as consultas. Mensagem de erro: {ErrorMessage}", ex.Message);
                return StatusCode(500, "Erro interno ao processar a solicitação.");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await asmxClient.DeleteConsultaAsync(id);
            if (request)
                return Ok(request);
            return BadRequest();
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update(AsmxService.Consulta consulta)
        {
            if (consulta == null)
                return BadRequest("Os dados da consulta são obrigatórios.");

            // Validação básica dos campos necessários
            if (consulta.UtenteId <= 0 || consulta.FuncionárioId <= 0 || consulta.HospitalId <= 0 || consulta.MedicoId <= 0 || consulta.Data == default )
                return BadRequest("Dados da consulta inválidos.");

            try
            {
                // Exemplo de chamada ao serviço ASMX para atualizar consulta
                var request = await asmxClient.UpdateConsultaAsync(
                    consulta.UtenteId,
                    consulta.FuncionárioId,
                    consulta.HospitalId,
                    consulta.MedicoId,
                    consulta.Data,
                    consulta.Descricao
                );

                if (request.Body.UpdateConsultaResult)
                    return Ok("Consulta atualizada com sucesso.");

                return BadRequest("Falha ao atualizar a consulta.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AsmxService.Consulta consulta)
        {
            if (consulta == null)
                return BadRequest("Os dados da consulta são obrigatórios.");

            // Validação básica dos campos necessários
            if (consulta.UtenteId <= 0 || consulta.FuncionárioId <= 0 || consulta.HospitalId <= 0 || consulta.MedicoId <= 0 || consulta.Data == default)
                return BadRequest("Dados da consulta inválidos.");

            try
            {
                // Passando os campos individuais do objeto 'consulta' para o método
                var request = await asmxClient.CreateConsultaAsync(
                    consulta.UtenteId,
                    consulta.FuncionárioId,
                    consulta.HospitalId,
                    consulta.MedicoId,
                    consulta.Data,
                    consulta.Descricao
                );

                if (request.Body.CreateConsultaResult) // Certifique-se de usar o campo correto no retorno
                    return Ok("Consulta criada com sucesso.");

                return BadRequest("Erro ao criar a consulta.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }




    }
}