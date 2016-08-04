using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestPay.Models;

namespace TestPay.PayCallBackInterface
{
    // ******************************************************
    // 文件名（FileName）:               ICallBack.cs  
    // 功能描述（Description）:          此文件用于支付机构的回调。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public abstract partial class ICallBack
    {
        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="arg">支付回调参数</param>
        /// <returns>支付回调结果</returns>
        public abstract CallBackResult CallBack(CallBackArg arg);

        /// <summary>
        /// 退款回调
        /// </summary>
        /// <param name="arg">退款回调参数</param>
        /// <returns>退款回调结果</returns>
        public abstract CallBackRefundResult CallBackRefund(CallBacRefundkArg arg);


    }
}
