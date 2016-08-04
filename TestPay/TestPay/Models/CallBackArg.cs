using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TestPay.Models
{
    // ******************************************************
    // 文件名（FileName）:               CallBackArg.cs  
    // 功能描述（Description）:          此文件用于支付时的回调参数。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class CallBackArg
    {
        public HttpRequestBase Request { get; set; }

        /// <summary>
        /// 渠道编号
        /// </summary>
        public string ChannelNo { get; set; }

        /// <summary>
        /// 商户Key
        /// </summary>
        public string AccountNo{ get; set; }

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

        #region 用于财付通回调,其他请根据文档传值

        /// <summary>
        /// 标识是同步或者异步通知（用于财付通回调,其他请根据文档传值）
        /// </summary>
        public string SynAsynKey { get; set; }


        /// <summary>
        /// 商户号（用于财付通回调,其他请根据文档传值）
        /// </summary>
        public string Bargainor_id { get; set; } 

        #endregion
    }
}
