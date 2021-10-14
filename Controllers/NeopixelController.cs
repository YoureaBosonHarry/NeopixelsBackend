using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeopixelsBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeopixelsBackend.Controllers
{
    [ApiController]
    [Route("Neopixels")]
    public class NeopixelController : ControllerBase
    {

        private readonly INeopixelService neopixelService;

        public NeopixelController(INeopixelService neopixelService)
        {
            this.neopixelService = neopixelService;
        }

        [HttpPost("ChangePattern")]
        public async Task ChangeNeopixelPattern([FromBody]Dictionary<int, string> colorDict)
        {
            this.neopixelService.SetPattern(colorDict);
            
        }
    }
}
