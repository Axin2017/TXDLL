using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXDLL.Winform.Model
{
    public class TXTrayAttribute
    {
        /// <summary>
        /// 气泡显示的文字
        /// </summary>
        public string BalloonTipText { get; set; }
        /// <summary>
        /// 气泡显示的标题
        /// </summary>
        public string BalloonTipTitle { get; set; }
        /// <summary>
        /// 鼠标移动上去显示的文字
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 托盘程序图标
        /// </summary>
        public Icon Icon { get; set; }
    }
}
