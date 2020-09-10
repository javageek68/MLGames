namespace TestPlatform.Games
{
    partial class NetworkDesigner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dvGrid = new System.Windows.Forms.DataGridView();
            this.LayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMoveLayerUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMoveLayerDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dvGrid)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            this.dvGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dvGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dvGrid.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LayerName,
            this.Size,
            this.Activation});
            this.dvGrid.ContextMenuStrip = this.cmsMenu;
            this.dvGrid.GridColor = System.Drawing.Color.Gray;
            this.dvGrid.Location = new System.Drawing.Point(8, 12);
            this.dvGrid.MultiSelect = false;
            this.dvGrid.Name = "dvGrid";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.dvGrid.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dvGrid.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.dvGrid.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dvGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DimGray;
            this.dvGrid.Size = new System.Drawing.Size(485, 221);
            this.dvGrid.TabIndex = 3;
            // 
            // LayerName
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            this.LayerName.DefaultCellStyle = dataGridViewCellStyle3;
            this.LayerName.HeaderText = "Layer Name";
            this.LayerName.Name = "LayerName";
            this.LayerName.ReadOnly = true;
            // 
            // Size
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver;
            this.Size.DefaultCellStyle = dataGridViewCellStyle4;
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            // 
            // Activation
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            this.Activation.DefaultCellStyle = dataGridViewCellStyle5;
            this.Activation.HeaderText = "Activation";
            this.Activation.Name = "Activation";
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMoveLayerUpToolStripMenuItem,
            this.mnuMoveLayerDownToolStripMenuItem,
            this.mnuDeleteLayerToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(170, 70);
            // 
            // mnuMoveLayerUpToolStripMenuItem
            // 
            this.mnuMoveLayerUpToolStripMenuItem.Name = "mnuMoveLayerUpToolStripMenuItem";
            this.mnuMoveLayerUpToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.mnuMoveLayerUpToolStripMenuItem.Text = "Move Layer Up";
            this.mnuMoveLayerUpToolStripMenuItem.Click += new System.EventHandler(this.mnuMoveLayerUpToolStripMenuItem_Click);
            // 
            // mnuMoveLayerDownToolStripMenuItem
            // 
            this.mnuMoveLayerDownToolStripMenuItem.Name = "mnuMoveLayerDownToolStripMenuItem";
            this.mnuMoveLayerDownToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.mnuMoveLayerDownToolStripMenuItem.Text = "Move Layer Down";
            this.mnuMoveLayerDownToolStripMenuItem.Click += new System.EventHandler(this.mnuMoveLayerDownToolStripMenuItem_Click);
            // 
            // mnuDeleteLayerToolStripMenuItem
            // 
            this.mnuDeleteLayerToolStripMenuItem.Name = "mnuDeleteLayerToolStripMenuItem";
            this.mnuDeleteLayerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.mnuDeleteLayerToolStripMenuItem.Text = "Delete Layer";
            this.mnuDeleteLayerToolStripMenuItem.Click += new System.EventHandler(this.mnuDeleteLayerToolStripMenuItem_Click);
            // 
            // NetworkDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(519, 259);
            this.Controls.Add(this.dvGrid);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "NetworkDesigner";
            this.Text = "Network Designer";
            ((System.ComponentModel.ISupportInitialize)(this.dvGrid)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dvGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn LayerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewComboBoxColumn Activation;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuMoveLayerUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuMoveLayerDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteLayerToolStripMenuItem;
    }
}