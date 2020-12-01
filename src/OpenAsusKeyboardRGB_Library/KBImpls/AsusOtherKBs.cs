using OpenAsusKeyboardRGB.KBImpls.GenericImpls;
using OpenAsusKeyboardRGB.KeyMappings;
using System;
using System.Linq;

namespace OpenAsusKeyboardRGB.KBImpls
{
    class AsusRogMA02 : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus MA02"; //Unnamed yet?
        protected override int DevicePID => 6452;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(0, 0); //Unknown yet D:
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusRogFalchionWired : GenericArmouryProtocolKB
    {
        public override string PrettyName => "ROG Falchion Wired"; //M601
        protected override int DevicePID => 6460;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(18, 8);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
        public override Tuple<int, int> GetDirectColorCanvasIndexOfKey(AsusAuraSDKKeys key)
        {
            var rgbKey = AuraSyncProtocolKeyMappings.FalchionMapping.FirstOrDefault(x => x.KeyCode == (ushort)key);
            if (rgbKey == null || rgbKey.X >= DirectColorCanvasLength.Item2 || rgbKey.Y >= DirectColorCanvasLength.Item1)
            {
                throw new ArgumentException();
            }

            return Tuple.Create((int)rgbKey.X, (int)rgbKey.Y);
        }
    }

    class AsusRogFalchionWireless : AsusRogFalchionWired
    {
        public override string PrettyName => "ROG Falchion Wireless"; //M601 Wireless
        protected override int DevicePID => 6462;
    }
}
