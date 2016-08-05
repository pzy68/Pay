using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Models.Alipay
{
    // ******************************************************
    // 文件名（FileName）:               AlipayCreditResult.cs  
    // 功能描述（Description）:          此文件用于支付宝退款时的参数实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    public class AlipayRefundArg 
    {
        /// <summary>
        /// 渠道编号(指使用哪个支付机构)
        /// </summary>
        public string ChannelNo;

        /// <summary>
        ///交易成功时， 支付机构生成的订单号
        /// </summary>
        public string TraceNo;

        /// <summary>
        ///退款时间
        /// </summary>
        public string CraeteDatetime;

        /// <summary>
        ///批次号
        /// </summary>
        public string BatchNumber;

        /// <summary>
        ///退款笔数
        /// </summary>
        public string RefundNumber;

        /// <summary>
        ///退款数据集
        /// </summary>
        public string RefundData;

        /// <summary>
        /// 异步回调地址人为可以看不到，且数据生成）
        /// </summary>
        public string AsynCallBack;

        /// <summary>
        /// 同步回调地址（人为可以看到，且数据生成）
        /// </summary>
        public string SynCallBack;

        /// <summary>
        /// 商户编号(用户在支付机构生成的ID)
        /// </summary>
        public string AccountNo;

        /// <summary>
        /// 商户账号(用户在支付机构生成的账户)
        /// </summary>
        public string Account;

        /// <summary>
        /// 商户Key（或者公钥）
        /// </summary>
        public string AccountKey;

        /// <summary>
        /// 商户Key2（或者私钥）
        /// </summary>
        public string AccountKey2;
    }
}
