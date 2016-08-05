using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Models.Alipay
{
    // ******************************************************
    // 文件名（FileName）:               AlipayCallBackRefundResult.cs  
    // 功能描述（Description）:          此文件用于支付宝退款时的回调结果实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    public class AlipayCallBackRefundResult 
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

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
