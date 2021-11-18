using JARWARiNFO.Models;
using JARWARiNFO.Services;
using Microsoft.AspNetCore.Mvc;

namespace JARWARiNFO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HWMonitorController : ControllerBase
    {
        private readonly HWMonitor _hw;
        public HWMonitorController(HWMonitor hw)
        {
            _hw = hw;
        }

        /// <summary>
        ///     Devuelve un objeto serializado en formato JSON que contiene toda la información del hardware del equipo.
        /// </summary>
        /// <response code="200">La lista se genero exitosamente y fue entregada.</response>
        /// <response code="404">La lista no pudo ser generada debido a algún error interno, este es detallado en la sección "msg" de la respuesta.</response>
        [HttpGet]
        public ActionResult<ReqMessage> GetHardwareInfo()
        {
            try
            {
                return Ok(new ReqMessage
                {
                    Msg = "success",
                    State = true,
                    HardwareInfo = _hw.GetHardwareData() ?? throw new Exception("El método que devuelve la información de su hardware ha retornado null.")
                });
            }
            catch (Exception e)
            {
                return NotFound(new ReqMessage { Msg = e.Message, State = false });
            }
        }

        /// <summary>
        ///     Devuelve un objeto serializado en formato JSON que contiene toda la información del hardware del equipo.
        /// </summary>
        /// <response code="200">La lista se genero exitosamente y fue entregada.</response>
        /// <response code="404">La lista no pudo ser generada debido a algún error interno, este es detallado en la sección "msg" de la respuesta.</response>
        [HttpGet("{HardwareType}")]
        public ActionResult<ReqMessage> GetInfoByType(string HardwareType)
        {
            try
            {
                return Ok(new ReqMessage
                {
                    Msg = "success",
                    State = true,
                    HardwareInfo = _hw.GetDataByHardwareType(HardwareType) ?? throw new Exception("El método que devuelve la información de su hardware ha retornado null.")
                });
            }
            catch (Exception e)
            {
                return NotFound(new ReqMessage { Msg = e.Message, State = false });
            }
        }
    }
}
