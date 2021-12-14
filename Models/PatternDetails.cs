using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeopixelsBackend.Models
{
    public class PatternDetails
    {
        public Guid PatternUUID { get; set; }
        public string PatternName { get; set; }
        public int SequenceNumber { get; set; }
        public string SequenceDescription { get; set; }
        public string SequenceMetadata { get; set; }
        public Dictionary<int, string> SequenceDictionary => (JsonSerializer.Deserialize<Dictionary<int, string>>(SequenceDescription));
    }
}
