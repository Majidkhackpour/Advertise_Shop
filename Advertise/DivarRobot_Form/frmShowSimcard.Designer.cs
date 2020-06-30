namespace Advertise.DivarRobot_Form
{
    partial class frmShowSimcard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowSimcard));
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsAdv = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSimcard = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DGrid = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Radif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ownerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasDivarTokenDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.hasDivarChatTokenDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.divarTokenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chatTokenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advCat1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advCat2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advCat3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chatCat1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chatCat2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chatCat3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SimcardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAdv = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChat = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSimcard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimcardBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuView
            // 
            this.mnuView.ForeColor = System.Drawing.Color.Silver;
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(195, 24);
            this.mnuView.Text = "مشاهده";
            this.mnuView.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(192, 6);
            // 
            // mnuDelete
            // 
            this.mnuDelete.ForeColor = System.Drawing.Color.Silver;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(195, 24);
            this.mnuDelete.Text = "حذف سیمکارت جاری";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.ForeColor = System.Drawing.Color.Silver;
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(195, 24);
            this.mnuEdit.Text = "ویرایش سیمکارت جاری";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuInsAdv
            // 
            this.mnuInsAdv.ForeColor = System.Drawing.Color.Silver;
            this.mnuInsAdv.Name = "mnuInsAdv";
            this.mnuInsAdv.Size = new System.Drawing.Size(195, 24);
            this.mnuInsAdv.Text = "درج سیمکارت جدید";
            this.mnuInsAdv.Click += new System.EventHandler(this.mnuInsAdv_Click);
            // 
            // cmSimcard
            // 
            this.cmSimcard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.cmSimcard.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmSimcard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsAdv,
            this.mnuEdit,
            this.mnuDelete,
            this.toolStripMenuItem5,
            this.mnuView,
            this.toolStripMenuItem1,
            this.mnuAdv,
            this.mnuChat});
            this.cmSimcard.Name = "contextMenuStrip1";
            this.cmSimcard.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmSimcard.Size = new System.Drawing.Size(196, 182);
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.DGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGrid.AutoGenerateColumns = false;
            this.DGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Radif,
            this.ownerNameDataGridViewTextBoxColumn,
            this.dgNumber,
            this.hasDivarTokenDataGridViewCheckBoxColumn,
            this.hasDivarChatTokenDataGridViewCheckBoxColumn,
            this.dgGuid,
            this.modifiedDataGridViewTextBoxColumn,
            this.divarTokenDataGridViewTextBoxColumn,
            this.chatTokenDataGridViewTextBoxColumn,
            this.advCat1DataGridViewTextBoxColumn,
            this.advCat2DataGridViewTextBoxColumn,
            this.advCat3DataGridViewTextBoxColumn,
            this.chatCat1DataGridViewTextBoxColumn,
            this.chatCat2DataGridViewTextBoxColumn,
            this.chatCat3DataGridViewTextBoxColumn});
            this.DGrid.ContextMenuStrip = this.cmSimcard;
            this.DGrid.DataSource = this.SimcardBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.DGrid.Location = new System.Drawing.Point(3, 44);
            this.DGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DGrid.Name = "DGrid";
            this.DGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(781, 507);
            this.DGrid.TabIndex = 55704;
            this.DGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGrid_CellFormatting);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.Location = new System.Drawing.Point(42, 9);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PreventEnterBeep = true;
            this.txtSearch.Size = new System.Drawing.Size(701, 27);
            this.txtSearch.TabIndex = 55705;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.WatermarkText = "مورد جستجو را وارد نمایید ...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // Radif
            // 
            this.Radif.HeaderText = "ردیف";
            this.Radif.Name = "Radif";
            this.Radif.ReadOnly = true;
            this.Radif.Width = 50;
            // 
            // ownerNameDataGridViewTextBoxColumn
            // 
            this.ownerNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ownerNameDataGridViewTextBoxColumn.DataPropertyName = "OwnerName";
            this.ownerNameDataGridViewTextBoxColumn.HeaderText = "مالک";
            this.ownerNameDataGridViewTextBoxColumn.Name = "ownerNameDataGridViewTextBoxColumn";
            // 
            // dgNumber
            // 
            this.dgNumber.DataPropertyName = "Number";
            this.dgNumber.HeaderText = "شماره";
            this.dgNumber.Name = "dgNumber";
            // 
            // hasDivarTokenDataGridViewCheckBoxColumn
            // 
            this.hasDivarTokenDataGridViewCheckBoxColumn.DataPropertyName = "HasDivarToken";
            this.hasDivarTokenDataGridViewCheckBoxColumn.HeaderText = "توکن دیوار";
            this.hasDivarTokenDataGridViewCheckBoxColumn.Name = "hasDivarTokenDataGridViewCheckBoxColumn";
            this.hasDivarTokenDataGridViewCheckBoxColumn.ReadOnly = true;
            this.hasDivarTokenDataGridViewCheckBoxColumn.Width = 70;
            // 
            // hasDivarChatTokenDataGridViewCheckBoxColumn
            // 
            this.hasDivarChatTokenDataGridViewCheckBoxColumn.DataPropertyName = "HasDivarChatToken";
            this.hasDivarChatTokenDataGridViewCheckBoxColumn.HeaderText = "توکن چت دیوار";
            this.hasDivarChatTokenDataGridViewCheckBoxColumn.Name = "hasDivarChatTokenDataGridViewCheckBoxColumn";
            this.hasDivarChatTokenDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // dgGuid
            // 
            this.dgGuid.DataPropertyName = "Guid";
            this.dgGuid.HeaderText = "Guid";
            this.dgGuid.Name = "dgGuid";
            this.dgGuid.Visible = false;
            // 
            // modifiedDataGridViewTextBoxColumn
            // 
            this.modifiedDataGridViewTextBoxColumn.DataPropertyName = "Modified";
            this.modifiedDataGridViewTextBoxColumn.HeaderText = "Modified";
            this.modifiedDataGridViewTextBoxColumn.Name = "modifiedDataGridViewTextBoxColumn";
            this.modifiedDataGridViewTextBoxColumn.Visible = false;
            // 
            // divarTokenDataGridViewTextBoxColumn
            // 
            this.divarTokenDataGridViewTextBoxColumn.DataPropertyName = "DivarToken";
            this.divarTokenDataGridViewTextBoxColumn.HeaderText = "DivarToken";
            this.divarTokenDataGridViewTextBoxColumn.Name = "divarTokenDataGridViewTextBoxColumn";
            this.divarTokenDataGridViewTextBoxColumn.Visible = false;
            // 
            // chatTokenDataGridViewTextBoxColumn
            // 
            this.chatTokenDataGridViewTextBoxColumn.DataPropertyName = "ChatToken";
            this.chatTokenDataGridViewTextBoxColumn.HeaderText = "ChatToken";
            this.chatTokenDataGridViewTextBoxColumn.Name = "chatTokenDataGridViewTextBoxColumn";
            this.chatTokenDataGridViewTextBoxColumn.Visible = false;
            // 
            // advCat1DataGridViewTextBoxColumn
            // 
            this.advCat1DataGridViewTextBoxColumn.DataPropertyName = "AdvCat1";
            this.advCat1DataGridViewTextBoxColumn.HeaderText = "AdvCat1";
            this.advCat1DataGridViewTextBoxColumn.Name = "advCat1DataGridViewTextBoxColumn";
            this.advCat1DataGridViewTextBoxColumn.Visible = false;
            // 
            // advCat2DataGridViewTextBoxColumn
            // 
            this.advCat2DataGridViewTextBoxColumn.DataPropertyName = "AdvCat2";
            this.advCat2DataGridViewTextBoxColumn.HeaderText = "AdvCat2";
            this.advCat2DataGridViewTextBoxColumn.Name = "advCat2DataGridViewTextBoxColumn";
            this.advCat2DataGridViewTextBoxColumn.Visible = false;
            // 
            // advCat3DataGridViewTextBoxColumn
            // 
            this.advCat3DataGridViewTextBoxColumn.DataPropertyName = "AdvCat3";
            this.advCat3DataGridViewTextBoxColumn.HeaderText = "AdvCat3";
            this.advCat3DataGridViewTextBoxColumn.Name = "advCat3DataGridViewTextBoxColumn";
            this.advCat3DataGridViewTextBoxColumn.Visible = false;
            // 
            // chatCat1DataGridViewTextBoxColumn
            // 
            this.chatCat1DataGridViewTextBoxColumn.DataPropertyName = "ChatCat1";
            this.chatCat1DataGridViewTextBoxColumn.HeaderText = "ChatCat1";
            this.chatCat1DataGridViewTextBoxColumn.Name = "chatCat1DataGridViewTextBoxColumn";
            this.chatCat1DataGridViewTextBoxColumn.Visible = false;
            // 
            // chatCat2DataGridViewTextBoxColumn
            // 
            this.chatCat2DataGridViewTextBoxColumn.DataPropertyName = "ChatCat2";
            this.chatCat2DataGridViewTextBoxColumn.HeaderText = "ChatCat2";
            this.chatCat2DataGridViewTextBoxColumn.Name = "chatCat2DataGridViewTextBoxColumn";
            this.chatCat2DataGridViewTextBoxColumn.Visible = false;
            // 
            // chatCat3DataGridViewTextBoxColumn
            // 
            this.chatCat3DataGridViewTextBoxColumn.DataPropertyName = "ChatCat3";
            this.chatCat3DataGridViewTextBoxColumn.HeaderText = "ChatCat3";
            this.chatCat3DataGridViewTextBoxColumn.Name = "chatCat3DataGridViewTextBoxColumn";
            this.chatCat3DataGridViewTextBoxColumn.Visible = false;
            // 
            // SimcardBindingSource
            // 
            this.SimcardBindingSource.DataSource = typeof(EntityCache.Bussines.SimcardBussines);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
            // 
            // mnuAdv
            // 
            this.mnuAdv.ForeColor = System.Drawing.Color.Silver;
            this.mnuAdv.Name = "mnuAdv";
            this.mnuAdv.Size = new System.Drawing.Size(195, 24);
            this.mnuAdv.Text = "دریافت توکن آگهی دیوار";
            this.mnuAdv.Click += new System.EventHandler(this.mnuAdv_Click);
            // 
            // mnuChat
            // 
            this.mnuChat.ForeColor = System.Drawing.Color.Silver;
            this.mnuChat.Name = "mnuChat";
            this.mnuChat.Size = new System.Drawing.Size(195, 24);
            this.mnuChat.Text = "دریافت توکن چت دیوار";
            this.mnuChat.Click += new System.EventHandler(this.mnuChat_Click);
            // 
            // frmShowSimcard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.DGrid);
            this.Controls.Add(this.txtSearch);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowSimcard";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmShowSimcard_Load);
            this.cmSimcard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimcardBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource SimcardBindingSource;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuInsAdv;
        private System.Windows.Forms.ContextMenuStrip cmSimcard;
        private DevComponents.DotNetBar.Controls.DataGridViewX DGrid;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Radif;
        private System.Windows.Forms.DataGridViewTextBoxColumn ownerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasDivarTokenDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasDivarChatTokenDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn divarTokenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chatTokenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn advCat1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn advCat2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn advCat3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chatCat1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chatCat2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chatCat3DataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuAdv;
        private System.Windows.Forms.ToolStripMenuItem mnuChat;
    }
}