using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pay.Models.Wechat;
using Pay.PayInterface;
using Pay.File.Wechat;

namespace Pay.Bll
{
    // ******************************************************
    // 文件名（FileName）:               WechatPayProcess.cs  
    // 功能描述（Description）:          此文件用于微信支付。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-04
    // ******************************************************
    internal class WechatPayProcess : WechatInterface
    {

        /// <summary>
        /// 微信(只用于微信获取code返回url)
        /// </summary>
        /// <param name="objects">授权(获取code)参数</param>
        /// <returns>url地址</returns>
        public object GetWechatCode(object objects)
        {
            WechatCodeArg arg = (WechatCodeArg)objects;

            return JSAPISumbit.GetCodeUrl(arg.redirect_uri, arg.appid, arg.state);
        }

        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="objects">微信支付参数</param>
        /// <returns>微信支付结果</returns>
        public object Pay(object objects)
        {
            WechatCreditArg arg = (WechatCreditArg)objects;

            #region 获取支付信息

            var remoteResult = JSAPISumbit.PostXml(JSAPIConfig.Pay_url, WechatPayProvisionalMethod.GetWechatPayInfo(arg));
            XmlUserPayInfo xmlmodel = JSAPISumbit.GetDataMode<XmlUserPayInfo>(remoteResult);

            #endregion

            #region 解析支付信息


            LaunchPay model = new LaunchPay();
            WechatPayProvisionalMethod.GetLaunchPayInfo(arg, model, xmlmodel);

            #endregion

            return new WechatCreditResult()
            {
                Success = true,
                Html = JSAPISumbit.Html(model),
            };
        }

        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <param name="objects">微信支付回调</param>
        /// <returns>微信支付回调结果</returns>
        public object PayCallBack(object objects)
        {
            //微信支付回调参数对象
            WechatCallBackArg arg = (WechatCallBackArg)objects;
            //微信支付回调参数解析成的字典
            Dictionary<string, string> sp;
            //微信支付回调参数中的签名
            string sign;
            //根据微信支付回调参数生成签名
            string validate = string.Empty;

            //验证微信回调参数是否存在签名
            if (WechatPayProvisionalMethod.ValidatePayCallBackSign(arg, out sign, out sp))
            {
                return new WechatCallBackResult()
                {
                    Success = false,
                    Content = "<xml><return_code><![CDATA[FAIL]]></return_code>  <return_msg><![CDATA[签名错误]]></return_msg></xml>"
                };
            }

            validate = WechatPayProvisionalMethod.GetPayCallBackSignByWechatInfo(sp, arg);

            //验证微信签名与自己手动生成签名是否一致
            if (validate != sign)
            {
                return new WechatCallBackResult()
                {
                    Success = false,
                    Content = "<xml><return_code><![CDATA[FAIL]]></return_code>  <return_msg><![CDATA[签名错误]]></return_msg></xml>"
                };
            }

            return new WechatCallBackResult()
            {
                Success = true,
                PayNumber = sp["out_trade_no"],
                TraceNo = sp["transaction_id"],
                AttachInfo = sp["attach"],
                Content = "<xml><return_code><![CDATA[SUCCESS]]></return_code>  <return_msg><![CDATA[OK]]></return_msg></xml>"
            };
        }

        public object Refund(object objects)
        {
            throw new NotImplementedException();
        }

        public object RefundCallBack(object objects)
        {
            throw new NotImplementedException();
        }
    }
}
