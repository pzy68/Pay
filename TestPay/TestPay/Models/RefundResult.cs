using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPay.Models
{
    // ******************************************************
    // 文件名（FileName）:               RefundResult.cs  
    // 功能描述（Description）:          此文件用于退款成功时返回的结果。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class RefundResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string ChannelCode { get; set; }

        /// <summary>
        /// 欧克代码
        /// </summary>
        public int OukCode { get; set; }

        /// <summary>
        /// 生成的Html代码
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Charset { get; set; }

        /// <summary>
        /// 支付记录ID
        /// </summary>
        public long PayRecordID { get; set; }

        /// <summary>
        /// 支付记录单号
        /// </summary>
        public string PayNumber { get; set; }
    }
}
