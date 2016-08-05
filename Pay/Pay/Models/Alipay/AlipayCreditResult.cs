using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pay.Models.Alipay
{
    // ******************************************************
    // 文件名（FileName）:               AlipayCreditResult.cs  
    // 功能描述（Description）:          此文件用于支付宝支付时的结果实体类。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    public class AlipayCreditResult 
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 生成的Html代码
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Charset { get; set; }
    }
}
