using AsmxService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;

namespace ISI_TP02_APIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        public HospitalServiceSoapClient asmxClient;

        public FuncionarioController()
        {
            var endpointAddress = new EndpointAddress("https://localhost:44347/Services/HospitalService.asmx");
            asmxClient = new HospitalServiceSoapClient(HospitalServiceSoapClient.EndpointConfiguration.HospitalServiceSoap, endpointAddress);
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
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            
            var request = await asmxClient.CreateFuncionarioAsync(funcionario);
            if (request.Body.CreateFuncionarioResult) 
                return Ok(request.Body.CreateFuncionarioResult);
            return BadRequest();
        }


        [HttpPut("update")]

        public async Task<IActionResult> Update(Funcionario funcionario)
        {
            var request = await asmxClient.UpdateFuncionarioAsync(funcionario);
            if (request.Body.UpdateFuncionarioResult)
                return Ok(request.Body.UpdateFuncionarioResult);
            return BadRequest();

        }
    }
}
