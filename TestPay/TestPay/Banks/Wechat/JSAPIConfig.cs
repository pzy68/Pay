using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPay.Banks.Wechat
{
    // ******************************************************
    // 文件名（FileName）:               JSAPIConfig.cs  
    // 功能描述（Description）:          此文件用于微信支付参数配置,这些参数基本保持不变。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public static class JSAPIConfig
    {

        #region 微信支付时字段

        private static string get_code_url = string.Empty;//获取微信code的地址
        private static string pay_url = string.Empty;//微信支付的地址
        private static string get_user_opendId_url = string.Empty;//获取操作微信用户id
        private static string trade_type = "";//交易类型
        private static string grant_type = "";
        private static string wechat_redirect = "";
        private static string scope = "";
        private static string response_type = ""; 

        #endregion

        /// <summary>
        /// 初始化字段
        /// </summary>
        static JSAPIConfig()
        {

            get_code_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}{5}";
            pay_url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            get_user_opendId_url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type={3}";
            trade_type = "JSAPI";
            grant_type = "authorization_code";
            wechat_redirect = "#wechat_redirect";
            scope = "snsapi_base";
            response_type = "code";
        }

        /// <summary>
        /// 获取微信code的地址
        /// </summary>
        public static string Get_code_url
        {
            get { return get_code_url; }
        }

        /// <summary>
        /// 微信支付的地址
        /// </summary>
        public static string Pay_url
        {
            get { return pay_url; }
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        public static string Trade_type
        {
            get { return trade_type; }
        }

        public static string Wechat_redirect
        {
            get { return wechat_redirect; }
        }

        public static string Grant_type
        {
            get { return grant_type; }
        }

        /// <summary>
        /// 获取操作微信用户的url
        /// </summary>
        public static string Get_user_opendId_url
        {
            get { return get_user_opendId_url; }
        }

        public static string Scope
        {
            get { return scope; }
        }

        public static string Response_type
        {
            get { return response_type; }
        }
    }
}
