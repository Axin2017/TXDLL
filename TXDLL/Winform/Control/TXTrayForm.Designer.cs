namespace TXDLL.Winform.Control
{
    partial class TXTrayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TXTrayForm));
            this.TXNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TXContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TXShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TXExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TXContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TXNotifyIcon
            // 
            this.TXNotifyIcon.ContextMenuStrip = this.TXContextMenuStrip;
            this.TXNotifyIcon.Text = "notifyIcon1";
            this.TXNotifyIcon.Visible = true;
            this.TXNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TXNotifyIcon_MouseDoubleClick);
            // 
            // TXContextMenuStrip
            // 
            this.TXContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TXShowToolStripMenuItem,
            this.TXExitToolStripMenuItem});
            this.TXContextMenuStrip.Name = "TXContextMenuStrip";
            this.TXContextMenuStrip.Size = new System.Drawing.Size(101, 48);
            // 
            // TXShowToolStripMenuItem
            // 
            this.TXShowToolStripMenuItem.Name = "TXShowToolStripMenuItem";
            this.TXShowToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.TXShowToolStripMenuItem.Text = "还原";
            this.TXShowToolStripMenuItem.Click += new System.EventHandler(this.TXShowToolStripMenuItem_Click);
            // 
            // TXExitToolStripMenuItem
            // 
            this.TXExitToolStripMenuItem.Name = "TXExitToolStripMenuItem";
            this.TXExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.TXExitToolStripMenuItem.Text = "退出";
            this.TXExitToolStripMenuItem.Click += new System.EventHandler(this.TXExitToolStripMenuItem_Click);
            // 
            // TXTrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "TXTrayForm";
            this.Text = "TXTrayForm";
            this.TXContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.NotifyIcon TXNotifyIcon;
        protected System.Windows.Forms.ContextMenuStrip TXContextMenuStrip;
        protected System.Windows.Forms.ToolStripMenuItem TXShowToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem TXExitToolStripMenuItem;
    }
}