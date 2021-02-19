using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctc
{
    public enum TokenKind
    {
        String,
        Sequence,
        Random,
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second,
        MilliSecond,
    }

    public class Token
    {
        public TokenKind kind { get; }
#nullable enable
        public string? str    { get; }
#nullable disable

        public Token(TokenKind kind)
        {
            this.kind = kind;
        }

        public Token(TokenKind kind, string str)
        {
            this.kind = kind;
            this.str  = str;
        }

        static public bool tokenize(string format)
        {
            List<Token> old_tokens = Form1.TOKENS;
            Form1.TOKENS.Clear();
            char[] f = format.ToCharArray();
            char[] invalid_chars = System.IO.Path.GetInvalidFileNameChars();

            void invalid_format() {
                MessageBox.Show("invalid format: " + format);
                Form1.TOKENS = old_tokens;
            }

            for (uint idx = 0; idx < f.Length; ++idx) {
                if (f[idx] == '{') {
                    string keyword = null;
                    while (idx+1 < f.Length && f[idx+1] != '}') {
                        keyword += f[++idx];
                    }
                    if (f.Length <= idx+1 && f[idx] != '}') {
                        invalid_format();
                        return false;
                    }
                    switch (keyword) {
                        case "sequence": Form1.TOKENS.Add(new Token(TokenKind.Sequence));    break;
                        case "rand":     Form1.TOKENS.Add(new Token(TokenKind.Random));      break;
                        case "Year":     Form1.TOKENS.Add(new Token(TokenKind.Year));        break;
                        case "Month":    Form1.TOKENS.Add(new Token(TokenKind.Month));       break;
                        case "Day":      Form1.TOKENS.Add(new Token(TokenKind.Day));         break;
                        case "hour":     Form1.TOKENS.Add(new Token(TokenKind.Hour));        break;
                        case "min":      Form1.TOKENS.Add(new Token(TokenKind.Minute));      break;
                        case "sec":      Form1.TOKENS.Add(new Token(TokenKind.Second));      break;
                        case "msec":     Form1.TOKENS.Add(new Token(TokenKind.MilliSecond)); break;
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
                    Form1.TOKENS = old_tokens;
                    return false;
                }
                Form1.TOKENS.Add(new Token(TokenKind.String, str));
            }

            return true;
        }

        static public string compile_filename()
        {
            string filename = null;
            var now = DateTime.Now;

            foreach (Token token in Form1.TOKENS) {
                switch (token.kind) {
                    case TokenKind.String:      filename += token.str;       break;
                    case TokenKind.Sequence:    filename += $"{Form1.SEQUENCE++}".PadLeft(Form1.DIGITS_OF_SEQUENCE, '0'); break;
                    case TokenKind.Random:      filename += random_string(); break;
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
            if (Form1.FILE_TYPE == ImageFormat.Bmp) {
                filename += "bmp";
            } else if (Form1.FILE_TYPE == ImageFormat.Png) {
                filename += "png";
            } else if (Form1.FILE_TYPE == ImageFormat.Jpeg) {
                filename += "jpg";
            } else if (Form1.FILE_TYPE == ImageFormat.Gif) {
                filename += "gif";
            } else {
                // unreachable
            }

            return filename;
        }

        private static Random RAND = new Random();
        private static string random_string()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, Form1.DIGITS_OF_RAND)
                .Select(s => s[RAND.Next(s.Length)]).ToArray());
        }
    }
}
