using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Pay.Models.Wechat
{
    // ******************************************************
    // 文件名（FileName）:               WechatCallBackArg.cs  
    // 功能描述（Description）:          此文件用于微信支付时的回调参数实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
   public class WechatCallBackArg
    {

        public HttpRequestBase Request { get; set; }

        /// <summary>
        /// 商户Key
        /// </summary>
        public string AccountKey { get; set; }

        /// <summary>
        /// 公众号(只用于微信)
        /// </summary>
        public string PublicNo;

        /// <summary>
        /// 公众号密钥(只用于微信)
        /// </summary>
        public string PublicKey;
    }
}
