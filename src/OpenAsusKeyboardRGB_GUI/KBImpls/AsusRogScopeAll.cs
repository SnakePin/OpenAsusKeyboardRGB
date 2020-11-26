using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;

namespace RogArmouryKbRevengGUI.InterfaceGenericKeyboard
{
    //WARNING: This might not work correctly, protocol is slightly different for all Rog Scopes
    //PID is 6400 for PBT (It doesn't even have RGB leds, dunno if I should add it)

    class AsusRogScopeNormal : GenericArmouryProtocolKB
    {
        protected override int PIDOfThisDevice
        {
            get { return 6392; }
        }
        public override string GetPrettyName()
        {
            return "Asus ROG Scope"; //Also named "ROG CTRL" which is the old name
        }
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(24, 6); //Value confirmed
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusRogScopeTKL : GenericArmouryProtocolKB
    {
        protected override int PIDOfThisDevice
        {
            get { return 6412; }
        }
        public override string GetPrettyName()
        {
            return "Asus ROG Scope TKL";
        }
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(26, 7);
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
