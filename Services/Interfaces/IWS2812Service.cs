using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeopixelsBackend.Services.Interfaces
{
    public interface IWS2812Service
    {
        void SetPattern(Dictionary<int, string> colorDict);
        void ClearPixels();
    }
}
