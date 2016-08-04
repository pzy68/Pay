using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TestPay.Models
{
    // ******************************************************
    // 文件名（FileName）:               CallBackRefundResult.cs  
    // 功能描述（Description）:          此文件用于退款时的回调参数。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class CallBacRefundkArg
    {
        public HttpRequestBase Request { get; set; }

        /// <summary>
        /// 渠道编号
        /// </summary>
        public string ChannelNo { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public string AccountNo{ get; set; }

        /// <summary>
        /// 商户Key
        /// </summary>
        public string AccountKey { get; set; }

        /// <summary>
        /// 支付机构在订单付款成功时给的单号(用于对账跟踪)
        /// </summary>
        public string TraceNo { get; set; }
    }
}
