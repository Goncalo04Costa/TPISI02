﻿using AsmxService;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using WCFHospitalService;

namespace ISI_TP02_APIRestful.Controllers
{
    public class UtenteController : Controller
    {
        public HospitalServiceSoapClient asmxClient;

     

        public UtenteController()
        {
            var endpointAddress = new EndpointAddress("https://localhost:44347/Services/HospitalService.asmx");
            asmxClient = new HospitalServiceSoapClient(HospitalServiceSoapClient.EndpointConfiguration.HospitalServiceSoap, endpointAddress);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await asmxClient.DeleteUtenteAsync(id);
            if (request)
                return Ok(request);
            return BadRequest();
        }



        [HttpPut("update")]
        public async Task<IActionResult> Update(AsmxService.Utente utente)
        {
            if (utente == null)
                return BadRequest("Os dados do utente são obrigatórios.");

            // Validação básica dos campos necessários
            if (string.IsNullOrWhiteSpace(utente.Nome) || utente.NIF <= 0 || utente.HospitalId <= 0)
                return BadRequest("Dados do utente inválidos.");

            try
            {
               
                var request = await asmxClient.UpdateUtenteAsync(
                    utente.Id,        
                    utente.Nome,
                    utente.NIF,
                    utente.DataEntrada,
                    utente.TipoUtenteId,
                    utente.HospitalId
                );

                if (request.Body.UpdateUtenteResult)
                    return Ok("Utente atualizado com sucesso.");

                return BadRequest("Falha ao atualizar o utente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AsmxService.Utente utente)
        {
            if (utente == null)
                return BadRequest("Os dados do utente são obrigatórios.");

            try
            {
                // Passando os campos individuais do objeto 'utente' para o método
                var request = await asmxClient.CreateUtenteAsync(
                    utente.Nome,
                    utente.NIF,
                    utente.DataEntrada,
                    utente.TipoUtenteId,
                    utente.HospitalId
                );

                if (request.Body.CreateUtenteResult) // Certifique-se de usar o campo correto no retorno
                    return Ok("Utente criado com sucesso.");

                return BadRequest("Erro ao criar o utente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }



    }
}