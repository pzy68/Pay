using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Models.Alipay
{
    // ******************************************************
    // 文件名（FileName）:               AlipayCreditArg.cs  
    // 功能描述（Description）:          此文件用于支付宝支付时的参数实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    public class AlipayCreditArg 
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo;

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Money;

        /// <summary>
        /// 渠道编号
        /// </summary>
        public string ChannelNo;

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNo;

        /// <summary>
        /// 用户IP
        /// </summary>
        public string UserIP;

        /// <summary>
        /// 异步回调地址
        /// </summary>
        public string AsynCallBack;

        /// <summary>
        /// 同步回调地址
        /// </summary>
        public string SynCallBack;

        /// <summary>
        /// 商品地址
        /// </summary>
        public string GoodsUrl;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName;

        /// <summary>
        /// 商品描述
        /// </summary>
        public string GoodsDesc;

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl;

        /// <summary>
        /// 商户编号
        /// </summary>
        public string AccountNo;

        /// <summary>
        /// 商户账号
        /// </summary>
        public string Account;

        /// <summary>
        /// 商户Key（或者公钥或微信商户密钥）
        /// </summary>
        public string AccountKey;

        /// <summary>
        /// 商户Key2（或者私钥）
        /// </summary>
        public string AccountKey2;

        /// <summary>
        /// 商户名称
        /// </summary>
        public string AccountName;
    }
}
