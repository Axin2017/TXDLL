using System;
using System.IO;
using System.Net;
using System.Text;

namespace TXDLL.Network
{
    public class BaseWebClient
    {
        private TimeOutClient client;
        public  string Cookie;
        private TimeOutClient Instance
        {
            get
            {
                if (client == null)
                {
                    client = new TimeOutClient();
                }
                return client;
            }
        }


        public string HttpGetConnect(string url)
        {
            string content = string.Empty;
            try
            {
                Stream stream = Instance.OpenRead(url);
                StreamReader sre = new StreamReader(stream);
                string line = string.Empty;
                while ((line = sre.ReadLine()) != null)
                {
                    content += line;
                }
                return content;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public HttpWebResponse GetHttpPostResponse(string serverUrl, string postData)
        {
            var dataArray = Encoding.UTF8.GetBytes(postData);
            //创建请求  
            var request = (HttpWebRequest)HttpWebRequest.Create(serverUrl);
            request.Method = "POST";
            request.ContentLength = dataArray.Length;
            if (!string.IsNullOrEmpty(Cookie))
            {
                request.Headers.Add("Cookie", Cookie);
            }
            //设置上传服务的数据格式  
            request.ContentType = "application/x-www-form-urlencoded";
            //请求的身份验证信息为默认  
            request.Credentials = CredentialCache.DefaultCredentials;
            //请求超时时间  
            request.Timeout = 10000;
            //创建输入流  
            Stream dataStream;
            try
            {
                dataStream = request.GetRequestStream();
            }
            catch (Exception ex)
            {
                throw new Exception("连接服务器失败，详细信息:" + ex.Message);
            }
            //发送请求  
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            //读取返回消息  
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception("连接服务器失败，详细信息:" + ex.Message);
            }
            return response;
        }
        public string GetResponseText(HttpWebResponse response )
        {
            string res = string.Empty;
            try
            {
                var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                res = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("读取返回流失败:" + ex.Message);
            }
            return res;
        }
    }
}
