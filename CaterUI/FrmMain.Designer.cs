namespace CaterUI
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuManagerInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMemberInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDishInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tcHallInfo = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::CaterUI.Properties.Resources.menuBg;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManagerInfo,
            this.menuMemberInfo,
            this.menuTableInfo,
            this.menuDishInfo,
            this.menuOrder,
            this.menuQuit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(676, 72);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuManagerInfo
            // 
            this.menuManagerInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuManagerInfo.Image = global::CaterUI.Properties.Resources.menuManager;
            this.menuManagerInfo.Name = "menuManagerInfo";
            this.menuManagerInfo.Size = new System.Drawing.Size(76, 68);
            this.menuManagerInfo.Text = "toolStripMenuItem1";
            this.menuManagerInfo.ToolTipText = "店员管理";
            this.menuManagerInfo.Click += new System.EventHandler(this.menuManagerInfo_Click);
            // 
            // menuMemberInfo
            // 
            this.menuMemberInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuMemberInfo.Image = global::CaterUI.Properties.Resources.menuMember;
            this.menuMemberInfo.Name = "menuMemberInfo";
            this.menuMemberInfo.Size = new System.Drawing.Size(76, 68);
            this.menuMemberInfo.Text = "toolStripMenuItem2";
            this.menuMemberInfo.ToolTipText = "会员管理";
            this.menuMemberInfo.Click += new System.EventHandler(this.menuMemberInfo_Click);
            // 
            // menuTableInfo
            // 
            this.menuTableInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuTableInfo.Image = global::CaterUI.Properties.Resources.menuTable;
            this.menuTableInfo.Name = "menuTableInfo";
            this.menuTableInfo.Size = new System.Drawing.Size(76, 68);
            this.menuTableInfo.Text = "toolStripMenuItem3";
            this.menuTableInfo.ToolTipText = "餐桌管理";
            this.menuTableInfo.Click += new System.EventHandler(this.menuTableInfo_Click);
            // 
            // menuDishInfo
            // 
            this.menuDishInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuDishInfo.Image = global::CaterUI.Properties.Resources.menuDish;
            this.menuDishInfo.Name = "menuDishInfo";
            this.menuDishInfo.Size = new System.Drawing.Size(76, 68);
            this.menuDishInfo.Text = "toolStripMenuItem4";
            this.menuDishInfo.ToolTipText = "菜单管理";
            this.menuDishInfo.Click += new System.EventHandler(this.menuDishInfo_Click);
            // 
            // menuOrder
            // 
            this.menuOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuOrder.Image = global::CaterUI.Properties.Resources.menuOrder;
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.Size = new System.Drawing.Size(76, 68);
            this.menuOrder.Text = "toolStripMenuItem5";
            this.menuOrder.ToolTipText = "结账付款";
            this.menuOrder.Click += new System.EventHandler(this.menuOrder_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuQuit.Image = global::CaterUI.Properties.Resources.menuQuit;
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(76, 68);
            this.menuQuit.Text = "toolStripMenuItem6";
            this.menuQuit.ToolTipText = "退出程序";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // tcHallInfo
            // 
            this.tcHallInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHallInfo.Location = new System.Drawing.Point(0, 72);
            this.tcHallInfo.Name = "tcHallInfo";
            this.tcHallInfo.SelectedIndex = 0;
            this.tcHallInfo.Size = new System.Drawing.Size(676, 284);
            this.tcHallInfo.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "desk1.png");
            this.imageList1.Images.SetKeyName(1, "desk2.png");
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 356);
            this.Controls.Add(this.tcHallInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "餐饮管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuManagerInfo;
        private System.Windows.Forms.ToolStripMenuItem menuMemberInfo;
        private System.Windows.Forms.ToolStripMenuItem menuTableInfo;
        private System.Windows.Forms.ToolStripMenuItem menuDishInfo;
        private System.Windows.Forms.ToolStripMenuItem menuOrder;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.TabControl tcHallInfo;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}