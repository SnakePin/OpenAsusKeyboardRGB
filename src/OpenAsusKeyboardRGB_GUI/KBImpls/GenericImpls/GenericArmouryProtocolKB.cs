using HidSharp;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using RogArmouryKbRevengGUI.KBInterfaces;
using RogArmouryKbRevengGUI_NETFW.KeyMappings;

namespace RogArmouryKbRevengGUI.KBImpls.GenericImpls
{
    abstract class GenericArmouryProtocolKB : GenericHIDKeyboard, IArmouryProtocolKB, IAuraSyncProtocolKB
    {
        protected override int DeviceVID => 2821;
        public abstract string PrettyName { get; }

        public virtual void Connect()
        {
            HidDevice iface0Device;
            if ((iface0Device = Utils.GetHidDevice(2821, DevicePID, 1, 0xFF00, out _)) != null)
            {
                //We will use the report ID of the interface 3
                _ = Utils.GetHidDevice(2821, DevicePID, 1, 0xFFC0, out DeviceReportIDToUse);
                DeviceHIDStream = iface0Device.Open();
                DeviceHIDStream.ReadTimeout = 3000;
                DeviceInputHandler = iface0Device.GetReportDescriptor().CreateHidDeviceInputReceiver();
                DeviceInputHandler.Received += new EventHandler(OnHIDInputReceived);
                DeviceInputHandler.Start(DeviceHIDStream);
                DeviceMaximumInputReportLen = iface0Device.GetMaxInputReportLength();
            }
        }

        public virtual void SetEffect_Static(Color mainColor, Color backgroundColor, byte brightness)
        {
            InternalSendWriteType44((byte)ByteSelectedEffectTypes.Static, 0xFF, 0xFF,
                0, brightness, 0xFF, 0, mainColor, backgroundColor);
        }

        public virtual void SetEffect_ColorCycle(ColorCycleSpeeds colorCycleSpeed, byte brightness)
        {
            var speedMap = new Dictionary<ColorCycleSpeeds, byte>
            {
                { ColorCycleSpeeds.Fast, 1 },
                { ColorCycleSpeeds.Medium, 3 },
                { ColorCycleSpeeds.Slow, 6 }
            };

            InternalSendWriteType44((byte)ByteSelectedEffectTypes.ColorCycle, 0xFF, 0xFF,
                0, brightness, speedMap[colorCycleSpeed], 0, Color.Empty, Color.Empty);
        }

        public virtual void SetEffect_Breathing(Color breathingColor1, Color breathingColor2,
            byte brightness, BreathingTypes breathingType, BreathingSpeeds speed)
        {
            var speedMap = new Dictionary<BreathingSpeeds, byte>
            {
                { BreathingSpeeds.Fast, 2 },
                { BreathingSpeeds.Medium, 4 },
                { BreathingSpeeds.Slow, 7 }
            };

            InternalSendWriteType44((byte)ByteSelectedEffectTypes.Breathing, 0xFF, 0xFF,
                (byte)(breathingType == BreathingTypes.Double ? 1 : 0),
                brightness, speedMap[speed],
                (byte)(breathingType == BreathingTypes.Random ? 1 : 0),
                breathingColor1, breathingColor2);
        }

        protected void InternalSendWriteType44(byte byteSelectedEffect, byte direction, byte width,
            byte singleDouble, byte brightness, byte speed, byte isRandColor, Color color1, Color color2)
        {
            byte[] array = new byte[64];
            array[0] = 81;
            array[1] = 44; //write type 44

            array[2] = byteSelectedEffect;
            array[3] = 0; //choose custom byte?
            array[4] = speed;
            array[5] = brightness;

            array[6] = Utils.ConstructByteFromNibbles(singleDouble, isRandColor);
            array[7] = direction;
            array[8] = width;

            array[9] = color1.R;
            array[10] = color1.G;
            array[11] = color1.B;
            array[12] = color2.R;
            array[13] = color2.G;
            array[14] = color2.B;

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);
        }

