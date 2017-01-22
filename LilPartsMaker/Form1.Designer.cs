namespace LilPartsMaker
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.browseButton1 = new System.Windows.Forms.Button();
            this.tb_csvFile = new System.Windows.Forms.TextBox();
            this.tb_saveLocation = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.FlatAppearance.BorderSize = 5;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(32, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(316, 60);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.generateButtonPress);
            // 
            // browseButton1
            // 
            this.browseButton1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.browseButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseButton1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton1.Location = new System.Drawing.Point(288, 42);
            this.browseButton1.Name = "browseButton1";
            this.browseButton1.Size = new System.Drawing.Size(75, 20);
            this.browseButton1.TabIndex = 1;
            this.browseButton1.Text = "Browse";
            this.browseButton1.UseVisualStyleBackColor = false;
            this.browseButton1.Click += new System.EventHandler(this.browseCSV);
            // 
            // tb_csvFile
            // 
            this.tb_csvFile.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tb_csvFile.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tb_csvFile.Location = new System.Drawing.Point(12, 42);
            this.tb_csvFile.Name = "tb_csvFile";
            this.tb_csvFile.Size = new System.Drawing.Size(270, 20);
            this.tb_csvFile.TabIndex = 2;
            this.tb_csvFile.TextChanged += new System.EventHandler(this.tb_csvFile_TextChanged);
            // 
            // tb_saveLocation
            // 
            this.tb_saveLocation.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tb_saveLocation.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tb_saveLocation.Location = new System.Drawing.Point(12, 154);
            this.tb_saveLocation.Name = "tb_saveLocation";
            this.tb_saveLocation.Size = new System.Drawing.Size(270, 20);
            this.tb_saveLocation.TabIndex = 4;
            this.tb_saveLocation.TextChanged += new System.EventHandler(this.tb_saveLocation_TextChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(288, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 20);
            this.button2.TabIndex = 3;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.browseSaveLocation);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "CSV File Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.label2.Location = new System.Drawing.Point(12, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Save Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Xls File Location";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Location = new System.Drawing.Point(12, 96);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.onXLSTextChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(288, 96);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 20);
            this.button3.TabIndex = 7;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.browseXLS);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(384, 304);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_saveLocation);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tb_csvFile);
            this.Controls.Add(this.browseButton1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "LilParts Maker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button browseButton1;
        private System.Windows.Forms.TextBox tb_csvFile;
        private System.Windows.Forms.TextBox tb_saveLocation;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
    }
}

