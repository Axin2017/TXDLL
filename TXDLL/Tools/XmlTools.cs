using System;
using System.Xml;

namespace TXDLL.Tools
{
    /// <summary>
    /// Xml工具
    /// </summary>
    public class XmlTools
    {
        /// <summary>
        /// 获得XmlDocument对象。
        /// </summary>
        /// <param name="fileName">文件名，必须包含后缀</param>
        /// <param name="customPath">文件自定义路径，为空默认为当前程序根目录下的ConfigXml文件夹中</param>
        /// <returns></returns>
        public static XmlDocument GetXm(string fileName,string customPath)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\ConfigXml\" + fileName;
            if (!string.IsNullOrEmpty(customPath))
            {
                filePath = customPath + "\\" + fileName;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            return xmlDoc;
        }
        /// <summary>
        /// 直接从特定位置加载xml文件
        /// </summary>
        /// <param name="filePath">完整路径,，必须包含后缀</param>
        /// <returns></returns>
        public static XmlDocument GetXml(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            return xmlDoc;
        }

        /// <summary>
        /// 从xml中查询某个值.
        /// </summary>
        /// <param name="xmldoc">xml文档对象</param>
        /// <param name="tagName">标签名字</param>
        /// <param name="conditionName">条件名字</param>
        /// <param name="conditionValue">条件的值</param>
        /// <param name="conditionIsAttr">条件是否是属性(不是的话就是tag的innertText)</param>
        /// <param name="result">要获得的值的名字</param>
        /// <param name="resultIsAttr">要获得的是否是属性</param>
        /// <returns></returns>
        public static string GetValueFromXml(XmlDocument xmldoc, string tagName, string conditionName, string conditionValue, bool conditionIsAttr, string result, bool resultIsAttr)
        {
            XmlNodeList tagList = xmldoc.GetElementsByTagName(tagName);
            if (tagList.Count > 0)
            {
                foreach (XmlElement tag in tagList)
                {
                    if (conditionIsAttr)
                    {
                        if (tag.GetAttribute(conditionName) == conditionValue)
                        {
                            if (resultIsAttr)
                            {
                                return tag.GetAttribute(result);
                            }
                            else
                            {
                                return tag.InnerText;
                            }
                        }
                    }
                    else
                    {
                        if (tag.InnerText == conditionValue)
                        {
                            if (resultIsAttr)
                            {
                                return tag.GetAttribute(result);
                            }
                            else
                            {
                                return tag.InnerText;
                            }
                        }
                    }
                }
            }
            return "";
        }
        public static string GetInnerTextByUniqueTagName(XmlDocument xmldoc, string tagName)
        {
            XmlNodeList tagList = xmldoc.GetElementsByTagName(tagName);
            if (tagList.Count > 0)
            {
                XmlElement tag = (XmlElement)tagList[0];
                return tag.InnerText;
            }else
            {
                return "";
            }
        }
    }
}
