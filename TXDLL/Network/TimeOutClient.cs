using System;
using System.Net;
using TXDLL.Tools;

namespace TXDLL.Network
{
    class TimeOutClient : WebClient
    {
        private int _timeout;
        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public TimeOutClient()
        {
            if (!string.IsNullOrEmpty(AppConfigTools.GetAppSettingString("TimeOut")))
            {
                try
                {
                    this._timeout = int.Parse(AppConfigTools.GetAppSettingString("TimeOut"));
                }
                catch { }
            }
            else
            {
                this._timeout = 5000;
            }
        }


        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}
