using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        static public ulong SEQUENCE = 0;
        static public byte DIGITS_OF_SEQUENCE = 0;  // 0 == Auto
        static public bool ASK_OVERWRITTEN = false;
        static public List<Token> TOKENS = new List<Token>() {
            new Token(TokenKind.Sequence),
        };

        static private Form2 SETTING_FORM = null;

        public Form1()
        {
            // load settings
            LOCATION = Properties.Settings.Default.location;
            LOCATION_TYPE = Properties.Settings.Default.location_type;
            string file_type = Properties.Settings.Default.filetype;
            if (file_type == "bmp") {
                FILE_TYPE = ImageFormat.Bmp;
            } else if (file_type == "png") {
                FILE_TYPE = ImageFormat.Png;
            } else if (file_type == "jpg") {
                FILE_TYPE = ImageFormat.Jpeg;
            } else if (file_type == "gif") {
                FILE_TYPE = ImageFormat.Gif;
            }
            string filename_format = Properties.Settings.Default.filename_format;
            Token.tokenize(filename_format);
            SEQUENCE = Properties.Settings.Default.sequence;
            DIGITS_OF_SEQUENCE = Properties.Settings.Default.digits_of_sequence;
            ASK_OVERWRITTEN = Properties.Settings.Default.ask_overwritten;

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
            Properties.Settings.Default.sequence = SEQUENCE;
            Application.Exit();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            if (SETTING_FORM is null || SETTING_FORM.IsDisposed) {
                SETTING_FORM = new Form2();
                SETTING_FORM.Show();
            } else {
                SETTING_FORM.TopMost = true;
            }
        }

        // Main loop
        static bool DIALOG_OPENING = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage() && !DIALOG_OPENING) {
                Image img = Clipboard.GetImage();
                if (img is not null) {
                    string filename = Token.compile_filename();

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
                        string path = LOCATION + filename;
                        if (ASK_OVERWRITTEN && File.Exists(path)) {
                            DIALOG_OPENING = true;
                            var result = MessageBox.Show(
                                $"A file named \"{filename}\" already exists.\nDo you want to overwrite it?",
                                "CTC",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2
                            );
                            if (result is DialogResult.Yes) {
                                img.Save(path, FILE_TYPE);
                            }
                            DIALOG_OPENING = false;
                        } else {
                            img.Save(path, FILE_TYPE);
                        }
                    }

                    if (SETTING_FORM is not null) {
                        SETTING_FORM.reflect_sequence();
                    }
                    img.Dispose();
                    Clipboard.Clear();
                }
            }
        }
    }
}
