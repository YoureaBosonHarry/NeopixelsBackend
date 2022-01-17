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

        [HttpPost("CreatePattern")]
        [ProducesResponseType(200, Type = typeof(PatternList))]
        public async Task<IActionResult> CreatePatternAsync([FromBody]PatternList patternList)
        {
            var createdPattern = await this.patternService.CreatePatternAsync(patternList);
            return Ok(createdPattern);
        }

        [HttpDelete("DeletePattern")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> DeletePatternAsync([FromQuery]Guid patternUUID)
        {
            var createdPattern = await this.patternService.DeletePatternAsync(patternUUID);
            return Ok(createdPattern);
        }

        [HttpGet("GetPatternDetails")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatternDetails>))]
        public async Task<IActionResult> GetPatternDetailsAsync([FromQuery]Guid patternUUID)
        {
            var patternDetails = await this.patternService.GetPatternDetailsAsync(patternUUID);
            return Ok(patternDetails);
        }

        [HttpPost("AddPatternDetails")]
        [ProducesResponseType(200, Type = typeof(PatternDetails))]
        public async Task<IActionResult> AddPatternDetailsAsync([FromBody] PatternDetails patternDetails)
        {
            var processedDetails = await this.patternService.AddPatternDetailsAsync(patternDetails);
            return Ok(processedDetails);
        }

        [HttpPut("UpdatePatternDetails")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatternDetails>))]
        public async Task<IActionResult> UpdatePatternDetailsAsync(IEnumerable<PatternDetails> updatedPatternDetails)
        {
            var verifiedPatternDetails = await this.patternService.UpdatePatternDetailsAsync(updatedPatternDetails);
            return Ok(verifiedPatternDetails);
        }

        [HttpPost("ChangePattern")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangeNeopixelPattern([FromBody]PatternChangeRequest changeRequest)
        {
            var patternDetails = await this.patternService.GetPatternDetailsAsync(changeRequest.PatternUUID);
            patternDetails = patternDetails.OrderBy(i => i.SequenceNumber);
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
