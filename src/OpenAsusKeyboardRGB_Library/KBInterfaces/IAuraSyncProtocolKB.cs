using OpenAsusKeyboardRGB.KeyMappings;
using System;
using System.Drawing;

namespace OpenAsusKeyboardRGB.KBInterfaces
{
    public interface IAuraSyncProtocolKB : IBasicHIDKB
    {
        void SendDirectColorCanvas(Color[,] arg1);
        Color[,] GetNewDirectColorCanvas();
        Tuple<int, int> GetDirectColorCanvasIndexOfKey(AsusAuraSDKKeys key);
    }
}
