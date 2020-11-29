using OpenAsusKeyboardRGB.KBImpls.GenericImpls;
using System;

namespace OpenAsusKeyboardRGB.InterfaceGenericKeyboard
{
    class AsusStrixFlareNormal : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus Strix Flare"; //Referred to as the "Asus Charm" in the Aura SDK
        protected override int DevicePID => 6261;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(24, 6);

        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
    class AsusStrixFlareCOD : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus Strix COD";
        protected override int DevicePID => 6319;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(24, 6);

        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
    class AsusStrixFlarePNK : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus Strix PNK";
        protected override int DevicePID => 6351;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(24, 6);

        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
