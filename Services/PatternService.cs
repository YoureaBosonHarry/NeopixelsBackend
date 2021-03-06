using NeopixelsBackend.Models;
using NeopixelsBackend.Repositories.Interfaces;
using NeopixelsBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NeopixelsBackend.Services
{
    public class PatternService : IPatternService
    {
        private readonly IPatternRepository patternRepository;
        private readonly IWS2812Service wS2812Service;
        private bool isSending = false;
        private Thread ws2812Thread;

        public PatternService(IPatternRepository patternRepository, IWS2812Service wS2812Service)
        {
            this.patternRepository = patternRepository;
            this.wS2812Service = wS2812Service;
        }

        public async Task<IEnumerable<PatternList>> GetPatternListAsync()
        {
            var availablePatterns = await this.patternRepository.GetPatternsAsync();
            return availablePatterns;
        }

        public async Task<PatternList> CreatePatternAsync(PatternList pattern)
        {
            var createdPattern = await this.patternRepository.CreatePatternAsync(pattern);
            return createdPattern;
        }

        public async Task<Guid> DeletePatternAsync(Guid patternUUID)
        {
            var deletedPattern = await this.patternRepository.DeletePatternAsync(patternUUID);
            return deletedPattern;
        }

        public async Task<IEnumerable<PatternDetails>> GetPatternDetailsAsync(Guid patternUUID)
        {
            var patternDetails = await this.patternRepository.GetPatternGenerationByGuidAsync(patternUUID);
            return patternDetails;
        }

        public async Task<IEnumerable<PatternDetails>> UpdatePatternDetailsAsync(IEnumerable<PatternDetails> updatedPatternDetails)
        {
            foreach (var patternDetails in updatedPatternDetails)
            {
                await this.patternRepository.UpdatePatternDetails(patternDetails);
            }
            return updatedPatternDetails;
        }

        public async Task<PatternDetails> AddPatternDetailsAsync(PatternDetails patternDetails)
        {
            var repoDetails = await this.patternRepository.AddPatternDetailsAsync(patternDetails);
            return repoDetails;
        }

        

        public bool SendPatternToNeopixels(IEnumerable<PatternDetails> patternDetails)
        {
            if (isSending)
            {
                StopSending();
            }
            isSending = true;
            ws2812Thread = new Thread(() => SendData(patternDetails));
            ws2812Thread.IsBackground = true;
            ws2812Thread.Start();
            return true;
        }

        private void SendData(IEnumerable<PatternDetails> patternDetails)
        {
            while (isSending)
            {
                foreach (var pattern in patternDetails)
                {
                    this.wS2812Service.SetPattern(pattern.SequenceDictionary);
                    Thread.Sleep(700);
                    Console.WriteLine(pattern.SequenceNumber.ToString());
                }
            }
        }

        public void StopSending()
        {
            isSending = false;
            if (ws2812Thread.Join(200) == false)
            {
                ws2812Thread.Join();
            }
            ws2812Thread = null;
        }
    }
}
