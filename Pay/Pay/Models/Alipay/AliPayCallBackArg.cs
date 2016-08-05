using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Pay.Models.Alipay
{
    // ******************************************************
    // 文件名（FileName）:               AliPayCallBackArg.cs  
    // 功能描述（Description）:          此文件用于支付宝支付时的回调参数实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    public class AliPayCallBackArg
    {
        public HttpRequestBase Request { get; set; }

        /// <summary>
        /// 商户Key
        /// </summary>
        public string AccountNo{ get; set; }

        /// <summary>
        /// 商户Key
        /// </summary>
        public string AccountKey { get; set; }
    }
}
