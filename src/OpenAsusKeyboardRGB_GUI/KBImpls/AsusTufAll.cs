﻿
using HidSharp;
using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RogArmouryKbRevengGUI.InterfaceGenericKeyboard
{
    class AsusTufK7 : GenericArmouryProtocolKB
    {
        protected override int PIDOfThisDevice
        {
            get { return 6314; }
        }
        public override string GetPrettyName()
        {
            return "Asus TUF K7";
        }
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(23, 6);
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusTufK5 : GenericArmouryProtocolKB
    {
        protected override int PIDOfThisDevice
        {
            get { return 6297; }
        }
        public override string GetPrettyName()
        {
            return "Asus TUF K5";
        }
        public override bool DoesExistOnSystem()
        {
            return Utils.GetHidDevice(2821, PIDOfThisDevice, 3, 65280, out _) != null
                && Utils.GetHidDevice(2821, PIDOfThisDevice, 1, 65472, out _) != null;
        }
        public override void Connect()
        {
            HidDevice iface0Device;
            if ((iface0Device = Utils.GetHidDevice(2821, PIDOfThisDevice, 3, 65280, out _)) != null)
            {
                _ = Utils.GetHidDevice(2821, PIDOfThisDevice, 1, 65472, out DeviceReportIDToUse);
                DeviceHIDStream = iface0Device.Open();
                DeviceHIDStream.ReadTimeout = 3000;
                DeviceInputHandler = iface0Device.GetReportDescriptor().CreateHidDeviceInputReceiver();
                DeviceInputHandler.Received += new EventHandler(OnHIDInputReceived);
                DeviceInputHandler.Start(DeviceHIDStream);
                DeviceMaximumInputReportLen = iface0Device.GetMaxInputReportLength();
            }
        }
        public override Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(5, 1);
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
        public override Tuple<int, int> GetDirectColorCanvasIndexByAuraSDKKey(AsusAuraSDKKeys key)
        {
            throw new NotImplementedException(); //TODO
        }
        public override void SetDirectColorCanvas(Color[,] colorData)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
