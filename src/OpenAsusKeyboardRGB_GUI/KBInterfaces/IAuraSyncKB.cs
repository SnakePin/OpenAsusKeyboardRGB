using System;
using System.Drawing;

namespace RogArmouryKbRevengGUI.KBInterfaces
{
    interface IAuraSyncKB : IGenericKB
    {
        void SetDirectColorCanvas(Color[] arg1);
        Tuple<int, int> GetDirectColorCanvasMaxLength();
        Tuple<int, int> GetDirectColorCanvasIndexByVKCode(int virtualkeyCode);
    }
}
