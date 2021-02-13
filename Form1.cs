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

using Microsoft.WindowsAPICodePack.Dialogs;

namespace ctc
{
    public partial class Form1 : Form
    {
        static public ImageFormat FILE_TYPE = ImageFormat.Jpeg;
        static public byte LOCATION_TYPE = 0;  // My Pictures
        static public string LOCATION = @"";

        static private uint SEQUENCE = 0;

        public Form1()
        {
            // init sequence number
            DateTime dt = DateTime.Now;
            string today = dt.ToString("yyMMddHHmm");
            SEQUENCE = uint.Parse(today);

            // load settings
            LOCATION = ConfigurationManager.AppSettings["location"];
            LOCATION_TYPE = byte.Parse(ConfigurationManager.AppSettings["location_type"]);
            string file_type = ConfigurationManager.AppSettings["filetype"];
            if (file_type == "bmp") {
                FILE_TYPE = ImageFormat.Bmp;
            } else if (file_type == "png") {
                FILE_TYPE = ImageFormat.Png;
            } else if (file_type == "jpg") {
                FILE_TYPE = ImageFormat.Jpeg;
            } else if (file_type == "gif") {
                FILE_TYPE = ImageFormat.Gif;
            }

            // prepare location
            if (LOCATION_TYPE == 0) {
                LOCATION = @"%userprofile%\Pictures\";
            } else if (LOCATION_TYPE == 1) {
                LOCATION = @"%userprofile%\Desktop\";
            }
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
        static bool DIALOG_OPENING = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage() && !DIALOG_OPENING) {
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

                    if (LOCATION_TYPE == 2) {
                        DIALOG_OPENING = true;
                        var dialog = new CommonOpenFileDialog() {
                            Title = "Open Folder",
                            RestoreDirectory = true,
                            IsFolderPicker = true,
                        };
                        if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                            img.Save(dialog.FileName + "\\" + filename, FILE_TYPE);
                        }
                        DIALOG_OPENING = false;
                    } else {
                        img.Save(LOCATION + filename, FILE_TYPE);
                    }
                    img.Dispose();
                    Clipboard.Clear();
                }
            }
        }
    }
}
