using HidSharp;
using HidSharp.Reports.Input;
using System.Linq;

namespace RogArmouryKbRevengGUI.KBImpls.GenericImpls
{
    abstract class GenericHIDKeyboard
    {
        protected HidStream DeviceHIDStream;
        protected HidDeviceInputReceiver DeviceInputHandler;
        protected int DeviceMaximumInputReportLen;
        protected byte DeviceReportIDToUse;
        abstract protected int DevicePID { get; }
        abstract protected int DeviceVID { get; }

        protected void SendIface0ByteArray(byte[] sendBuf)
        {
            byte[] newSendBuf = new byte[sendBuf.Length + 1];
            sendBuf.CopyTo(newSendBuf, 1);
            newSendBuf[0] = DeviceReportIDToUse;

            DeviceHIDStream.Write(newSendBuf);
        }

        public virtual void Close()
        {
            DeviceHIDStream.Close();
        }

        public virtual bool DoesExistOnSystem()
        {
            return DeviceList.Local.GetHidDevices(DeviceVID, DevicePID).Count() > 0;
        }
    }
}