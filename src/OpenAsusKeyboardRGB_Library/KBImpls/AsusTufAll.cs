﻿using HidSharp;
using OpenAsusKeyboardRGB.KBImpls.GenericImpls;
using OpenAsusKeyboardRGB.KeyMappings;
using System;
using System.Drawing;

namespace OpenAsusKeyboardRGB.InterfaceGenericKeyboard
{
    class AsusTufK7 : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus TUF K7";
        protected override int DevicePID => 6314;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(23, 6);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusTufK3 : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus TUF K3";
        protected override int DevicePID => 6475;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(23, 6);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusTufK1 : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus TUF K1";
        protected override int DevicePID => 6469;
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(5,1);
        public override bool DoesExistOnSystem()
        {
            return Utils.GetHidDevice(2821, DevicePID, 1, 0xFF00, out _) != null
                && Utils.GetHidDevice(2821, DevicePID, 3, 0xFF00, out _) != null;
        }
        public override void Connect()
        {
            HidDevice iface0Device;
            if ((iface0Device = Utils.GetHidDevice(2821, DevicePID, 1, 0xFF00, out _)) != null)
            {
                _ = Utils.GetHidDevice(2821, DevicePID, 3, 0xFF00, out DeviceReportIDToUse);
                DeviceHIDStream = iface0Device.Open();
                DeviceHIDStream.ReadTimeout = 3000;
                DeviceInputHandler = iface0Device.GetReportDescriptor().CreateHidDeviceInputReceiver();
                DeviceInputHandler.Received += new EventHandler(OnHIDInputReceived);
                DeviceInputHandler.Start(DeviceHIDStream);
                DeviceMaximumInputReportLen = iface0Device.GetMaxInputReportLength();
            }
        }
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
        public override Tuple<int, int> GetDirectColorCanvasIndexOfKey(AsusAuraSDKKeys key)
        {
            throw new NotImplementedException(); //TODO
        }
        public override void SendDirectColorCanvas(Color[,] colorData)
        {
            throw new NotImplementedException(); //TODO
        }
    }

    class AsusTufK5 : GenericArmouryProtocolKB
    {
        public override string PrettyName => "Asus TUF K5";
        protected override int DevicePID => 6297;
        public override bool DoesExistOnSystem()
        {
            return Utils.GetHidDevice(2821, DevicePID, 3, 0xFF00, out _) != null
                && Utils.GetHidDevice(2821, DevicePID, 1, 0xFFC0, out _) != null;
        }
        public override void Connect()
        {
            HidDevice iface0Device;
            if ((iface0Device = Utils.GetHidDevice(2821, DevicePID, 3, 0xFF00, out _)) != null)
            {
                _ = Utils.GetHidDevice(2821, DevicePID, 1, 0xFFC0, out DeviceReportIDToUse);
                DeviceHIDStream = iface0Device.Open();
                DeviceHIDStream.ReadTimeout = 3000;
                DeviceInputHandler = iface0Device.GetReportDescriptor().CreateHidDeviceInputReceiver();
                DeviceInputHandler.Received += new EventHandler(OnHIDInputReceived);
                DeviceInputHandler.Start(DeviceHIDStream);
                DeviceMaximumInputReportLen = iface0Device.GetMaxInputReportLength();
            }
        }
        protected override Tuple<int, int> DirectColorCanvasLength => Tuple.Create(5, 1);
        public override Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
        public override Tuple<int, int> GetDirectColorCanvasIndexOfKey(AsusAuraSDKKeys key)
        {
            throw new NotImplementedException(); //TODO
        }
        public override void SendDirectColorCanvas(Color[,] colorData)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
