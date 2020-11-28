﻿using RogArmouryKbRevengGUI.KBInterfaces;
using RogArmouryKbRevengGUI_NETFW.KeyMappings;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace RogArmouryKbRevengGUI
{
    public partial class Form1 : Form
    {
        IArmouryProtocolKB ourKeyboard = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (IArmouryProtocolKB currentKBInstance in ReflectiveEnumerator.GetEnumerableOfType<IArmouryProtocolKB>())
            {
                if (currentKBInstance.DoesExistOnSystem())
                {
                    ourKeyboard = currentKBInstance;
                    ourKeyboard.Connect();
                    infoDeviceNameLabel.Text = ourKeyboard.PrettyName;
                    break;
                }
            }

            if (ourKeyboard == null)
            {
                Console.WriteLine("No compatible device found. Exiting...");
                Console.ReadKey();
            }
        }

        private void staticChangeColorBtn_Click(object sender, EventArgs e)
        {
            ourKeyboard.SetEffect_Static(staticEffectColorPbox.BackColor, Color.Empty, 100);
        }

        private void saveColorCycleBtn_Click(object sender, EventArgs e)
        {
            ColorCycleSpeeds speed = ColorCycleSpeeds.Medium;
            if (colorCycleFast.Checked)
            {
                speed = ColorCycleSpeeds.Fast;
            }
            else if (colorCycleMedium.Checked)
            {
                speed = ColorCycleSpeeds.Medium;
            }
            else if (colorCycleSlow.Checked)
            {
                speed = ColorCycleSpeeds.Slow;
            }

            ourKeyboard.SetEffect_ColorCycle(speed, 100);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ourKeyboard.ExecuteProfileFlashCmd();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var msData = new MultiStaticData();

            var enumVals = (Keys[])Enum.GetValues(typeof(Keys));
            for (int i = 0; i < enumVals.Length; i++)
            {
                try
                {
                    var idx = ourKeyboard.GetMultiStaticColorDataIndexByVKCode((int)enumVals[i]);
                    msData.colorAndBrightness[idx.Item1, idx.Item2].brightness = 100;
                    msData.colorAndBrightness[idx.Item1, idx.Item2].color = GetRainbowGradient((float)i / (float)enumVals.Length);
                }
                catch { }
            }

            ourKeyboard.SetEffect_WriteMultiStaticColorData(msData);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ourKeyboard.AuraSyncModeSwitch(true);

            IAuraSyncProtocolKB iface = ourKeyboard as IAuraSyncProtocolKB;

            var maxlen = iface.GetDirectColorCanvasMaxLength();
            Color[,] colorArr = new Color[maxlen.Item2, maxlen.Item1];

            numericUpDown2.Maximum = colorArr.Length - 1;
            numericUpDown2.Value = Math.Min(numericUpDown2.Maximum, numericUpDown2.Value);

            int x = (int)numericUpDown2.Value / maxlen.Item1;
            int y = (int)numericUpDown2.Value % maxlen.Item1;

            colorArr[x, y] = Color.Red;
            iface.SetDirectColorCanvas(colorArr);

            //ourKeyboard.AuraSyncModeSwitch(false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ourKeyboard.SetProfileIndex((byte)newProfileIndexNumBox.Value);
        }

        private void forceExitAuraSyncModeBtn_Click(object sender, EventArgs e)
        {
            ourKeyboard.AuraSyncModeSwitch(false);
            ourKeyboard.SetProfileIndex(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BreathingSpeeds speed = BreathingSpeeds.Medium;
            if (breathingFast.Checked)
            {
                speed = BreathingSpeeds.Fast;
            }
            else if (breathingMedium.Checked)
            {
                speed = BreathingSpeeds.Medium;
            }
            else if (breathingSlow.Checked)
            {
                speed = BreathingSpeeds.Slow;
            }

            BreathingTypes type = BreathingTypes.Single;
            if (breathingDoubleColorCheckBox.Checked)
            {
                type = BreathingTypes.Double;
            }
            if (breathingRandomColorCheckBox.Checked)
            {
                type = BreathingTypes.Random;
            }
            ourKeyboard.SetEffect_Breathing(pictureBox1.BackColor, pictureBox2.BackColor, 100, type, speed);
        }

        private void staticEffectColorPbox_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            staticEffectColorPbox.BackColor = colorDialog1.Color;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pictureBox1.BackColor = colorDialog1.Color;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pictureBox2.BackColor = colorDialog1.Color;
        }

        private static Color GetRainbowGradient(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TODO: Add a state to keep track of the aura sync mode to avoid unnecessary calls to this
            ourKeyboard.AuraSyncModeSwitch(true);
            
            IAuraSyncProtocolKB iface = ourKeyboard as IAuraSyncProtocolKB;

            var maxlen = iface.GetDirectColorCanvasMaxLength();
            Color[,] colorArr = new Color[maxlen.Item2, maxlen.Item1];

            numericUpDown2.Maximum = colorArr.Length - 1;
            numericUpDown2.Value = Math.Min(numericUpDown2.Maximum, numericUpDown2.Value);

            var enumVals = (AsusAuraSDKKeys[])Enum.GetValues(typeof(AsusAuraSDKKeys));
            for (int i = 0; i < enumVals.Length; i++)
            {
                try
                {
                    var idx = iface.GetDirectColorCanvasIndexByAuraSDKKey(enumVals[i]);
                    colorArr[idx.Item1, idx.Item2] = GetRainbowGradient((float)i / (float)enumVals.Length);
                }
                catch (ArgumentException)
                {
                    //GetMultiStaticColorDataIndexByVKCode will throw an ArgumentException for unmapped keys
                    continue;
                }
            }

            iface.SetDirectColorCanvas(colorArr);
        }
    }
}