        public virtual void SetEffect_WriteMultiStaticColorData(MultiStaticData arg1)
        {
            byte[] array = new byte[64];
            array[0] = 81;
            array[1] = 168; //write type 168

            int num2 = 4;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (num2 < 64)
                    {
                        array[num2++] = arg1.colorAndBrightness[i, j].brightness.Clamp<byte>(0, 100);
                        array[num2++] = arg1.colorAndBrightness[i, j].color.R;
                        array[num2++] = arg1.colorAndBrightness[i, j].color.G;
                        array[num2++] = arg1.colorAndBrightness[i, j].color.B;
                        if (i == 15 && num2 == 36)
                        {
                            //array[3] = 1;

                            SendIface0ByteArray(array);
                            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);
                        }
                    }
                    if (num2 >= 64)
                    {
                        num2 = 4;
                        //array[3] = 1;

                        SendIface0ByteArray(array);
                        WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);
                    }
                }
            }
            SetWritableData_MultiStatic_EffectMap_AllStatic();
            ExecuteProfileFlashCmd();
        }


        //This is for specifying which effect layer applies to which key, in here we just set one static layer across all keys
        private void SetWritableData_MultiStatic_EffectMap_AllStatic()
        {
            var m_byteSelectEffectArray = new byte[16, 8];
            for (int i = 0; i < m_byteSelectEffectArray.GetLength(0); i++)
            {
                for (int j = 0; j < m_byteSelectEffectArray.GetLength(1); j++)
                {
                    //Effect applied on all keys will be static
                    m_byteSelectEffectArray[i, j] = (byte)ByteSelectedEffectTypes.Static;
                }
            }

            byte[] array = new byte[64];
            array[0] = 81;
            array[1] = 160; //write type 160

            array[2] = 0;
            array[3] = 0;

            array[4] = 0;
            array[5] = 7;
            //Array.Copy(byteRev, 0, array, 6, 2); Is this always zero?
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    array[8 + num] = m_byteSelectEffectArray[i, j];
                    num++;
                }
            }

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);

            array[4] = 7;
            array[5] = 7;
            num = 0;
            for (int k = 7; k < 14; k++)
            {
                for (int l = 0; l < 8; l++)
                {
                    array[8 + num] = m_byteSelectEffectArray[k, l];
                    num++;
                }
            }
            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);

            array[4] = 14;
            array[5] = 2;
            num = 0;
            for (int m = 14; m < 16; m++)
            {
                for (int n = 0; n < 8; n++)
                {
                    array[8 + num] = m_byteSelectEffectArray[m, n];
                    num++;
                }
            }

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);

            //ExecuteProfileFlashCmd();
        }

        public virtual void ExecuteProfileFlashCmd()
        {
            byte[] array = new byte[64];
            array[0] = 80;
            array[1] = 85; //iKey (is always 85 for TUF K7)
            array[4] = 0; //iMode (is always 0 for TUF K7)
            SendIface0ByteArray(array);

            WaitForIface0Confirmation(InterfaceZeroResponseTypes.ExecuteProfileFlashCmd);
        }

        public virtual void SetProfileIndex(byte newIndex)
        {
            byte[] array = new byte[64];
            array[0] = 81;
            array[1] = 0;
            array[4] = newIndex;
            SendIface0ByteArray(array);

            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SetWritableData);
        }

        public virtual byte GetProfileIndex()
        {
            byte[] array = new byte[64];
            array[0] = 82;
            array[1] = 0;
            SendIface0ByteArray(array);

            GetIface0Response(InterfaceZeroResponseTypes.GetWritableData, out byte[] buf);

            return buf[4];
        }

        public virtual void AuraSyncModeSwitch(bool state)
        {
            byte[] array = new byte[64];
            array[0] = 65; //Aura sync mode set
            array[1] = (byte)(state ? 1 : 0);

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.SyncSwitch);
        }

        public virtual void KeylogModeSwitch(bool state)
        {
            byte[] array = new byte[64];
            array[0] = 67; //Keylog mode set
            array[1] = (byte)(state ? 1 : 0);

            SendIface0ByteArray(array);
            WaitForIface0Confirmation(InterfaceZeroResponseTypes.KeyLogSwitch);
        }

        public virtual KeyLogData GetKeylogHistory()
        {
            //README! If you're reading this code and wondering what the fuck is a keylogger doing in a keyboard's firmware
            //please do know that this keylogger only keeps the count of the keypresses and not their timestamps
            //a feature like this probably can be used for something like a heatmap, which is exactly what the original software is using it for.
            //Now, indeed that an application can constantly query the keylog history and get the keypresses by checking the difference in the press count
            //don't ask me why ASUS implemented this in the keyboard's firmware.

            byte[] array = new byte[64];
            array[0] = 67; //Keylogger command suffix(?)

            var keylogArray = new byte[128];
            for (byte i = 0; i < 3; i++)
            {
                //Keylog history get (128, 129, 130)
                array[1] = (byte)(i + 128);

                SendIface0ByteArray(array);
                GetIface0Response(InterfaceZeroResponseTypes.GetKeyLogData, out byte[] byteIN);

                //First two packets have 56 key's state whereas the third one has only 16 key's state
                if (i < 2)
                {
                    Array.Copy(byteIN, 4, keylogArray, i * 56, 56);
                }
                else
                {
                    Array.Copy(byteIN, 4, keylogArray, i * 56, 16);
                }
            }

            var retVal = new KeyLogData();

            //TODO: Check if these values differ between different ROG Armoury keyboards
            const int keylogXMax = 16;
            const int keylogYMax = 8;

            for (int x = 0; x < keylogXMax; x++)
            {
                for (int y = 0; y < keylogYMax; y++)
                {
                    retVal.keyPressCounts[x, y] = keylogArray[x * keylogYMax + y];
                }
            }

            return retVal;
        }

        public abstract Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode);
        public abstract Tuple<int, int> GetDirectColorCanvasMaxLength();
        public virtual Tuple<int, int> GetDirectColorCanvasIndexByAuraSDKKey(AsusAuraSDKKeys key)
        {
            //Mapping the keys that are left unmapped by the Aura SDK
            if (key == AsusAuraSDKKeys.UNOFFICIAL_ISO_HASH)
            {
                return Tuple.Create(3, 13);
            }
            else if (key == AsusAuraSDKKeys.UNOFFICIAL_ISO_BACKSLASH)
            {
                return Tuple.Create(4, 1);
            }

            var rgbKey = AuraSyncProtocolKeyMappings.GenericMapping.FirstOrDefault(x => x.KeyCode == (ushort)key);
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

        public virtual void SetDirectColorCanvas(Color[,] colorDataArg)
        {
            //NOTE: the number of rows and columns for all keyboards are as follows
            //Claymore 23,8
            //TUF K5 5,1
            //Charm(Flare Normal) 24,6
            //Flare COD 24,6
            //Rog CTRL(Rog Scope Normal) 24,6
            //Flare PNK 24,6
            //TUF K7 23,6
            //Scope TKL 26,7
            //This function doesn't work with TUFKB(K5) and Claymore(any model including the core)

            var colorData = colorDataArg.FlattenMatrix();

            byte[] buffer = new byte[64];
            int XMax = GetDirectColorCanvasMaxLength().Item1;
            int YMax = GetDirectColorCanvasMaxLength().Item2;

            int uVar4 = XMax * YMax;
            byte[] generated0x54field = new byte[uVar4];
            for (int y = 0; y < YMax; y++)
            {
                for (int x = 0; x < XMax; x++)
                {
                    generated0x54field[(y * XMax) + x] = (byte)(x * 8 + y);
                }
            }

            buffer[0] = 0xc0;
            buffer[1] = 0x81;

            long lVar10 = 0;
            do
            {
                buffer[2] = (byte)uVar4;
                buffer[3] = (byte)(uVar4 >> 8);

                long uVar7 = 15;

                if (uVar4 != 0)
                {
                    if (uVar4 < 16)
                    {
                        uVar7 = uVar4;
                    }

                    int lVar9 = 0;
                    do
                    {
                        int currentIndexInBuffer = lVar9 * 4 + 4;

                        buffer[currentIndexInBuffer] = generated0x54field[lVar10];

                        buffer[++currentIndexInBuffer] = colorData[lVar10].R;
                        buffer[++currentIndexInBuffer] = colorData[lVar10].G;
                        buffer[++currentIndexInBuffer] = colorData[lVar10].B;

                        lVar9++;
                        lVar10++;
                    } while (lVar9 < uVar7);
                }
                SendIface0ByteArray(buffer);
                if (uVar4 < 16) break;
                uVar4 -= 15;
            } while (uVar4 != 0);

        }

        #region Enums
        protected enum ByteSelectedEffectTypes
        {
            Static,
            Breathing,
            ColorCycle,
            Reactive,
            Wave,
            StarryNight,
            Quicksand,
            Current,
            Raindrop,
            OFF = 254
        };
        protected enum InterfaceZeroResponseTypes
        {
            GetInfo,
            GetConstantValue,
            SetWritableData,
            GetWritableData,
            ExecuteMacroFlash,
            ExecuteProfileFlashCmd,
            GetKeyLogData,
            KeyLogSwitch,
            SyncSwitch,
            AuraSyncProtocolUpdateCommand,
            InvalidResponse
        };
        #endregion

        #region HID response handling
        protected virtual void OnHIDInputReceived(object sender, EventArgs e)
        {
            var inputReportBuffer = new byte[DeviceMaximumInputReportLen];

            //Flush pending report buffer
            while (DeviceInputHandler.TryRead(inputReportBuffer, 0, out _))
            {
                //Dispatch by removing the report ID
                DispatchInterfaceZeroResponse(inputReportBuffer.Skip(1).ToArray());
            }
        }

        protected virtual InterfaceZeroResponseTypes Iface0GetResponseType(byte[] receiveBuffer)
        {
            if (receiveBuffer.Length < 2)
            {
                return InterfaceZeroResponseTypes.InvalidResponse;
            }

            if (receiveBuffer[0] == 18 /*&& (new byte[] { 1, 0, 32, 18, getInfoID }.Contains(receiveBuffer[1]))*/)
            {
                return InterfaceZeroResponseTypes.GetInfo;
            }
            else if (receiveBuffer[0] == 64 && (new byte[] { 0, 32, 96, 97, 98, 99 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.GetConstantValue;
            }
            else if (receiveBuffer[0] == 81 && (new byte[] { 0, 44, 160, 48, 168, 24, 32, 41, 145, 144 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.SetWritableData;
            }
            else if (receiveBuffer[0] == 82 && (new byte[] { 0, 44, 160, 48, 168, 32, 41, 145, 144 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.GetWritableData;
            }
            else if (receiveBuffer[0] == 83)
            {
                return InterfaceZeroResponseTypes.ExecuteMacroFlash;
            }
            else if (receiveBuffer[0] == 80 && (new byte[] { 0, 16, 32, 85, 0, 1 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.ExecuteProfileFlashCmd;
            }
            else if (receiveBuffer[0] == 67 && (new byte[] { 128, 129, 130 }.Contains(receiveBuffer[1])))
            {
                return InterfaceZeroResponseTypes.GetKeyLogData;
            }
            else if (receiveBuffer[0] == 67 && (receiveBuffer[1] == 1 || receiveBuffer[1] == 0))
            {
                return InterfaceZeroResponseTypes.KeyLogSwitch;
            }
            else if (receiveBuffer[0] == 65 && (receiveBuffer[1] == 1 || receiveBuffer[1] == 0))
            {
                return InterfaceZeroResponseTypes.SyncSwitch;
            }
            else if (receiveBuffer[0] == 0xc0 && receiveBuffer[1] == 0x81)
            {
                return InterfaceZeroResponseTypes.AuraSyncProtocolUpdateCommand;
            }

            return InterfaceZeroResponseTypes.InvalidResponse;
        }

        protected event Action<InterfaceZeroResponseTypes, ReadOnlyCollection<byte>> OnValidIface0ResponseEvent;

        protected virtual void DispatchInterfaceZeroResponse(byte[] receivedBuffer)
        {
            var respType = Iface0GetResponseType(receivedBuffer);
            Console.WriteLine("[GenericAsusRogKB] Got a response! Response type: " + respType.ToString());
            OnValidIface0ResponseEvent?.Invoke(respType, Array.AsReadOnly(receivedBuffer));
        }

        //Warning: This function may throw a TimeoutException
        protected virtual void GetIface0Response(InterfaceZeroResponseTypes responseType, out byte[] outBuffer, TimeSpan? timeout = null, bool copyBuffer = true)
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

        protected virtual bool WaitForIface0Confirmation(InterfaceZeroResponseTypes responseType, TimeSpan? timeout = null)
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
        #endregion HID response handling
    }
}
