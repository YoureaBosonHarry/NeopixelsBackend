using NeopixelsBackend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeopixelsBackend.Repositories.Interfaces
{
    public interface IPatternRepository
    {
        Task<IEnumerable<PatternList>> GetPatternsAsync();
        Task<PatternList> CreatePatternAsync(PatternList pattern);
        Task<Guid> DeletePatternAsync(Guid patternUUID);
        Task<IEnumerable<PatternDetails>> GetPatternGenerationByGuidAsync(Guid patternUUID);
        Task<PatternDetails> AddPatternDetailsAsync(PatternDetails patternDetails);
        Task<PatternDetails> UpdatePatternDetails(PatternDetails patternDetails);
    }
}
