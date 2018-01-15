using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXDLL.Winform.Model;

namespace TXDLL.Winform.Control
{
    public partial class TXTrayForm : Form
    {

        public TXTrayForm()
        {
            InitializeComponent();
            TXTrayAttribute attr = new TXTrayAttribute();
            attr.BalloonTipText = "TxSoft";
            attr.BalloonTipTitle = "TxSoft";
            attr.Text = "TxSoft is running";
            ResourceManager rm = new ResourceManager("TXDLL.Winform.Control.TXTrayForm", Assembly.GetExecutingAssembly());
            this.Icon = (Icon)rm.GetObject("$this.Icon");
            attr.Icon = (Icon)rm.GetObject("TXNotifyIcon.Icon");
            SetTrayAttribute(attr);

        }
        protected  void SetTrayAttribute(TXTrayAttribute attr)
        {
            this.TXNotifyIcon.BalloonTipText = attr.BalloonTipText;
            this.TXNotifyIcon.BalloonTipTitle = attr.BalloonTipTitle;
            this.TXNotifyIcon.Text = attr.Text;
            this.TXNotifyIcon.Icon = attr.Icon;
        }
        #region 托盘相关
        private void TXNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized || Visible == false)
            {
                TXNormalForm();
            }
            else
            {
                TXMinForm();
            }
        }
        private void TXNormalForm()
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        }

        private void TXMinForm()
        {
            WindowState = FormWindowState.Minimized;
            Visible = false;
            TXNotifyIcon.Visible = true;
            TXNotifyIcon.ShowBalloonTip(10);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                Visible = false;
                return;
            }
            base.WndProc(ref m);
        }

        private void TXShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TXNormalForm();
        }

        private void TXExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TXNotifyIcon.Visible = false;
            Application.ExitThread();
        }
        #endregion

        
    }
}
