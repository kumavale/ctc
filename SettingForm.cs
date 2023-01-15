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
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            load_setting();
            label5.Text = Application.ProductVersion;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;  // Disable Apply button
        }

        public void reflect_sequence()
        {
            numericUpDown1.Value = MainForm.SEQUENCE;
        }

        // OK
        private void button1_Click(object sender, EventArgs e)
        {
            if (apply() is false) {
                return;
            }
            this.Close();
        }

        // Cancel
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Apply
        private void button3_Click(object sender, EventArgs e)
        {
            apply();
        }

        private bool apply()
        {
            // Apply filename_format
            if (Token.tokenize(filename_format.Text) is false) {
                return false;
            }

            // Apply location
            if (radioButton1.Checked) {
                MainForm.LOCATION_TYPE = 0;
                MainForm.LOCATION = @"%userprofile%\Pictures\";
            } else if (radioButton2.Checked) {
                MainForm.LOCATION_TYPE = 1;
                MainForm.LOCATION = @"%userprofile%\Desktop\";
            } else if (radioButton3.Checked) {
                MainForm.LOCATION_TYPE = 2;
            } else if (radioButton4.Checked) {
                MainForm.LOCATION_TYPE = 3;
                if (!System.IO.Directory.Exists(location.Text)) {
                    MessageBox.Show("invalid location: " + location.Text);
                    return false;
                }
                MainForm.LOCATION = location.Text;
                if (MainForm.LOCATION.Last() is not '\\') {
                    MainForm.LOCATION += '\\';
                }
            }
            MainForm.LOCATION = Environment.ExpandEnvironmentVariables(MainForm.LOCATION);

            // Apply filetype
            string filetype = null;
            if (type_bmp.Checked) {
                filetype = "bmp";
                MainForm.FILE_TYPE = ImageFormat.Bmp;
            } else if (type_png.Checked) {
                filetype = "png";
                MainForm.FILE_TYPE = ImageFormat.Png;
            } else if (type_jpg.Checked) {
                filetype = "jpg";
                MainForm.FILE_TYPE = ImageFormat.Jpeg;
            } else if (type_gif.Checked) {
                filetype = "gif";
                MainForm.FILE_TYPE = ImageFormat.Gif;
            }

            // Apply sequence number
            MainForm.SEQUENCE = (uint)numericUpDown1.Value;

            // Apply digits of sequence number
            switch (comboBox1.Text) {
                case "Auto": MainForm.DIGITS_OF_SEQUENCE = 0; break;
                default:     MainForm.DIGITS_OF_SEQUENCE = byte.Parse(comboBox1.Text); break;
            }

            // Apply digits of random
            MainForm.DIGITS_OF_RAND = (byte)numericUpDown2.Value;

            // Apply ask overwritten
            MainForm.ASK_OVERWRITTEN = checkBox1.Checked;

            // Save
            Properties.Settings.Default.location           = location.Text;
            Properties.Settings.Default.location_type      = MainForm.LOCATION_TYPE;
            Properties.Settings.Default.filetype           = filetype;
            Properties.Settings.Default.filename_format    = filename_format.Text;
            Properties.Settings.Default.sequence           = MainForm.SEQUENCE;
            Properties.Settings.Default.digits_of_sequence = MainForm.DIGITS_OF_SEQUENCE;
            Properties.Settings.Default.digits_of_rand     = MainForm.DIGITS_OF_RAND;
            Properties.Settings.Default.ask_overwritten    = MainForm.ASK_OVERWRITTEN;
            Properties.Settings.Default.Save();

            button3.Enabled = false;
            return true;
        }

        private void load_setting() {
            location.Text = Properties.Settings.Default.location;

            if (MainForm.LOCATION_TYPE == 0) { radioButton1.Checked = true; }
            else if (MainForm.LOCATION_TYPE == 1) { radioButton2.Checked = true; }
            else if (MainForm.LOCATION_TYPE == 2) { radioButton3.Checked = true; }
            else if (MainForm.LOCATION_TYPE == 3) { radioButton4.Checked = true; }

            if (MainForm.FILE_TYPE == ImageFormat.Bmp) {
                type_bmp.Checked = true;
            } else if (MainForm.FILE_TYPE == ImageFormat.Png) {
                type_png.Checked = true;
            } else if (MainForm.FILE_TYPE == ImageFormat.Jpeg) {
                type_jpg.Checked = true;
            } else if (MainForm.FILE_TYPE == ImageFormat.Gif) {
                type_gif.Checked = true;
            }

            filename_format.Text = Properties.Settings.Default.filename_format;
            filename_format.SelectionStart = filename_format.Text.Length;

            numericUpDown1.Value = MainForm.SEQUENCE;

            switch (MainForm.DIGITS_OF_SEQUENCE) {
                case 0:  comboBox1.Text = "Auto"; break;
                default: comboBox1.Text = MainForm.DIGITS_OF_SEQUENCE.ToString(); break;
            }

            numericUpDown2.Value = MainForm.DIGITS_OF_RAND;

            checkBox1.Checked = MainForm.ASK_OVERWRITTEN;
        }

        private void component_button_Click(object sender, EventArgs e)
        {
            int selection_start = filename_format.SelectionStart;
            filename_format.Text = filename_format.Text.Insert(
                selection_start,
                (sender as Button).Text
            );
            filename_format.Select(
                selection_start + (sender as Button).Text.Length,
                0
            );
            filename_format.Focus();
        }

        private void apply_button_enable(object sender, EventArgs e)
        {
            button3.Enabled = true;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(
                new ProcessStartInfo(this.linkLabel1.Text) {
                    UseShellExecute = true,
                }
            );
        }
    }
}
