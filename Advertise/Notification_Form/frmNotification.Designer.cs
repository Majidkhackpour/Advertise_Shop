namespace Advertise.Notification_Form
{
    partial class frmNotification
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotification));
            this.Styler = new System.Windows.Forms.Timer(this.components);
            this.ClosingTimer = new System.Windows.Forms.Timer(this.components);
            this.RightSide = new System.Windows.Forms.Panel();
            this.lblText = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.LeftSide = new System.Windows.Forms.Panel();
            this.RightSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // Styler
            // 
            this.Styler.Interval = 1;
            this.Styler.Tick += new System.EventHandler(this.Styler_Tick);
            // 
            // ClosingTimer
            // 
            this.ClosingTimer.Interval = 5000;
            this.ClosingTimer.Tick += new System.EventHandler(this.ClosingTimer_Tick);
            // 
            // RightSide
            // 
            this.RightSide.Controls.Add(this.lblText);
            this.RightSide.Controls.Add(this.Message);
            this.RightSide.Controls.Add(this.Title);
            this.RightSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightSide.Location = new System.Drawing.Point(72, 0);
            this.RightSide.Name = "RightSide";
            this.RightSide.Size = new System.Drawing.Size(358, 69);
            this.RightSide.TabIndex = 3;
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.Color.Transparent;
            this.lblText.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblText.ForeColor = System.Drawing.Color.Silver;
            this.lblText.Location = new System.Drawing.Point(0, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(358, 69);
            this.lblText.TabIndex = 31;
            this.lblText.Text = "عنوان";
            this.lblText.Click += new System.EventHandler(this.lblText_Click);
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Location = new System.Drawing.Point(6, 25);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(0, 20);
            this.Message.TabIndex = 1;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(6, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(0, 15);
            this.Title.TabIndex = 0;
            // 
            // LeftSide
            // 
            this.LeftSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftSide.Location = new System.Drawing.Point(0, 0);
            this.LeftSide.Name = "LeftSide";
            this.LeftSide.Size = new System.Drawing.Size(72, 69);
            this.LeftSide.TabIndex = 2;
            // 
            // frmNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(430, 69);
            this.Controls.Add(this.RightSide);
            this.Controls.Add(this.LeftSide);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNotification";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmNotification_Load);
            this.RightSide.ResumeLayout(false);
            this.RightSide.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Styler;
        private System.Windows.Forms.Timer ClosingTimer;
        private System.Windows.Forms.Panel RightSide;
        private System.Windows.Forms.Label Message;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Panel LeftSide;
        public System.Windows.Forms.Label lblText;
    }
}