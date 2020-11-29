using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;

namespace RogArmouryKbRevengGUI.InterfaceGenericKeyboard
{
    //WARNING: This might not work correctly, protocol is slightly different for all Rog Scopes
    //PID is 6400 for PBT (It doesn't even have RGB leds, dunno if I should add it)

    class AsusRogScopeNormal : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus ROG Scope"; //Also named "ROG CTRL" which is the old name
        protected override int DevicePID => 6392;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(24, 6);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusRogScopeTKL : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus ROG Scope TKL";
        protected override int DevicePID => 6412;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(26, 7);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
