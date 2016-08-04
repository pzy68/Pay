using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPay.Banks.Alipay
{
    // ******************************************************
    // 文件名（FileName）:               AlipayConfig.cs  
    // 功能描述（Description）:          此文件用于支付宝网页版配置一些基本信息。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class AlipayConfig
    {
        #region 字段

        //字符编码格式 目前支持 gbk 或 utf-8
        private static string input_charset = "";

        //签名方式，选择项：RSA、DSA、MD5
        private static string sign_type = "";

        //支付类型 ,不能修改
        private static string payment_type = "";

        #endregion

        static AlipayConfig()
        {
            //字符编码格式 目前支持 gbk 或 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：RSA、DSA、MD5
            sign_type = "MD5";

            //支付类型 ,不能修改
            payment_type = "1";
        }

        #region 属性

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }

        /// <summary>
        /// 支付类型
        /// </summary>
        public static string Payment_type
        {
            get { return payment_type; }
        }

        #endregion
    }
}
