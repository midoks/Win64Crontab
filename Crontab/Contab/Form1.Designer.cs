namespace Crontab
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cron_start = new System.Windows.Forms.Button();
            this.cron_logs = new System.Windows.Forms.ListBox();
            this.notifyIcon_cron = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_cron = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxMin = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonText = new System.Windows.Forms.Button();
            this.contextMenuStrip_cron.SuspendLayout();
            this.SuspendLayout();
            // 
            // cron_start
            // 
            this.cron_start.Location = new System.Drawing.Point(12, 12);
            this.cron_start.Name = "cron_start";
            this.cron_start.Size = new System.Drawing.Size(78, 33);
            this.cron_start.TabIndex = 0;
            this.cron_start.Text = "启动";
            this.cron_start.UseVisualStyleBackColor = true;
            this.cron_start.Click += new System.EventHandler(this.cron_start_Click);
            // 
            // cron_logs
            // 
            this.cron_logs.FormattingEnabled = true;
            this.cron_logs.ItemHeight = 12;
            this.cron_logs.Location = new System.Drawing.Point(101, 12);
            this.cron_logs.Name = "cron_logs";
            this.cron_logs.Size = new System.Drawing.Size(353, 172);
            this.cron_logs.TabIndex = 1;
            // 
            // notifyIcon_cron
            // 
            this.notifyIcon_cron.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon_cron.ContextMenuStrip = this.contextMenuStrip_cron;
            this.notifyIcon_cron.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_cron.Icon")));
            this.notifyIcon_cron.Text = "Crontab";
            this.notifyIcon_cron.Visible = true;
            this.notifyIcon_cron.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_cron_MouseDoubleClick);
            // 
            // contextMenuStrip_cron
            // 
            this.contextMenuStrip_cron.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOpen,
            this.ToolStripMenuItemExit});
            this.contextMenuStrip_cron.Name = "contextMenuStrip_cron";
            this.contextMenuStrip_cron.Size = new System.Drawing.Size(125, 48);
            // 
            // ToolStripMenuItemOpen
            // 
            this.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            this.ToolStripMenuItemOpen.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemOpen.Text = "打开窗口";
            this.ToolStripMenuItemOpen.Click += new System.EventHandler(this.ToolStripMenuItemOpen_Click);
            // 
            // ToolStripMenuItemExit
            // 
            this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            this.ToolStripMenuItemExit.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemExit.Text = "退出程序";
            this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
            // 
            // checkBoxMin
            // 
            this.checkBoxMin.AutoSize = true;
            this.checkBoxMin.Location = new System.Drawing.Point(13, 52);
            this.checkBoxMin.Name = "checkBoxMin";
            this.checkBoxMin.Size = new System.Drawing.Size(84, 16);
            this.checkBoxMin.TabIndex = 2;
            this.checkBoxMin.Text = "开启秒监控";
            this.checkBoxMin.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 75);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 12);
            this.linkLabel1.TabIndex = 3;
            // 
            // buttonText
            // 
            this.buttonText.Enabled = false;
            this.buttonText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonText.Location = new System.Drawing.Point(12, 75);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(78, 72);
            this.buttonText.TabIndex = 5;
            this.buttonText.Text = "开启后，会在任务的分钟内执行60次。";
            this.buttonText.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 199);
            this.Controls.Add(this.buttonText);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.checkBoxMin);
            this.Controls.Add(this.cron_logs);
            this.Controls.Add(this.cron_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Crontab";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip_cron.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cron_start;
        private System.Windows.Forms.ListBox cron_logs;
        private System.Windows.Forms.NotifyIcon notifyIcon_cron;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_cron;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpen;
        private System.Windows.Forms.CheckBox checkBoxMin;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonText;
    }
}

