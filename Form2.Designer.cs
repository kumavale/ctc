
namespace ctc
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.FileType = new System.Windows.Forms.GroupBox();
            this.type_gif = new System.Windows.Forms.RadioButton();
            this.type_jpg = new System.Windows.Forms.RadioButton();
            this.type_png = new System.Windows.Forms.RadioButton();
            this.type_bmp = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_location = new System.Windows.Forms.Button();
            this.location = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.filename_format = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.FileType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileType
            // 
            this.FileType.Controls.Add(this.type_gif);
            this.FileType.Controls.Add(this.type_jpg);
            this.FileType.Controls.Add(this.type_png);
            this.FileType.Controls.Add(this.type_bmp);
            this.FileType.Location = new System.Drawing.Point(12, 12);
            this.FileType.Name = "FileType";
            this.FileType.Size = new System.Drawing.Size(90, 124);
            this.FileType.TabIndex = 0;
            this.FileType.TabStop = false;
            this.FileType.Text = "Save as type";
            // 
            // type_gif
            // 
            this.type_gif.AutoSize = true;
            this.type_gif.Location = new System.Drawing.Point(6, 97);
            this.type_gif.Name = "type_gif";
            this.type_gif.Size = new System.Drawing.Size(39, 19);
            this.type_gif.TabIndex = 3;
            this.type_gif.Text = "gif";
            this.type_gif.UseVisualStyleBackColor = true;
            this.type_gif.Click += new System.EventHandler(this.apply_button_enable);
            // 
            // type_jpg
            // 
            this.type_jpg.AutoSize = true;
            this.type_jpg.Location = new System.Drawing.Point(6, 72);
            this.type_jpg.Name = "type_jpg";
            this.type_jpg.Size = new System.Drawing.Size(42, 19);
            this.type_jpg.TabIndex = 2;
            this.type_jpg.Text = "jpg";
            this.type_jpg.UseVisualStyleBackColor = true;
            this.type_jpg.Click += new System.EventHandler(this.apply_button_enable);
            // 
            // type_png
            // 
            this.type_png.AutoSize = true;
            this.type_png.Location = new System.Drawing.Point(6, 47);
            this.type_png.Name = "type_png";
            this.type_png.Size = new System.Drawing.Size(46, 19);
            this.type_png.TabIndex = 1;
            this.type_png.Text = "png";
            this.type_png.UseVisualStyleBackColor = true;
            this.type_png.Click += new System.EventHandler(this.apply_button_enable);
            // 
            // type_bmp
            // 
            this.type_bmp.AutoSize = true;
            this.type_bmp.Checked = true;
            this.type_bmp.Location = new System.Drawing.Point(6, 22);
            this.type_bmp.Name = "type_bmp";
            this.type_bmp.Size = new System.Drawing.Size(50, 19);
            this.type_bmp.TabIndex = 0;
            this.type_bmp.TabStop = true;
            this.type_bmp.Text = "bmp";
            this.type_bmp.UseVisualStyleBackColor = true;
            this.type_bmp.Click += new System.EventHandler(this.apply_button_enable);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_location);
            this.groupBox1.Controls.Add(this.location);
            this.groupBox1.Location = new System.Drawing.Point(109, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location";
            // 
            // button_location
            // 
            this.button_location.Location = new System.Drawing.Point(226, 21);
            this.button_location.Name = "button_location";
            this.button_location.Size = new System.Drawing.Size(24, 24);
            this.button_location.TabIndex = 1;
            this.button_location.Text = "...";
            this.button_location.UseVisualStyleBackColor = true;
            this.button_location.Click += new System.EventHandler(this.button_location_Click);
            // 
            // location
            // 
            this.location.Location = new System.Drawing.Point(6, 22);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(214, 23);
            this.location.TabIndex = 0;
            this.location.TextChanged += new System.EventHandler(this.apply_button_enable);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Controls.Add(this.filename_format);
            this.groupBox2.Location = new System.Drawing.Point(13, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 204);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filename";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(339, 145);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button11);
            this.tabPage1.Controls.Add(this.button10);
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.button8);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button6);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(331, 117);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Components";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(166, 91);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 7;
            this.button11.Text = "{msec}";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.component_button_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(166, 62);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 6;
            this.button10.Text = "{sec}";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.component_button_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(166, 33);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 5;
            this.button9.Text = "{min}";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.component_button_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(166, 4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 4;
            this.button8.Text = "{hour}";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.component_button_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(85, 62);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 3;
            this.button7.Text = "{Day}";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.component_button_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(85, 33);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "{Month}";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.component_button_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(85, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "{Year}";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.component_button_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 0;
            this.button6.Text = "{sequence}";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.component_button_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(331, 117);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Other";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // filename_format
            // 
            this.filename_format.Location = new System.Drawing.Point(7, 23);
            this.filename_format.Name = "filename_format";
            this.filename_format.Size = new System.Drawing.Size(339, 23);
            this.filename_format.TabIndex = 0;
            this.filename_format.Text = "{sequence}";
            this.filename_format.TextChanged += new System.EventHandler(this.apply_button_enable);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(290, 353);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Apply";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(209, 353);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 388);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FileType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "CTC - Settings";
            this.TopMost = true;
            this.FileType.ResumeLayout(false);
            this.FileType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FileType;
        private System.Windows.Forms.RadioButton type_gif;
        private System.Windows.Forms.RadioButton type_jpg;
        private System.Windows.Forms.RadioButton type_png;
        private System.Windows.Forms.RadioButton type_bmp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_location;
        private System.Windows.Forms.TextBox location;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox filename_format;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TabPage tabPage2;
    }
}