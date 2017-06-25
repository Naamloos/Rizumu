namespace RizumuMapConverter
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
            this.mapname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mp3name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bgfilename = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.creator = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mapdesc = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mapname
            // 
            this.mapname.Location = new System.Drawing.Point(12, 25);
            this.mapname.Name = "mapname";
            this.mapname.Size = new System.Drawing.Size(260, 20);
            this.mapname.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Map name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MP3 filename (with ext)";
            // 
            // mp3name
            // 
            this.mp3name.Location = new System.Drawing.Point(12, 68);
            this.mp3name.Name = "mp3name";
            this.mp3name.Size = new System.Drawing.Size(260, 20);
            this.mp3name.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Background filename (with ext)";
            // 
            // bgfilename
            // 
            this.bgfilename.Location = new System.Drawing.Point(12, 107);
            this.bgfilename.Name = "bgfilename";
            this.bgfilename.Size = new System.Drawing.Size(260, 20);
            this.bgfilename.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Map Creator";
            // 
            // creator
            // 
            this.creator.Location = new System.Drawing.Point(12, 146);
            this.creator.Name = "creator";
            this.creator.Size = new System.Drawing.Size(260, 20);
            this.creator.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Map description";
            // 
            // mapdesc
            // 
            this.mapdesc.Location = new System.Drawing.Point(12, 185);
            this.mapdesc.Multiline = true;
            this.mapdesc.Name = "mapdesc";
            this.mapdesc.Size = new System.Drawing.Size(260, 126);
            this.mapdesc.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Convert map";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 352);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mapdesc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.creator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bgfilename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mp3name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapname);
            this.Name = "Form1";
            this.Text = "Old map converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mapname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mp3name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox bgfilename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox creator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mapdesc;
        private System.Windows.Forms.Button button1;
    }
}

