using AsmxService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;

namespace ISI_TP02_APIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        public HospitalServiceSoapClient asmxClient;
        public HospitalController() 
        {
            var endpointAddress = new EndpointAddress("https://localhost:44347/Services/HospitalService.asmx");
            asmxClient = new HospitalServiceSoapClient(HospitalServiceSoapClient.EndpointConfiguration.HospitalServiceSoap, endpointAddress);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> delete(int id)
        {
            var request = await asmxClient.DeleteFuncionarioAsync(id);
            if (request)
                return Ok(request);
            return BadRequest();
        }
    }
}
