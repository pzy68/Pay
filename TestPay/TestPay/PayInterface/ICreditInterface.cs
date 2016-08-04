using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestPay.Models;
using TestPay.Banks.Alipay;

namespace TestPay.PayInterface
{
    // ******************************************************
    // 文件名（FileName）:               ICreditInterface.cs  
    // 功能描述（Description）:          此文件用于支付机构基类的接口。
    // 数据表（Tables）:                 CreditResult, CreditArg
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public abstract class ICreditInterface
    {
        /// <summary>
        /// 请求支付接口
        /// </summary>
        /// <param name="arg">支付参数</param>
        /// <returns>返回支付结果</returns>
        public abstract CreditResult Credit(CreditArg arg);

        /// <summary>
        /// 请求退款接口
        /// </summary>
        /// <param name="arg">退款参数</param>
        /// <returns>返回退款结果</returns>
        public abstract RefundResult Refund(RefundArg arg);
    }
}
