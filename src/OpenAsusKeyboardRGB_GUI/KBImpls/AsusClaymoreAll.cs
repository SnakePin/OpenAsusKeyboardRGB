using HidSharp;
using RogArmouryKbRevengGUI;
using RogArmouryKbRevengGUI.KBImpls.GenericImpls;
using RogArmouryKbRevengGUI.KBInterfaces;
using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace RogArmouryKbRevengGUI_NETFW.KBImpls
{
    class AsusClaymore : GenericHIDKeyboard, IArmouryProtocolKB, IAuraSyncProtocolKB
    {
        protected override int DevicePID => 6221;
        protected override int DeviceVID => 2821;
        public string PrettyName => "Asus Claymore (Core?)";

        public void Connect()
        {
            HidDevice iface0Device;
            if ((iface0Device = Utils.GetHidDevice(2821, DevicePID, 1, 0xFF00, out DeviceReportIDToUse)) != null)
            {
                DeviceHIDStream = iface0Device.Open();
                DeviceHIDStream.ReadTimeout = 3000;
                DeviceInputHandler = iface0Device.GetReportDescriptor().CreateHidDeviceInputReceiver();
                DeviceInputHandler.Received += new EventHandler(OnHIDInputReceived);
                DeviceInputHandler.Start(DeviceHIDStream);
                DeviceMaximumInputReportLen = iface0Device.GetMaxInputReportLength();
            }
        }

        public void SetProfileIndex(byte newIndex)
        {
            EnableHIDControl(true);

            byte[] array = new byte[64];
            array[0] = 81;
            array[1] = 0;
            array[4] = newIndex;
            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);

            EnableHIDControl(false);
        }

        public byte GetProfileIndex()
        {
            EnableHIDControl(true);

            byte[] array = new byte[64];
            array[0] = 82;
            array[1] = 0;
            SendIface0ByteArray(array);

            GetIface0Response(InterfaceZeroResponseTypes.GetWritableData, out byte[] buf);
            EnableHIDControl(false);

            return buf[4];
        }

        public void AuraSyncModeSwitch(bool state)
        {
            if (state)
            {
                SwitchToAuraSyncMode();
            }
            else
            {
                //1 is the default profile, 2 is the first user-programmable profile(?)
                SetProfileIndex(1);
            }
        }

        public void ExecuteProfileFlashCmd()
        {
            EnableHIDControl(true);

            byte[] array = new byte[64];
            array[0] = 80;
            array[1] = 85;
            array[4] = 0;
            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.ExecuteProfileFlashCmd);

            EnableHIDControl(false);
        }

        public void SetEffect_Static(Color mainColor, Color backgroundColor, byte brightness)
        {
            //Brightness is hardcoded
            brightness = 0xFF;
            InternalSendWriteType44((byte)ByteSelectedEffectTypes.Static, brightness, mainColor, backgroundColor);
        }

        public void SetEffect_ColorCycle(ColorCycleSpeeds colorCycleSpeed, byte brightness)
        {
            //Both parameters are hardcoded
            InternalSendWriteType44((byte)ByteSelectedEffectTypes.ColorCycle, 128, Color.Empty, Color.Empty, 121);
        }

        public void SetEffect_Breathing(Color breathingColor1, Color breathingColor2, byte brightness, BreathingTypes breathingType, BreathingSpeeds speed)
        {
            if (breathingType != BreathingTypes.Single)
            {
                //Only single breathing mode is implemented in this keyboard
                throw new ArgumentException();
            }

            //Both parameters are hardcoded here too
            InternalSendWriteType44((byte)ByteSelectedEffectTypes.Breathing, byte.MaxValue, breathingColor1, Color.Empty, 107);
        }

        private void InternalSendWriteType44(byte byteSelectedEffect, byte brightness, Color color1, Color color2, byte speed = 0, byte randColorBit = 0, byte bgSinkBit = 0,
            byte brightnessFadeBit = 0, byte byteDirection5Bits = 0, byte byteExt1 = 0xFF, byte byteExt2 = 0xFF)
        {
            EnableHIDControl(true);
            byte[] array = new byte[64];
            array[0] = 81;
            array[1] = 44; //write type 44

            //Array.Copy(kbInfo.effectModeData[0].byteIndex, 0, array, 2, 2);
            for (int l = 0; l < 5; l++)
            {
                array[4 + l * 12] = byte.MaxValue;
                array[7 + l * 12] = byte.MaxValue;
                array[8 + l * 12] = byte.MaxValue;
                array[9 + l * 12] = byte.MaxValue;
            }

            array[4] = byteSelectedEffect;
            array[5] = speed;

            array[6] = 0;
            array[6] |= (byte)(randColorBit << 7);
            array[6] |= (byte)((bgSinkBit << 6) & 0b01000000);
            array[6] |= (byte)((brightnessFadeBit << 5) & 0b00100000);
            array[6] |= (byte)((byteDirection5Bits) & 0b00011111);

            array[7] = byteExt1;
            array[8] = byteExt2;
            array[9] = brightness;

            array[10] = color1.R;
            array[11] = color1.G;
            array[12] = color1.B;
            array[13] = color2.R;
            array[14] = color2.G;
            array[15] = color2.B;

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);

            SetByteSelectedEffectWritableData(byteSelectedEffect);

            /*GetConst(99);
            if (iProfileIndex == 0)
            {
                return;
            }*/

            ExecuteProfileFlashCmd();
            EnableHIDControl(false);
        }

        #region Enums
        private enum InterfaceZeroResponseTypes
        {
            GetInfo,
            ApplyLight20P,
            GetTarget,
            SetTarget,
            HIDControlStateSwitch,
            GetConstantValue,
            SetWritableData,
            GetWritableData,
            SetLedResponse,
            AddressCommand,
            SetFlashWritePoint,
            GetFlashWritePoint,
            WriteToFlash,
            ExecuteMacroFlash,
            ExecuteProfileFlashCmd,
            GetKeyLogData,
            AuraSyncProtocolUpdateCommand,
            InvalidResponse
        };

        private enum ByteSelectedEffectTypes
        {
            Static,
            Breathing,
            ColorCycle,
            Reactive,
            Wave,
            Ripple,
            StarryNight,
            Quicksand,
            OFF = 254
        };
        #endregion

        #region Direct color canvas update functions
        public Tuple<int, int> GetDirectColorCanvasMaxLength()
        {
            return Tuple.Create(23, 8);
        }

        public Tuple<int, int> GetDirectColorCanvasIndexByAuraSDKKey(AsusAuraSDKKeys key)
        {
            switch (key)
            {
                case AsusAuraSDKKeys.UNOFFICIAL_ISO_BACKSLASH:
                    return Tuple.Create(4, 1);
                case AsusAuraSDKKeys.ROG_KEY_Z:
                    return Tuple.Create(4, 2);
                case AsusAuraSDKKeys.ROG_KEY_X:
                    return Tuple.Create(4, 3);
                case AsusAuraSDKKeys.ROG_KEY_C:
                    return Tuple.Create(4, 4);
                case AsusAuraSDKKeys.ROG_KEY_V:
                    return Tuple.Create(4, 5);
                case AsusAuraSDKKeys.ROG_KEY_B:
                    return Tuple.Create(4, 6);
                case AsusAuraSDKKeys.ROG_KEY_N:
                    return Tuple.Create(4, 7);
                case AsusAuraSDKKeys.ROG_KEY_M:
                    return Tuple.Create(4, 8);
                case AsusAuraSDKKeys.ROG_KEY_COMMA:
                    return Tuple.Create(4, 9);
                case AsusAuraSDKKeys.ROG_KEY_PERIOD:
                    return Tuple.Create(4, 10);
                case AsusAuraSDKKeys.ROG_KEY_SLASH:
                    return Tuple.Create(4, 11);
                case AsusAuraSDKKeys.UNOFFICIAL_ISO_HASH:
                    return Tuple.Create(3, 12);
                case AsusAuraSDKKeys.ROG_KEY_LOGO:
                    return Tuple.Create(5, 8);
                    /*case AsusAuraSDKKeys.ROG_KEY_NUMPAD5:
                        return Tuple.Create(4, 19);
                    case AsusAuraSDKKeys.ROG_KEY_NUMPAD6:
                        return Tuple.Create(4, 21);*/
            }

            var rgbKey = AuraSyncProtocolKeyMappings.ClaymoreMapping.FirstOrDefault(x => x.KeyCode == (ushort)key);
            if (rgbKey == null)
            {
                throw new ArgumentException();
            }

            var maxLen = GetDirectColorCanvasMaxLength();
            if (rgbKey.X >= maxLen.Item2 || rgbKey.Y >= maxLen.Item1)
            {
                throw new ArgumentException();
            }

            return Tuple.Create((int)rgbKey.X, (int)rgbKey.Y);
        }

        public void SetDirectColorCanvas(Color[,] arg1)
        {
            AuraSyncModeSwitch(true);

            //Claymore's key matrix is [Rows, Columns] whereas a sane key matrix is [Columns, Rows]
            var colorArray = arg1.TransposeMatrix().FlattenMatrix();

            int XMax = GetDirectColorCanvasMaxLength().Item1;
            int YMax = GetDirectColorCanvasMaxLength().Item2;

            int iVar12 = XMax * YMax;

            byte[] buffer = new byte[64];
            byte uVar14 = 0;
            byte bVar6 = 0;
            byte uVar9 = 0;

            buffer[0] = 0xc0;
            buffer[1] = 0x81;
            buffer[2] = 0x0f;

            do
            {
                int iVar10 = 0;
                uint uVar11 = uVar14;
                byte bVar8 = bVar6;
                do
                {
                    uint uVar3 = uVar11 & 0x80000007;
                    if ((int)uVar3 < 0)
                    {
                        uVar3 = (uVar3 - 1 | 0xfffffff8) + 1;
                    }
                    bVar6 = (byte)iVar10;
                    if (uVar3 != 0)
                    {
                        bVar6 = bVar8;
                    }
                    iVar10++;
                    uVar11--;
                    bVar8 = bVar6;
                } while (iVar10 < 8);

                iVar10 = uVar14 - bVar6;
                buffer[uVar9 + 4] = (byte)(((byte)iVar10 + ((byte)(iVar10 >> 0x1f) & 7) & 0xf8) + bVar6);

                buffer[uVar9 + 5] = colorArray[uVar14].R;
                buffer[uVar9 + 6] = colorArray[uVar14].G;
                buffer[uVar9 + 7] = colorArray[uVar14].B;

                uVar14++;
                bVar8 = (byte)(uVar9 + 4);
                if (bVar8 == 60)
                {
                    SendIface0ByteArray(buffer);
                    bVar8 = 0;

                    Array.Clear(buffer, 4, 20);
                    /*puVar5[4]

                    pbVar4 = puVar5 + 4;
                    lVar5 = 4;
                    do
                    {
                        *(undefined4*)(pbVar4) = 0;
                        pbVar4 += 4;

                        lVar5--;
                    } while (lVar5 != 0);*/
                }

                uVar9 = bVar8;
                if (uVar14 == iVar12 - 1)
                {
                    buffer[2] = (byte)(bVar8 >> 2);
                    SendIface0ByteArray(buffer);
                }
            } while (uVar14 < iVar12);
        }
        #endregion

        #region HID response handling
        private event Action<InterfaceZeroResponseTypes, ReadOnlyCollection<byte>> OnValidIface0ResponseEvent;

        private void OnHIDInputReceived(object sender, EventArgs e)
        {
            var inputReportBuffer = new byte[DeviceMaximumInputReportLen];

            //Flush pending report buffer
            while (DeviceInputHandler.TryRead(inputReportBuffer, 0, out _))
            {
                //Dispatch by removing the report ID
                DispatchInterfaceZeroResponse(inputReportBuffer.Skip(1).ToArray());
            }
        }

        private void DispatchInterfaceZeroResponse(byte[] receivedBuffer)
        {
            var respType = Iface0GetResponseType(receivedBuffer);
            Console.WriteLine("[Claymore] Got a response! Response type: " + respType.ToString());
            OnValidIface0ResponseEvent?.Invoke(respType, Array.AsReadOnly(receivedBuffer));
        }
        private InterfaceZeroResponseTypes Iface0GetResponseType(byte[] receiveBuffer)
        {
            if (receiveBuffer.Length < 2)
            {
                return InterfaceZeroResponseTypes.InvalidResponse;
            }

            if (receiveBuffer[0] == 18 /*&& (new byte[] { 1, 0, 32, getInfoID }.Contains(receiveBuffer[1]))*/)
            {
                return InterfaceZeroResponseTypes.GetInfo;
            }
            else if (receiveBuffer[0] == 64 && receiveBuffer[1] == 146)
            {
                return InterfaceZeroResponseTypes.ApplyLight20P;
            }
            else if (receiveBuffer[0] == 64 && (new byte[] { 0, 32, 96, 97, 98, 99 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.GetConstantValue;
            }
            else if (receiveBuffer[0] == 16 && (new byte[] { 0, 2 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.GetTarget;
            }
            else if (receiveBuffer[0] == 16 && receiveBuffer[1] == 1)
            {
                return InterfaceZeroResponseTypes.SetTarget;
            }
            else if (receiveBuffer[0] == 65 && (new byte[] { 0, 1, 2, 3, 128 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.HIDControlStateSwitch;
            }
            else if (receiveBuffer[0] == 81 && (new byte[] { 0, 16, 24, 25, 40, 44, 160, 48, 168, 32, 41, 145, 144, 2 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.SetWritableData;
            }
            else if (receiveBuffer[0] == 82 && (new byte[] { 0, 16, 24, 25, 40, 44, 160, 48, 168, 32, 41, 145, 144, 2 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.GetWritableData;
            }
            else if (receiveBuffer[0] == 192 && (new byte[] { 0, 1, 2, 240 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.SetLedResponse;
            }
            else if (receiveBuffer[0] == 30 && receiveBuffer[1] == 1)
            {
                return InterfaceZeroResponseTypes.SetFlashWritePoint;
            }
            else if (receiveBuffer[0] == 30 && receiveBuffer[1] == 0)
            {
                return InterfaceZeroResponseTypes.GetFlashWritePoint;
            }
            else if (receiveBuffer[0] == 31 && receiveBuffer[1] == 52)
            {
                return InterfaceZeroResponseTypes.WriteToFlash;
            }
            else if (receiveBuffer[0] == 83 && (new byte[] { 0, 1, 16, 17, 255 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.ExecuteMacroFlash;
            }
            else if (receiveBuffer[0] == 80 && (new byte[] { 0, 16, 32, 85, 0, 1 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.ExecuteProfileFlashCmd;
            }
            else if (receiveBuffer[0] == 80 && (new byte[] { 0, 16, 32, 85, 0, 1 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.ExecuteProfileFlashCmd;
            }
            else if (receiveBuffer[0] == 67 && receiveBuffer[1] >= 128 && receiveBuffer[1] <= 159)
            {
                return InterfaceZeroResponseTypes.GetKeyLogData;
            }
            else if (receiveBuffer[0] == 67 && receiveBuffer[1] == 1)
            {
                return InterfaceZeroResponseTypes.GetKeyLogData;
            }
            else if (receiveBuffer[0] == 67)
            {
                if (receiveBuffer[1] >= 128 && receiveBuffer[1] <= 159)
                {
                    return InterfaceZeroResponseTypes.GetKeyLogData;
                }
                else if (receiveBuffer[1] == 0)
                {
                    //bSetKeyEvent
                }
                else if (receiveBuffer[1] == 1)
                {
                    //array[4] = keyCodeTable X/Y packed struct
                    //array[5] = keystate packed struct
                    //FN + Special key response
                }
            }
            else if (receiveBuffer[0] == 0xc0 && receiveBuffer[1] == 0x81)
            {
                return InterfaceZeroResponseTypes.AuraSyncProtocolUpdateCommand;
            }

            return InterfaceZeroResponseTypes.InvalidResponse;
        }

        //Warning: This function may throw a TimeoutException
        private void GetIface0Response(InterfaceZeroResponseTypes responseType, out byte[] outBuffer, TimeSpan? timeout = null, bool copyBuffer = true)
        {
            if (timeout == null)
            {
                timeout = TimeSpan.FromSeconds(5);
            }

            byte[] localBufCopy = null;
            var event_1 = new AutoResetEvent(false);

            void eventSubscriber(InterfaceZeroResponseTypes respType, ReadOnlyCollection<byte> buf)
            {
                if (respType == responseType)
                {
                    if (copyBuffer)
                    {
                        localBufCopy = new byte[buf.Count];
                        buf.CopyTo(localBufCopy, 0);
                    }

                    event_1.Set();
                }
            }

            OnValidIface0ResponseEvent += eventSubscriber;
            bool isTimedOut = !event_1.WaitOne(timeout.Value);
            OnValidIface0ResponseEvent -= eventSubscriber;

            outBuffer = localBufCopy;

            if (isTimedOut)
            {
                throw new TimeoutException("Timed out while waiting for a response of the type " + responseType);
            }
        }

        private bool WaitForIface0Confirmation(InterfaceZeroResponseTypes responseType, TimeSpan? timeout = null)
        {
            try
            {
                GetIface0Response(responseType, out _, timeout, false);
                return true;
            }
            catch (TimeoutException)
            {
                //Timeouts happen because the keyboard doesn't respond sometimes
            }

            return false;
        }
        #endregion

        #region Unimplemented Functions
        public KeyLogData GetKeylogHistory()
        {
            throw new NotImplementedException(); //TODO
        }

        public void KeylogModeSwitch(bool state)
        {
            throw new NotImplementedException(); //TODO
        }

        public void SetEffect_WriteMultiStaticColorData(MultiStaticData arg1)
        {
            throw new NotImplementedException(); //TODO
        }

        public Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode)
        {
            throw new NotImplementedException(); //TODO
        }
        #endregion

        #region Claymore specific functions
        private void EnableHIDControl(bool state)
        {
            byte[] array = new byte[64];
            array[0] = 65;
            array[1] = (byte)(state ? 1 : 0);

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.HIDControlStateSwitch);
        }
        private void SwitchToAuraSyncMode()
        {
            byte[] array = new byte[64];
            array[0] = 65;
            array[1] = 3;

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.HIDControlStateSwitch);
        }
        private void SetByteSelectedEffectWritableData(byte newValue)
        {
            byte[] array = new byte[64];
            array[0] = 81;
            array[1] = 40; //write type 40
            array[4] = newValue;

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);
        }
        #endregion
    }
}
