﻿using AsmxService;
using ISI_TP02_APIRestful.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ServiceModel;
using WCFHospitalService;

namespace ISI_TP02_APIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : Controller
    {
        public static HospitalServiceSoapClient asmxClient;

        public ConsultaController()
        {
            var endpointAddress = new EndpointAddress("https://localhost:44347/Services/HospitalService.asmx");
            asmxClient = new HospitalServiceSoapClient(HospitalServiceSoapClient.EndpointConfiguration.HospitalServiceSoap, endpointAddress);
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
            if (consulta.UtenteId <= 0 || consulta.FuncionarioId <= 0 || consulta.HospitalId <= 0 || consulta.MedicoId <= 0 || consulta.Data == default || consulta.Hora == default)
                return BadRequest("Dados da consulta inválidos.");

            try
            {
                // Exemplo de chamada ao serviço ASMX para atualizar consulta
                var request = await asmxClient.UpdateConsultaAsync(
                    consulta.UtenteId,
                    consulta.FuncionarioId,
                    consulta.HospitalId,
                    consulta.MedicoId,
                    consulta.Data,
                    consulta.Hora,
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
            if (consulta.UtenteId <= 0 || consulta.FuncionarioId <= 0 || consulta.HospitalId <= 0 || consulta.MedicoId <= 0 || consulta.Data == default || consulta.Hora == default)
                return BadRequest("Dados da consulta inválidos.");

            try
            {
                // Passando os campos individuais do objeto 'consulta' para o método
                var request = await asmxClient.CreateConsultaAsync(
                    consulta.UtenteId,
                    consulta.FuncionarioId,
                    consulta.HospitalId,
                    consulta.MedicoId,
                    consulta.Data,
                    consulta.Hora,
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