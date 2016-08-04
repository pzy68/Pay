using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPay.Models
{
    // ******************************************************
    // 文件名（FileName）:               CallBackRefundResult.cs  
    // 功能描述（Description）:          此文件用于支付时的回调结果。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class CallBackResult
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Obsolete("有歧义，请使用PayNumber")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///  0：失败 1：成功 2：重复通知 
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 渠道代码
        /// </summary>
        public string ChannelCode { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TraceAmount { get; set; }

        /// <summary>
        /// 支付接口唯一标识
        /// </summary>
        public string TraceId { get; set; }

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
        /// 报告接收成功的代码
        /// </summary>
        public string ReportCode { get; set; }

        /// <summary>
        /// 支付记录ID
        /// </summary>
        public long PayRecordID { get; set; }

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
