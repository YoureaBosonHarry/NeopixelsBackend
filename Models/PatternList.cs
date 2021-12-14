using System;

namespace NeopixelsBackend.Models
{
    public class PatternList
    {
        public Guid PatternUUID { get; set; }
        public string PatternName { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
