using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPay.Models
{
    // ******************************************************
    // 文件名（FileName）:               CallBackRefundResult.cs  
    // 功能描述（Description）:          此文件用于退款时的回调结果。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class CallBackRefundResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 退款交易金额
        /// </summary>
        public decimal TraceAmount { get; set; }

        /// <summary>
        /// 实际退款金额
        /// </summary>
        public decimal ReailTraceAmount { get; set; }

        /// <summary>
        /// 退款批次号
        /// </summary>
        public string Batch_No { get; set; }

        /// <summary>
        /// 成功后退款笔数
        /// </summary>
        public int Success_Number { get; set; }

        /// <summary>
        /// 交易号(用于对账)
        /// </summary>     
        public string TraceNo { get; set; }

        /// <summary>
        /// 退款账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 退款账户iD
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        public string Trade_status { get; set; }

        /// <summary>
        /// 退款结果
        /// </summary>
        public string result_details { get; set; }
    }
}
