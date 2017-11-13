using System;
using System.Windows.Forms;

namespace TXDLL.Tools
{
    /// <summary>
    /// WindowsForm操作工具
    /// </summary>
    public class WinFormTools
    {
        /// <summary>
        /// 异步设置控件可用
        /// </summary>
        /// <param name="control"></param>
        /// <param name="enable"></param>
        public static void SetControlEnable_Asyn(Control control,bool enable)
        {
            control.BeginInvoke(new Action(() => { control.Enabled = enable; }));
        }

        /// <summary>
        /// 异步添加文本到控件
        /// </summary>
        /// <param name="control">要添加文本的控件</param>
        /// <param name="text">添加的文本</param>
        public static void AddTextToControl_Asyn(Control control, string text)
        {
            AddTextToControl_Asyn(control, text, "", "");
        }

        /// <summary>
        /// 异步添加文本到控件
        /// </summary>
        /// <param name="control">要添加文本的控件</param>
        /// <param name="text">添加的文本</param>
        /// <param name="head">文本头，将被添加在文本前</param>
        /// <param name="end">文本尾，将被添加在文本尾</param>
        public static void AddTextToControl_Asyn(Control control, string text, string head, string end)
        {
            if (control.Enabled == true)
            {
                if (control.GetType().GetProperty("Text") != null)
                {
                    control.BeginInvoke(new Action(() => { control.Text += head + text + end; }));
                }
            }
        }

        /// <summary>
        ///异步更新控件文本
        /// </summary>
        /// <param name="control">要更新文本的控件</param>
        /// <param name="text">添加的文本</param>
        public static void UpdateControlText_Asyn(Control control, string text)
        {
            UpdateControlText_Asyn(control, text, "", "");
        }

        /// <summary>
        ///异步更新控件文本
        /// </summary>
        /// <param name="control">要更新文本的控件</param>
        /// <param name="text">添加的文本</param>
        /// <param name="head">文本头，将被添加在文本前</param>
        /// <param name="end">文本尾，将被添加在文本尾</param>
        public static void UpdateControlText_Asyn(Control control, string text, string head, string end)
        {
            if (control.Enabled == true)
            {
                if (control.GetType().GetProperty("Text") != null)
                {
                    control.BeginInvoke(new Action(() => { control.Text = head + text + end; }));
                }
            }
        }
    }
}
