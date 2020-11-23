using RogArmouryKbRevengGUI.InterfaceGenericKeyboard;
using RogArmouryKbRevengGUI.KBInterfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RogArmouryKbRevengGUI
{
    public partial class Form1 : Form
    {
        IGenericAsusRogKB ourKeyboard = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (IGenericAsusRogKB currentKBInstance in ReflectiveEnumerator.GetEnumerableOfType<IGenericAsusRogKB>())
            {
                if (currentKBInstance.DoesExistOnSystem())
                {
                    ourKeyboard = currentKBInstance;
                    ourKeyboard.Connect();
                    infoDeviceNameLabel.Text = ourKeyboard.GetPrettyName();
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
            var rnd = new Random();
            for(int i = 0; i < 128; i++)
            {
                var idx = ourKeyboard.GetMultiStaticColorDataIndexByVKCode(i);
                msData.colorAndBrightness[idx.Item1, idx.Item2].brightness = 100;
                msData.colorAndBrightness[idx.Item1, idx.Item2].color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            }

            ourKeyboard.SetEffect_WriteMultiStaticColorData(msData);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ourKeyboard.AuraSyncModeSwitch(true);

            IAuraSyncKB iface = ourKeyboard as IAuraSyncKB;

            var maxlen = iface.GetDirectColorCanvasMaxLength();
            Color[] dundundun = new Color[maxlen.Item1 * maxlen.Item2];

            numericUpDown2.Maximum = dundundun.Length-1;
            numericUpDown2.Value = Math.Min(numericUpDown2.Maximum, numericUpDown2.Value);

            dundundun[(int)numericUpDown2.Value] = Color.Red;
            /*var rnd = new Random();
            for (int i = 0; i < 128; i++)
            {
                var idx = iface.GetDirectColorCanvasIndexByVKCode(i);
                dundundun[idx.Item1 * maxlen.Item2 + maxlen.Item2] = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            }*/

            iface.SetDirectColorCanvas(dundundun);

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
            if(breathingDoubleColorCheckBox.Checked)
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
    }
}
