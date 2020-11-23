using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using System;

namespace RogArmouryKbRevengGUI.InterfaceGenericKeyboard
{
    class AsusStrixFlareNormal : GenericAsusRogKB
    {
        public override string GetPrettyName()
        {
            return "Asus Strix Flare";
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
        public override Tuple<int, int> GetDirectColorCanvasIndexByVKCode(int virtualkeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
    class AsusStrixFlareCOD : GenericAsusRogKB
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
        public override Tuple<int, int> GetDirectColorCanvasIndexByVKCode(int virtualkeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
    class AsusStrixFlarePNK : GenericAsusRogKB
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
        public override Tuple<int, int> GetDirectColorCanvasIndexByVKCode(int virtualkeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
