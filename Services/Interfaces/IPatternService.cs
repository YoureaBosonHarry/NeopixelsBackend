using NeopixelsBackend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeopixelsBackend.Services.Interfaces
{
    public interface IPatternService
    {
        Task<IEnumerable<PatternList>> GetPatternListAsync();
        Task<IEnumerable<PatternDetails>> GetPatternDetailsAsync(Guid patternUUID);
        bool SendPatternToNeopixels(IEnumerable<PatternDetails> patternDetails);
        void StopSending();
    }
}
