using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TXDLL.Winform.Model;

namespace WindowsFormsTest
{
    public partial class ExtendsTestForm : TXDLL.Winform.Control.TXTrayForm
    {
        public ExtendsTestForm()
        {
            InitializeComponent();
        }

        private void ExtendsTestForm_Load(object sender, EventArgs e)
        {
            TXTrayAttribute tx = new TXTrayAttribute();
            tx.BalloonTipText = "xx";
            tx.BalloonTipTitle = "xx";
            tx.Text = "xx";
            tx.Icon = new System.Drawing.Icon(@"D:\vsproject\公司项目\大亚湾\规划公示\GHGS_V2\GHGS_V2\bin\Debug\update.ico");
            this.SetTrayAttribute(tx);
        }
    }
}
