using OpenAsusKeyboardRGB.KBImpls.GenericImpls;
using OpenAsusKeyboardRGB.KeyMappings;
using System;

namespace OpenAsusKeyboardRGB.InterfaceGenericKeyboard
{
    //WARNING: This might not work correctly, protocol is slightly different for all Rog Scopes
    //PID is 6400 for PBT (It doesn't even have RGB leds, dunno if I should add it)

    class AsusRogScopeNormal : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus ROG Scope"; //Also named "ROG CTRL" which is the old name
        protected override int DevicePID => 6392;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(24, 6);
        public override Tuple<int, int> GetDirectColorCanvasIndexOfKey(AsusAuraSDKKeys key)
        {
            switch (key)
            {
                case AsusAuraSDKKeys.ROG_KEY_LMENU:
                    return Tuple.Create(5, 3);
                case AsusAuraSDKKeys.ROG_KEY_LWIN:
                    return Tuple.Create(5, 2);
            }
            return base.GetDirectColorCanvasIndexOfKey(key);
        }
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

    class AsusRogScopeTKLPUNK : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus ROG Scope TKL Electro Punk";
        protected override int DevicePID => 6484;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(26, 7);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusRogScopeTKLGundam : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus ROG Scope TKL Gundam Ltd";
        protected override int DevicePID => 6561;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(26, 7);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusRogScopeRx_XA05 : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus ROG Scope Rx";
        protected override int DevicePID => 6481;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(23, 6);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
