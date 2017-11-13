using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsTest
{
    public partial class WordForm : Form
    {
        public WordForm(string html)
        {
            InitializeComponent();
            int x = Screen.PrimaryScreen.WorkingArea.Size.Width - 680;
            int y = Screen.PrimaryScreen.WorkingArea.Size.Height - 262;
            Point p = new Point(x, y);
            this.PointToScreen(p);
            this.Location = p;
            webBrowser1.DocumentText = html;
        }
    }
}
