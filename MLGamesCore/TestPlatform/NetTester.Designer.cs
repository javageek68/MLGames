namespace TestPlatform
{
    partial class NetTester
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
            this.btnStart = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnTTTMove = new System.Windows.Forms.Button();
            this.txtMove = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Black;
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.Location = new System.Drawing.Point(18, 24);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(102, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BackColor = System.Drawing.Color.Black;
            this.txtStatus.ForeColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(20, 75);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(763, 363);
            this.txtStatus.TabIndex = 1;
            // 
            // btnTTTMove
            // 
            this.btnTTTMove.BackColor = System.Drawing.Color.Black;
            this.btnTTTMove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTTTMove.Location = new System.Drawing.Point(161, 24);
            this.btnTTTMove.Name = "btnTTTMove";
            this.btnTTTMove.Size = new System.Drawing.Size(102, 28);
            this.btnTTTMove.TabIndex = 2;
            this.btnTTTMove.Text = "TTT Move";
            this.btnTTTMove.UseVisualStyleBackColor = false;
            this.btnTTTMove.Click += new System.EventHandler(this.btnTTTMove_Click);
            // 
            // txtMove
            // 
            this.txtMove.BackColor = System.Drawing.Color.Black;
            this.txtMove.ForeColor = System.Drawing.Color.White;
            this.txtMove.Location = new System.Drawing.Point(314, 29);
            this.txtMove.Name = "txtMove";
            this.txtMove.Size = new System.Drawing.Size(67, 20);
            this.txtMove.TabIndex = 3;
            // 
            // NetTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMove);
            this.Controls.Add(this.btnTTTMove);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnStart);
            this.Name = "NetTester";
            this.Text = "NetTester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnTTTMove;
        private System.Windows.Forms.TextBox txtMove;
    }
}