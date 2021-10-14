using NeopixelsBackend.Services.Interfaces;
using rpi_ws281x;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NeopixelsBackend.Services
{
    public class NeopixelService: INeopixelService
    {
        private readonly Settings settings = Settings.CreateDefaultSettings(false);
        private readonly Controller controller;
        public NeopixelService()
        {
            this.controller = settings.AddController(256, Pin.Gpio18, StripType.Unknown);
        }

        public void SetPattern(Dictionary<int, string> colorDict)
        {
            using (var rpi = new WS281x(settings))
            {
                foreach (var i in colorDict)
                {
                    var color = HexToColor(i.Value);
                    this.controller.SetLED(i.Key, Color.FromArgb(255, color.R, color.G, color.B));
                }
                rpi.Render();
            };
        }

        private static Color HexToColor(string hexString)
        {
            if (hexString.IndexOf('#') != -1)
                hexString = hexString.Replace("#", "");

            var r = int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            var g = int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            var b = int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            return Color.FromArgb(r, g, b);
        }
    }
}
