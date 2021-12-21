using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeopixelsBackend.Models;
using NeopixelsBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NeopixelsBackend.Controllers
{
    [ApiController]
    [Route("Neopixels")]
    public class NeopixelController : ControllerBase
    {

        private readonly IWS2812Service neopixelService;
        private readonly IPatternService patternService;

        public NeopixelController(IWS2812Service neopixelService, IPatternService patternService)
        {
            this.neopixelService = neopixelService;
            this.patternService = patternService;
        }

        [HttpGet("GetPatternList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatternList>))]
        public async Task<IActionResult> GetAvailablePatternsAsync()
        {
            var availablePatterns = await this.patternService.GetPatternListAsync();
            return Ok(availablePatterns);
        }

        [HttpGet("GetPatternDetails")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatternDetails>))]
        public async Task<IActionResult> GetPatternDetailsAsync([FromQuery]Guid patternUUID)
        {
            var patternDetails = await this.patternService.GetPatternDetailsAsync(patternUUID);
            return Ok(patternDetails);
        }

        [HttpPost("ChangePattern")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangeNeopixelPattern([FromBody]PatternChangeRequest changeRequest)
        {
            var patternDetails = await this.patternService.GetPatternDetailsAsync(changeRequest.PatternUUID);
            this.patternService.SendPatternToNeopixels(patternDetails);
            return Ok();
        }

        [HttpPost("ClearPixels")]
        [ProducesResponseType(200)]
        public IActionResult ClearNeopixels()
        {
            this.patternService.StopSending();
            this.neopixelService.ClearPixels();
            return Ok();
        }
    }
}
