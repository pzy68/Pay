using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPay.Models
{
    // ******************************************************
    // 文件名（FileName）:               OukCode.cs  
    // 功能描述（Description）:          此文件用于支付或者退款返回的编码，用于处理各种逻辑。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class OukCode
    {
        /// <summary>
        /// 10000
        /// </summary>
        public const int 处理中 = 10000;

        /// <summary>
        /// 10001
        /// </summary>
        public const int 处理成功 = 10001;

        /// <summary>
        /// 10002
        /// </summary>
        public const int 提交中 = 10002;

        /// <summary>
        /// 10003
        /// </summary>
        public const int 签名错误 = 10003;

        /// <summary>
        /// 10006
        /// </summary>
        public const int 订单号已经存在 = 10006;

        /// <summary>
        /// 10007
        /// </summary>
        public const int 订单号不存在 = 10007;

        /// <summary>
        /// 10008
        /// </summary>
        public const int 渠道不存在 = 10008;

        /// <summary>
        /// 10100
        /// </summary>
        public const int 未知错误 = 10100;

        /// <summary>
        /// 10103
        /// </summary>
        public const int 运营商维护 = 10103;

        /// <summary>
        /// 10111
        /// </summary>
        public const int 必填参数为空或者无效 = 10111;

        /// <summary>
        /// 10112
        /// </summary>
        public const int 未开通此产品或此产品已过期 = 10112;

        /// <summary>
        /// 10113
        /// </summary>
        public const int 支付金额超出额度 = 10113;
    }
}
