﻿using System;
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
using System.Runtime.InteropServices;

using Microsoft.WindowsAPICodePack.Dialogs;

namespace ctc
{
    public partial class MainForm : Form
    {
        public static ImageFormat FILE_TYPE = ImageFormat.Jpeg;
        public static byte LOCATION_TYPE = 0;  // My Pictures
        public static string LOCATION = @"";
        public static uint SEQUENCE = 0;
        public static byte DIGITS_OF_SEQUENCE = 0;  // 0 == Auto
        public static byte DIGITS_OF_RAND = 16;
        public static bool ASK_OVERWRITTEN = false;
        public static List<Token> TOKENS = new List<Token>() {
            new Token(TokenKind.Sequence),
        };

        private static SettingForm SETTING_FORM = null;
        private static ClipBoardWatcher CBW;

        public MainForm()
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
            DIGITS_OF_RAND = Properties.Settings.Default.digits_of_rand;
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

            CBW = new ClipBoardWatcher();
            CBW.DrawClipBoard += (object sender, EventArgs e) => {
                save_image();
            };

            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.sequence = SEQUENCE;
            CBW.Dispose();
            Application.Exit();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            if (SETTING_FORM is null || SETTING_FORM.IsDisposed) {
                SETTING_FORM = new SettingForm();
                SETTING_FORM.Show();
            } else {
                SETTING_FORM.TopMost = true;
            }
        }

        static bool PROCESSING = false;
        private static void save_image()
        {
            if (!PROCESSING && Clipboard.ContainsImage()) {
                PROCESSING = true;
                Image img = Clipboard.GetImage();
                if (img is not null) {
                    string filename = Token.compile_filename();

                    if (LOCATION_TYPE == 2) {
                        var dialog = new CommonOpenFileDialog() {
                            Title = "Open Folder",
                            RestoreDirectory = true,
                            IsFolderPicker = true,
                        };
                        if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                            img.Save(dialog.FileName + "\\" + filename, FILE_TYPE);
                        }
                    } else {
                        string path = LOCATION + filename;
                        if (ASK_OVERWRITTEN && File.Exists(path)) {
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
                PROCESSING = false;
            }
        }
    }

    public class ClipBoardWatcher : IDisposable
    {
        ClipBoardWatcherForm form;

        public event EventHandler DrawClipBoard;

        public ClipBoardWatcher()
        {
            form = new ClipBoardWatcherForm();
            form.StartWatch(raiseDrawClipBoard);
        }

        private void raiseDrawClipBoard()
        {
            if (DrawClipBoard is not null) {
                DrawClipBoard(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            form.Dispose();
        }

        private class ClipBoardWatcherForm : Form
        {
            [DllImport("user32.dll")]
            private static extern IntPtr SetClipboardViewer(IntPtr hwnd);
            [DllImport("user32.dll")]
            private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
            [DllImport("user32.dll")]
            private static extern bool ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNext);

            const int WM_DRAWCLIPBOARD = 0x0308;
            const int WM_CHANGECBCHAIN = 0x030D;

            IntPtr nextHandle;
            System.Threading.ThreadStart proc;

            public void StartWatch(System.Threading.ThreadStart proc)
            {
                this.proc = proc;
                nextHandle = SetClipboardViewer(this.Handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg is WM_DRAWCLIPBOARD) {
                    SendMessage(nextHandle, m.Msg, m.WParam, m.LParam);
                    proc();
                } else if (m.Msg is WM_CHANGECBCHAIN) {
                    if (m.WParam == nextHandle) {
                        nextHandle = m.LParam;
                    } else {
                        SendMessage(nextHandle, m.Msg, m.WParam, m.LParam);
                    }
                }
                base.WndProc(ref m);
            }

            protected override void Dispose(bool disposing)
            {
                ChangeClipboardChain(this.Handle, nextHandle);
                base.Dispose(disposing);
            }
        }
    }
}

