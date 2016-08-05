using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Pay.PayInterface
{
    // ******************************************************
    // 文件名（FileName）:               BaseInterface.cs  
    // 功能描述（Description）:          此文件用于支付机构基类的接口。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public interface BaseInterface
    {
        /// <summary>
        /// 支付
        /// </summary>
        object Pay(object objects);

        /// <summary>
        /// 支付回调
        /// </summary>
        object PayCallBack(object objects);

        /// <summary>
        /// 退款
        /// </summary>
        object Refund(object objects);

        /// <summary>
        /// 退款回调
        /// </summary>
        object RefundCallBack(object objects);
    }
}
