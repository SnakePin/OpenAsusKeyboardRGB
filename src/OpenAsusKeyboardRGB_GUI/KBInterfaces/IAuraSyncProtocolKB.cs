using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;
using System.Drawing;

namespace RogArmouryKbRevengGUI.KBInterfaces
{
    interface IAuraSyncProtocolKB : IBasicHIDKB
    {
        void SetDirectColorCanvas(Color[,] arg1);
        Tuple<int, int> GetDirectColorCanvasMaxLength();
        Tuple<int, int> GetDirectColorCanvasIndexByAuraSDKKey(AsusAuraSDKKeys key);
    }
}
