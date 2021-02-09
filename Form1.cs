using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctc
{
    public partial class Form1 : Form
    {
        static public string LOCATION = @"";

        static private uint SEQUENCE = 0;

        public Form1()
        {
            DateTime dt = DateTime.Now;
            string today = dt.ToString("yyMMddHHmm");
            SEQUENCE = uint.Parse(today);
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        // Main loop
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage()) {
                Image img = Clipboard.GetImage();
                if (img is not null) {
                    String filename = $"{SEQUENCE++}.png";
                    img.Save(LOCATION + filename, System.Drawing.Imaging.ImageFormat.Png);
                    img.Dispose();
                    Clipboard.Clear();
                }
            }
        }
    }
}
