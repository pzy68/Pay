using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Models.Wechat
{
    // ******************************************************
    // 文件名（FileName）:               WechatCreditArg.cs  
    // 功能描述（Description）:          此文件用于微信支付时的结果参数实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    public class WechatCreditResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 生成的Html代码
        /// </summary>
        public string Html { get; set; }
    }
}
