using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Enums
{
    // ******************************************************
    // 文件名（FileName）:               BrankType.cs  
    // 功能描述（Description）:          此文件显示支付机构类型。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public enum BrankType
    {

        支付宝 = 0,

        微信 = 5
    }

    // ******************************************************
    // 文件名（FileName）:               BrankAction.cs  
    // 功能描述（Description）:          此文件显示支付行为。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public enum BrankAction
    {
        授权获取code = 0,

        支付 = 5,

        支付回调 = 10,

        退款 = 15,

        退款回调 = 20,
    }
}
