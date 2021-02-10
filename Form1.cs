using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace ctc
{
    public partial class Form1 : Form
    {
        static public ImageFormat FILE_TYPE = ImageFormat.Jpeg;
        static public string LOCATION = @"";

        static private uint SEQUENCE = 0;

        public Form1()
        {
            // init sequence number
            DateTime dt = DateTime.Now;
            string today = dt.ToString("yyMMddHHmm");
            SEQUENCE = uint.Parse(today);

            // load settings
            LOCATION = System.Configuration.ConfigurationManager.AppSettings["location"];

            // prepare location
            LOCATION = Environment.ExpandEnvironmentVariables(LOCATION);
            if (LOCATION.Last() is not '\\') {
                LOCATION += '\\';
            }

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
                    string filename = "";
                    if (FILE_TYPE == ImageFormat.Bmp) {
                        filename = $"{SEQUENCE++}.bmp";
                    } else if (FILE_TYPE == ImageFormat.Png) {
                        filename = $"{SEQUENCE++}.png";
                    } else if (FILE_TYPE == ImageFormat.Jpeg) {
                        filename = $"{SEQUENCE++}.jpg";
                    } else if (FILE_TYPE == ImageFormat.Gif) {
                        filename = $"{SEQUENCE++}.gif";
                    }
                    img.Save(LOCATION + filename, FILE_TYPE);
                    img.Dispose();
                    Clipboard.Clear();
                }
            }
        }
    }
}
