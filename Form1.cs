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
        static public List<Token> TOKENS = new List<Token>() {
            new Token(TokenKind.Sequence),
        };

        static private uint SEQUENCE = 0;

        public Form1()
        {
            // init sequence number
            DateTime dt = DateTime.Now;
            string today = dt.ToString("yyMMddHHmm");
            SEQUENCE = uint.Parse(today);

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
            tokenize(filename_format);

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
                    string filename = compile_filename();

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

        static public bool tokenize(string format)
        {
            List<Token> old_tokens = TOKENS;
            TOKENS.Clear();
            char[] f = format.ToCharArray();
            char[] invalid_chars = System.IO.Path.GetInvalidFileNameChars();

            void invalid_format() {
                MessageBox.Show("invalid format: " + format);
                TOKENS = old_tokens;
            }

            for (uint idx = 0; idx < f.Length; ++idx) {
                if (f[idx] == '{') {
                    string keyword = "";
                    while (idx+1 < f.Length && f[idx+1] != '}') {
                        keyword += f[++idx];
                    }
                    if (f.Length <= idx+1 && f[idx] != '}') {
                        invalid_format();
                        return false;
                    }
                    switch (keyword) {
                        case "sequence": TOKENS.Add(new Token(TokenKind.Sequence));    break;
                        case "Year":     TOKENS.Add(new Token(TokenKind.Year));        break;
                        case "Month":    TOKENS.Add(new Token(TokenKind.Month));       break;
                        case "Day":      TOKENS.Add(new Token(TokenKind.Day));         break;
                        case "hour":     TOKENS.Add(new Token(TokenKind.Hour));        break;
                        case "min":      TOKENS.Add(new Token(TokenKind.Minute));      break;
                        case "sec":      TOKENS.Add(new Token(TokenKind.Second));      break;
                        case "msec":     TOKENS.Add(new Token(TokenKind.MilliSecond)); break;
                        default:
                            invalid_format();
                            return false;
                    }
                    // expect '}'
                    ++idx;
                    continue;
                }

                string str = f[idx].ToString();
                while (idx+1 < f.Length && f[idx+1] != '{') {
                    str += f[++idx];
                }
                int invalid_idx = str.IndexOfAny(invalid_chars);
                if (0 <= invalid_idx) {
                    MessageBox.Show("invalid charactor: '" + str[invalid_idx] + "'");
                    TOKENS = old_tokens;
                    return false;
                }
                TOKENS.Add(new Token(TokenKind.String, str));
            }

            return true;
        }

        static private string compile_filename()
        {
            string filename = "";
            var now = DateTime.Now;

            foreach (Token token in TOKENS) {
                switch (token.kind) {
                    case TokenKind.String:      filename += token.str;       break;
                    case TokenKind.Sequence:    filename += SEQUENCE++;      break;
                    case TokenKind.Year:        filename += now.Year;        break;
                    case TokenKind.Month:       filename += now.Month;       break;
                    case TokenKind.Day:         filename += now.Day;         break;
                    case TokenKind.Hour:        filename += now.Hour;        break;
                    case TokenKind.Minute:      filename += now.Minute;      break;
                    case TokenKind.Second:      filename += now.Second;      break;
                    case TokenKind.MilliSecond: filename += now.Millisecond; break;
                }
            }

            // Append extension
            filename += ".";
            if (FILE_TYPE == ImageFormat.Bmp) {
                filename += "bmp";
            } else if (FILE_TYPE == ImageFormat.Png) {
                filename += "png";
            } else if (FILE_TYPE == ImageFormat.Jpeg) {
                filename += "jpg";
            } else if (FILE_TYPE == ImageFormat.Gif) {
                filename += "gif";
            } else {
                // unreachable
            }

            return filename;
        }
    }
}
