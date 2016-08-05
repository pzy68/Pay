using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//此命名空间下的C参数，属于支付宝参数基本信息，一般不作改变
namespace Pay.File.Alipay
{

    // ******************************************************
    // 文件名（FileName）:               AlipayConfig.cs  
    // 功能描述（Description）:          此文件用于支付宝参数的基本配置。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    internal class AlipayConfig
    {
        #region 字段

        //字符编码格式 目前支持 gbk 或 utf-8
        protected static string input_charset = "";

        //签名方式，选择项：RSA、DSA、MD5
        protected static string sign_type = "";

        //支付类型 ,不能修改
        protected static string payment_type = "";

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

    // ******************************************************
    // 文件名（FileName）:               AlipayConfig.cs  
    // 功能描述（Description）:          此文件用于支付宝参数的回调基本配置。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    internal class AlipayNotifyConfig : AlipayConfig
    {
        #region 字段

        protected string _partner = "";               //合作身份者ID
        protected string _key = "";                   //商户的私钥
        protected string _input_charset = "";         //编码格式
        protected string _sign_type = "";             //签名方式

        //支付宝消息验证地址
        protected string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";

        #endregion

        /// <summary>
        /// 构造函数
        /// 从配置文件中初始化变量
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notify_id">通知验证ID</param>
        /// 
        public AlipayNotifyConfig(string partner, string key)
        {
            //初始化基础配置信息
            _partner = partner;
            _key = key;
            _input_charset = Input_charset;
            _sign_type = Sign_type.Trim().ToUpper();
        }
    }

    // ******************************************************
    // 文件名（FileName）:               AlipayConfig.cs  
    // 功能描述（Description）:          此文件用于支付宝参数的提交时基本配置。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    internal class AlipaySubmitConfig : AlipayConfig
    {
        #region 字段

        //支付宝网关地址（新）
        protected static string GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?";

        //商户的私钥
        protected static string _key = "";

        //编码格式
        protected static string _input_charset = "";

        //签名方式
        protected static string _sign_type = "";

        #endregion

        /// <summary>
        ///构造函数初始化数据
        /// </summary>
        static AlipaySubmitConfig()
        {
            _input_charset = Input_charset.Trim().ToLower();
            _sign_type = Sign_type.Trim().ToUpper();
        }
    }
}
