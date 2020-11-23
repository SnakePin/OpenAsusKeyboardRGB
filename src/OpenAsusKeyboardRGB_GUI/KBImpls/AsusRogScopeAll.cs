using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using System;

namespace RogArmouryKbRevengGUI.InterfaceGenericKeyboard
{
    //WARNING: This might not work correctly, protocol is slightly different for all Rog Scopes
    //PID is 6400 for PBT (It doesn't even have RGB leds, dunno if I should add it)

    class AsusRogScopeNormal : GenericAsusRogKB
    {
        protected override int PIDOfThisDevice
        {
            get { return 6392; }
        }
        public override string GetPrettyName()
        {
            return "Asus ROG Scope";
        }
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(26, 7); //This value is unknown. It might not even be 26,7 but ditto
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
        public override Tuple<int, int> GetDirectColorCanvasIndexByVKCode(int virtualkeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusRogScopeTKL : GenericAsusRogKB
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
        public override Tuple<int, int> GetDirectColorCanvasIndexByVKCode(int virtualkeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
