using NeopixelsBackend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeopixelsBackend.Repositories.Interfaces
{
    public interface IPatternRepository
    {
        Task<IEnumerable<PatternList>> GetPatternsAsync();
        Task<IEnumerable<PatternDetails>> GetPatternGenerationByGuidAsync(Guid patternUUID);
    }
}
