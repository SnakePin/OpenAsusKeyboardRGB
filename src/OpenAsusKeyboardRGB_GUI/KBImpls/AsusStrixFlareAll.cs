using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;

namespace RogArmouryKbRevengGUI.InterfaceGenericKeyboard
{
    class AsusStrixFlareNormal : GenericArmouryProtocolKB
    {
        public override string GetPrettyName()
        {
            return "Asus Strix Flare"; //Referred to as the "Asus Charm" in the Aura SDK
        }
        protected override int PIDOfThisDevice
        {
            get { return 6261; }
        }
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
        protected override int PIDOfThisDevice
        {
            get { return 6319; }
        }
        public override string GetPrettyName()
        {
            return "Asus Strix COD";
        }
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
        protected override int PIDOfThisDevice
        {
            get { return 6351; }
        }
        public override string GetPrettyName()
        {
            return "Asus Strix PNK";
        }
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
