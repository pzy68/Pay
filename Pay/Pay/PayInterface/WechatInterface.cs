using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.PayInterface
{
    // ******************************************************
    // 文件名（FileName）:               WechatInterface.cs  
    // 功能描述（Description）:          此文件用于微信支付机构基类的接口。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    interface WechatInterface : BaseInterface
    {
        /// <summary>
        /// 微信授权获取code
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        object GetWechatCode(object objects);
    }
}
