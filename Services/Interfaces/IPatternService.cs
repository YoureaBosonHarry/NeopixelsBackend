using NeopixelsBackend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeopixelsBackend.Services.Interfaces
{
    public interface IPatternService
    {
        Task<IEnumerable<PatternList>> GetPatternListAsync();
        Task<PatternList> CreatePatternAsync(PatternList pattern);
        Task<Guid> DeletePatternAsync(Guid patternUUID);
        Task<IEnumerable<PatternDetails>> GetPatternDetailsAsync(Guid patternUUID);
        Task<IEnumerable<PatternDetails>> UpdatePatternDetailsAsync(IEnumerable<PatternDetails> updatedPatternDetails);
        Task<PatternDetails> AddPatternDetailsAsync(PatternDetails patternDetails);
        bool SendPatternToNeopixels(IEnumerable<PatternDetails> patternDetails);
        void StopSending();
    }
}
