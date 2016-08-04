using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Xml;

namespace TestPay.Banks.Wechat
{
    // ******************************************************
    // 文件名（FileName）:               JSAPIConfig.cs  
    // 功能描述（Description）:          此文件用于微信获取重要参数方法。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class JSAPISumbit
    {
        public static string redirect_uri = string.Empty;//获取code后的跳转地址
        public static string notify_url = string.Empty;//支付成功后,异步回调的地址

        #region 获取code的url

        /// <summary>
        /// 获取code的url
        /// </summary>
        /// <param name="redirect_uri">回调地址</param>
        /// <param name="appid">appid</param>
        /// <param name="state">商户自己配置的信息，用于原样返回</param>
        /// <returns>地址链接</returns>
        public static string GetCodeUrl(string redirect_uri, string appid, string state = "")
        {
            var url = System.Web.HttpUtility.UrlEncode(redirect_uri);

            return string.Format(string.Format(JSAPIConfig.Get_code_url, appid, url, JSAPIConfig.Response_type, JSAPIConfig.Scope, state, JSAPIConfig.Wechat_redirect));
        }

        #endregion

        #region 获取操作微信用户的id

        /// <summary>
        ///  获取操作微信用户的
        /// </summary>
        /// <param name="code">微信号code</param>
        /// <param name="publicAppid">公众号id</param>
        /// <param name="publicSecret">公众号密钥</param>
        public static string GetUserId(string code, string publicAppid, string publicSecret)
        {
            #region 获取配置参数

            string appid = publicAppid;
            string secret = publicSecret;
            string grant_type = JSAPIConfig.Grant_type;

            #endregion

            var strTwo = "";
            strTwo += "appid=" + appid + "&";
            strTwo += "secret=" + secret + "&";
            strTwo += "code=" + code + "&";
            strTwo += "grant_type=" + grant_type;
            var urlTwo = string.Format(JSAPIConfig.Get_user_opendId_url, appid, secret, code, grant_type);
            var resultTwo = AkHelper.HttpHelper.RequestGet(urlTwo);
            UserOpendID model = AkHelper.JsonHelper.ToObject<UserOpendID>(resultTwo);
            return model.openid;
        }

        #endregion

        #region 生成html

        /// <summary>
        /// 支付时生成html
        /// </summary>
        /// <param name="model">预支付参数信息</param>
        /// <returns>生成</returns>
        public static string Html(LaunchPay model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<!DOCTYPE html>\r\n");
            str.Append("<html>\r\n");
            str.Append("\r\n");
            str.Append("<head><meta charset=\"UTF-8\"></head>\r\n");
            str.Append("\r\n");
            str.Append("<body>\r\n");
            str.Append("\r\n");
            str.Append("<script>");
            str.Append("\r\n");
            str.Append(" function onBridgeReady() {");
            str.Append("\r\n");
            str.Append(" WeixinJSBridge.invoke(");
            str.Append("\r\n");
            str.Append("'getBrandWCPayRequest', {");
            str.Append("\r\n");
            str.AppendFormat("  \"appId\": \"{0}\",     //公众号名称，由商户传入  ", model.appId);
            str.Append("\r\n");
            str.AppendFormat(" \"timeStamp\": \"{0}\",         //时间戳，自1970年以来的秒数 ", model.timeStamp);
            str.Append("\r\n");
            str.AppendFormat(" \"nonceStr\": \"{0}\", //随机串 ", model.nonceStr);
            str.Append("\r\n");
            str.AppendFormat(" \"package\": \"{0}\",", model.package);
            str.Append("\r\n");
            str.AppendFormat("\"signType\":\"{0}\",         //微信签名方式： ", model.signType);
            str.Append("\r\n");
            str.AppendFormat(" \"paySign\": \"{0}\" //微信签名 ", model.paySign);
            str.Append("\r\n");
            str.Append("  },");
            str.Append("\r\n");
            str.Append("  function (res) {");
            str.Append("\r\n");
            str.Append("   if (res.err_msg == \"get_brand_wcpay_request:ok\") {");
            str.Append("\r\n");
            str.AppendFormat("window.location.href = \"{0}\";", model.HtmlPayUrl);
            str.Append("\r\n");
            str.Append(" }     // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 ");
            str.Append("\r\n");
            str.Append("   }   );");
            str.Append("\r\n");
            str.Append(" }");
            str.Append("\r\n");
            str.Append("  if (typeof WeixinJSBridge == \"undefined\") {");
            str.Append("\r\n");
            str.Append(" if (document.addEventListener) {");
            str.Append("\r\n");
            str.Append("  document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);");
            str.Append("\r\n");
            str.Append(" } else if (document.attachEvent) {");
            str.Append("\r\n");
            str.Append(" document.attachEvent('WeixinJSBridgeReady', onBridgeReady);");
            str.Append("\r\n");
            str.Append("document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);");
            str.Append("\r\n");
            str.Append(" }");
            str.Append("\r\n");
            str.Append("  } else {");
            str.Append("\r\n");
            str.Append(" onBridgeReady();");
            str.Append(" } ");
            str.Append("\r\n");
            str.Append("</script>");
            str.Append("\r\n");
            str.Append("</body>\r\n");
            str.Append("</html>");
            string content = str.ToString();
            return str.ToString();
        }

        #endregion

        #region xml操作

        #region 得到xml数据

        /// <summary>
        ///得到xml数据
        /// </summary>
        /// <param name="dicData">参数集合</param>
        /// <returns>xml数据</returns>
        public static string GetXmlData(Dictionary<string, string> dicData)
        {
            if (dicData.Count <= 0)
                return "";
            StringBuilder str = new StringBuilder(dicData.Count * 200);
            str.Append("<xml>");
            foreach (var item in dicData)
            {
                str.Append(string.Format("<{0}>{1}</{0}>", item.Key, item.Value));

            }
            str.Append("</xml>");

            return str.ToString();
        }

        #endregion

        #region xml解析(xml反序列化)

        /// <summary>
        ///  xml反序列化
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="xmlString">xml字符串</param>
        /// <returns>结果类</returns>
        public static T GetDataMode<T>(string xmlString) where T : class
        {
            T result;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (System.IO.StringReader stringReader = new System.IO.StringReader(xmlString))
                {
                    result = (T)((object)xmlSerializer.Deserialize(stringReader));
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("XmlDeserialize发生异常：xmlString:" + xmlString + "异常信息：" + ex.Message);
            }
            return result;
        }

        #endregion

        #endregion

        #region post发送xml

        /// <summary>
        /// post发送xml
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="strPost">post的数据</param>
        /// <returns>post发送xml返回的信息</returns>
        public static string PostXml(string url, string strPost)
        {
            string result = "";

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentType = "text/xml";//提交xml
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }

            return result;
        }

