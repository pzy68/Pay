using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Models.Wechat
{
    // ******************************************************
    // 文件名（FileName）:               WechatCallBackResult.cs  
    // 功能描述（Description）:          此文件用于微信支付时的回调结果实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
   public class WechatCallBackResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 系统跟踪号(用于对账)(支付机构的流水号) 
        /// </summary>     
        public string TraceNo { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 支付记录单号(商户网站唯一订单号)
        /// </summary>
        public string PayNumber { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string AttachInfo { get; set; }
    }
}
