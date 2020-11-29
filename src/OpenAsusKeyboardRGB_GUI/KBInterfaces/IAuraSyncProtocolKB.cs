using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;
using System.Drawing;

namespace RogArmouryKbRevengGUI.KBInterfaces
{
    public interface IAuraSyncProtocolKB : IBasicHIDKB
    {
        void SendDirectColorCanvas(Color[,] arg1);
        Color[,] GetNewDirectColorCanvas();
        Tuple<int, int> GetDirectColorCanvasIndexOfKey(AsusAuraSDKKeys key);
    }
}
