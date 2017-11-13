using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using TXDLL.Winform.Control;

namespace WindowsFormsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TXDLL.Tools.ThreadTools.StartVoidAsynThread(this, "staTest", null);
        }

        private void priTest(string name,string sex)
        {
            TXDLL.Tools.WinFormTools.UpdateControlText_Asyn(this.label1, name+sex, "", "");
        }

        public void pubTest(string name, string sex)
        {
            TXDLL.Tools.WinFormTools.UpdateControlText_Asyn(this.label1, name + sex, "", "");
        }

        public static void staTest()
        {
            try
            {
                MessageBox.Show(int.Parse("s").ToString());
            }catch
            {
                MessageBox.Show("Error");
            }
        }
        private int intTest(int m)
        {
            return m * m;
        }
    }
}
