using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using System;

namespace RogArmouryKbRevengGUI.InterfaceGenericKeyboard
{
    class AsusStrixFlareNormal : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus Strix Flare"; //Referred to as the "Asus Charm" in the Aura SDK
        protected override int DevicePID => 6261;
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(24, 6);
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
    class AsusStrixFlareCOD : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus Strix COD";
        protected override int DevicePID => 6319;
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(24, 6);
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
    class AsusStrixFlarePNK : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus Strix PNK";
        protected override int DevicePID => 6351;
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(24, 6);
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
