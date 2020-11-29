using System;
using System.Drawing;

namespace OpenAsusKeyboardRGB.KBInterfaces
{
    //TODO: Add animation frame getter setters
    //TODO: Add macro stuff(?)
    //TODO: Implement generic FW update protocol(?)
    public interface IArmouryProtocolKB : IBasicHIDKB
    {
        void SetProfileIndex(byte newIndex);
        byte GetProfileIndex();
        KeyLogData GetKeylogHistory();
        void KeylogModeSwitch(bool state);
        void AuraSyncModeSwitch(bool state);
        void ExecuteProfileFlashCmd();

        Tuple<int, int> GetMultiStaticColorDataIndexByVKCode(int virtualKeyCode);
        void SetEffect_WriteMultiStaticColorData(MultiStaticData arg1);

        void SetEffect_Breathing(Color breathingColor1, Color breathingColor2, byte brightness, BreathingTypes breathingType, BreathingSpeeds speed);
        void SetEffect_ColorCycle(ColorCycleSpeeds colorCycleSpeed, byte brightness);
        void SetEffect_Static(Color mainColor, Color backgroundColor, byte brightness);
    }

    public enum ColorCycleSpeeds
    {
        Fast,
        Medium,
        Slow
    };
    public enum BreathingTypes
    {
        Single,
        Double,
        Random
    };
    public enum BreathingSpeeds
    {
        Fast,
        Medium,
        Slow
    };
    public struct ColorAndBrightness
    {
        public byte brightness;
        public Color color;
    }
    public class MultiStaticData
    {
        //The size of this array is hardcoded in the official app and is same for every keyboard
        public ColorAndBrightness[,] colorAndBrightness = new ColorAndBrightness[16, 9];
    }
    public class KeyLogData
    {
        //The size of this array is hardcoded too
        public byte[,] keyPressCounts = new byte[16, 9];
    }
}
