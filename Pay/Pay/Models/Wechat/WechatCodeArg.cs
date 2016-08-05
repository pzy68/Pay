using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Models.Wechat
{
    // ******************************************************
    // 文件名（FileName）:               WechatCallBackArg.cs  
    // 功能描述（Description）:          此文件用于微信支付前的授权(获取code)。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    public class WechatCodeArg
    {
        /// <summary>
        /// 获取code后的跳转地址
        /// </summary>
        public string redirect_uri { get; set; }

        /// <summary>
        ///  公众号id
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 给微信的备注
        /// </summary>
        public string state { get; set; }

    }
}