        #endregion

        #region 用户id
        // ******************************************************
        // 文件名（FileName）:               UserOpendID.cs  
        // 功能描述（Description）:          此文件特用于微信支付时，需要获取的用户ID。
        // 数据表（Tables）:                 UserOpendID
        // 作者（Author）:                   pzy
        // 日期（Create Date）:              2016-08-03
        // ******************************************************
        public class UserOpendID
        {
            public string openid { get; set; }

        }

        #endregion

        #region xml序列化

        /// <summary>
        /// 返回字典数据(键值对)
        /// </summary>
        /// <param name="xmlString">xml字符串</param>
        /// <param name="sp">字典(有键无值-->值从xml读取)</param>
        public static Dictionary<string, string> GetData(string xmlString, Dictionary<string, string> sp, string root = "")
        {

            XmlDocument dom = new XmlDocument();
            dom.LoadXml(xmlString);
            XmlNode rootNode;
            if (string.IsNullOrEmpty(root))
            {
                rootNode = dom.DocumentElement;
            }
            else
            {
                rootNode = dom.SelectSingleNode(root);
            }
            foreach (XmlNode item in rootNode.ChildNodes)
            {
                if (sp.Keys.Contains(item.Name))
                {
                    sp[item.Name] = item.InnerText;
                }
            }

            return sp;
        }

        #endregion
    }

    #region 返回用户xml支付信息

    // ******************************************************
    // 文件名（FileName）:               XmlUserPayInfo.cs  
    // 功能描述（Description）:          此文件特用于微信支付时，返回的xml参数信息。
    // 数据表（Tables）:                 XmlUserPayInfo
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    [XmlRoot("xml")]
    public class XmlUserPayInfo
    {
        [XmlElement("return_code")]
        public string return_code { get; set; }

        [XmlElement("return_msg")]
        public string return_msg { get; set; }

        [XmlElement("appid")]
        public string appid { get; set; }

        [XmlElement("mch_id")]
        public string mch_id { get; set; }

        [XmlElement("nonce_str")]
        public string nonce_str { get; set; }

        [XmlElement("sign")]
        public string sign { get; set; }

        [XmlElement("result_code")]
        public string result_code { get; set; }

        [XmlElement("prepay_id")]
        public string prepay_id { get; set; }

        [XmlElement("trade_type")]
        public string trade_type { get; set; }
    }

    #endregion

    #region 欲支付信息

    // ******************************************************
    // 文件名（FileName）:               LaunchPay.cs  
    // 功能描述（Description）:          此文件特用于微信支付时，提交给微信的参数信息。
    // 数据表（Tables）:                 LaunchPay
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class LaunchPay
    {
        /// <summary>
        /// 公众号id
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timeStamp { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonceStr { get; set; }

        /// <summary>
        /// 订单详情扩展字符串
        /// </summary>
        public string package { get; set; }

        /// <summary>
        /// 签名方式
        /// </summary>
        public string signType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string paySign { get; set; }

        /// <summary>
        /// 生成的页面代码路径
        /// </summary>
        public string HtmlPayUrl { get; set; }
    }
    #endregion
}
