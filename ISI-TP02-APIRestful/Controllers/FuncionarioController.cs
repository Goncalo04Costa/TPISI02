using AsmxService;
using ISI_TP02_APIRestful.JWT;
using ISI_TP02_APIRestful.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ServiceModel;
using WCFHospitalService;

namespace ISI_TP02_APIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        public HospitalServiceSoapClient asmxClient;

        public HospitalServiceClient WCFcliente = new HospitalServiceClient();

        public FuncionarioController()
        {
            var endpointAddress = new EndpointAddress("https://localhost:44347/Services/HospitalService.asmx");
            asmxClient = new HospitalServiceSoapClient(HospitalServiceSoapClient.EndpointConfiguration.HospitalServiceSoap, endpointAddress);
        }
        [HttpGet("obter")]
        public async Task<IActionResult> Obter()
        {
            var request = await asmxClient.GetAllFuncionariosAsync();
            if (request != null)
                return Ok(request.Body.GetAllFuncionariosResult.ToList());
            return NotFound();
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int  id)
        {
            var request = await asmxClient.DeleteFuncionarioAsync(id);
            if (request)
                return Ok(request);
            return BadRequest();
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(AsmxService.Funcionario funcionario)
        {
            
            var request = await asmxClient.CreateFuncionarioAsync(funcionario);
            if (request.Body.CreateFuncionarioResult) 
                return Ok(request.Body.CreateFuncionarioResult);
            return BadRequest();
        }


        [HttpPut("update")]

        public async Task<IActionResult> Update(AsmxService.Funcionario funcionario)
        {
            var request = await asmxClient.UpdateFuncionarioAsync(funcionario);
            if (request.Body.UpdateFuncionarioResult)
                return Ok(request.Body.UpdateFuncionarioResult);
            return BadRequest();

        }

        /// <summary>
        /// Auxilio para autenticar o utilizador.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        /// <returns></returns>
        [HttpPost("RealizarLogin")]
        public async Task<IActionResult> LoginFuncionario([FromBody] Login loginRequest)
        {
            obterJWT funcoesAuxiliares = new obterJWT();
            try
            {
                WCFHospitalService.Funcionario f = new WCFHospitalService.Funcionario();
                f.NIF = loginRequest.NIF;
                f.Password = loginRequest.Password;
                var userValido = await WCFcliente.AutenticarUtilizadorAsync(f);
                if (userValido == null)
                {
                    return Unauthorized("Email ou password inválido.");
                }
                var token = funcoesAuxiliares.GenerateJwtToken(userValido);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
