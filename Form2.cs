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

using Microsoft.WindowsAPICodePack.Dialogs;

namespace ctc
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            load_setting();
        }

        // OK
        private void button1_Click(object sender, EventArgs e)
        {
            if (apply() is false) {
                return;
            }
            this.Close();
        }

        // Apply
        private void button2_Click(object sender, EventArgs e)
        {
            apply();
        }

        // Cancel
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool apply()
        {
            // apply location
            if (!System.IO.Directory.Exists(location.Text)) {
                MessageBox.Show("invalid location: " + location.Text);
                return false;
            }
            Form1.LOCATION = location.Text;
            if (Form1.LOCATION.Last() is not '\\') {
                Form1.LOCATION += '\\';
            }

            // apply filetype
            string filetype = "";
            if (type_bmp.Checked) {
                filetype = "bmp";
                Form1.FILE_TYPE = ImageFormat.Bmp;
            }
            else if (type_png.Checked) {
                filetype = "png";
                Form1.FILE_TYPE = ImageFormat.Png;
            }
            else if (type_jpg.Checked) {
                filetype = "jpg";
                Form1.FILE_TYPE = ImageFormat.Jpeg;
            }
            else if (type_gif.Checked) {
                filetype = "gif";
                Form1.FILE_TYPE = ImageFormat.Gif;
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["location"].Value = location.Text;
            config.AppSettings.Settings["filetype"].Value = filetype;
            config.AppSettings.Settings["filename_format"].Value = filename_format.Text;
            config.Save();

            button2.Enabled = false;
            return true;
        }

        private void load_setting() {
            location.Text = Form1.LOCATION;
            if (Form1.FILE_TYPE == ImageFormat.Bmp) {
                type_bmp.Checked = true;
            } else if (Form1.FILE_TYPE == ImageFormat.Png) {
                type_png.Checked = true;
            } else if (Form1.FILE_TYPE == ImageFormat.Jpeg) {
                type_jpg.Checked = true;
            } else if (Form1.FILE_TYPE == ImageFormat.Gif) {
                type_gif.Checked = true;
            }
        }

        private void component_button_Click(object sender, EventArgs e)
        {
            filename_format.Text += (sender as Button).Text;
            button2.Enabled = true;
        }

        private void apply_button_enable(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button_location_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog() {
                Title = "Open Folder",
                RestoreDirectory = true,
                IsFolderPicker = true,
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) {
                return;
            }

            location.Text = dialog.FileName;
        }
    }
}
