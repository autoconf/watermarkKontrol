namespace watermarkKontrol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.metniAyirButon = new System.Windows.Forms.PictureBox();
            this.goruntuSecPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.metinRichTextBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.metniAyirButon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goruntuSecPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // metniAyirButon
            // 
            this.metniAyirButon.Image = ((System.Drawing.Image)(resources.GetObject("metniAyirButon.Image")));
            this.metniAyirButon.Location = new System.Drawing.Point(61, 311);
            this.metniAyirButon.Margin = new System.Windows.Forms.Padding(4);
            this.metniAyirButon.Name = "metniAyirButon";
            this.metniAyirButon.Size = new System.Drawing.Size(399, 125);
            this.metniAyirButon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.metniAyirButon.TabIndex = 4;
            this.metniAyirButon.TabStop = false;
            this.metniAyirButon.Click += new System.EventHandler(this.metniAyirButon_Click);
            // 
            // goruntuSecPictureBox
            // 
            this.goruntuSecPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("goruntuSecPictureBox.Image")));
            this.goruntuSecPictureBox.Location = new System.Drawing.Point(13, 13);
            this.goruntuSecPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.goruntuSecPictureBox.Name = "goruntuSecPictureBox";
            this.goruntuSecPictureBox.Size = new System.Drawing.Size(515, 290);
            this.goruntuSecPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.goruntuSecPictureBox.TabIndex = 3;
            this.goruntuSecPictureBox.TabStop = false;
            this.goruntuSecPictureBox.Click += new System.EventHandler(this.goruntuSecPictureBox_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(977, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 39);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 56;
            this.pictureBox2.TabStop = false;
            // 
            // metinRichTextBox
            // 
            this.metinRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metinRichTextBox.Location = new System.Drawing.Point(552, 32);
            this.metinRichTextBox.Name = "metinRichTextBox";
            this.metinRichTextBox.ReadOnly = true;
            this.metinRichTextBox.Size = new System.Drawing.Size(382, 390);
            this.metinRichTextBox.TabIndex = 55;
            this.metinRichTextBox.Text = "Metin burada gösterilecektir.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(536, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 423);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1028, 444);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.metinRichTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metniAyirButon);
            this.Controls.Add(this.goruntuSecPictureBox);
            this.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Su İzi (watermark) Kontrol";
            ((System.ComponentModel.ISupportInitialize)(this.metniAyirButon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goruntuSecPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox metniAyirButon;
        private System.Windows.Forms.PictureBox goruntuSecPictureBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox metinRichTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

