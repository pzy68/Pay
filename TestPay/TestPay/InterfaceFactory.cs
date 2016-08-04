using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestPay.Models;
using TestPay.Banks.Wechat;
using TestPay.Banks.Alipay;
using TestPay.PayCallBackInterface;
using TestPay.PayInterface;

namespace TestPay
{
    // ******************************************************
    // 文件名（FileName）:               InterfaceFactory.cs  
    // 功能描述（Description）:          此文件用于支付对外接口(含有支付与退款)。
    // 数据表（Tables）:                 CreditResult, CreditArg ,RefundArg, RefundResult ,CallBacRefundkArg，CallBackRefundResult ，CallBackArg，CallBackResult，OukCode
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class InterfaceFactory
    {
        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="arg">支付参数</param>
        /// <returns>支付结果</returns>
        public static CreditResult CallInterface(CreditArg arg)
        {
            ICreditInterface face = null;
            switch (arg.ChannelNo)
            {
                case "Alipay": //支付宝
                    face = new AlipayPayInterface();
                    break;
                case "Wechat"://微信
                    face = new WechatInterface();
                    break;
            }
            if (face == null)
            {
                return new CreditResult()
                {
                    Success = false,
                    OukCode = OukCode.渠道不存在,
                    Msg = "渠道不存在",
                    Html = "渠道不存在"
                };
            }
            return face.Credit(arg);
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="arg">退款参数</param>
        /// <returns>退款结果</returns>
        public static RefundResult Refund(RefundArg arg)
        {
            ICreditInterface face = null;
            switch (arg.ChannelNo)
            {
                case "Alipay": //支付宝
                    face = new AlipayPayInterface();
                    break;
            }
            if (face == null)
            {
                return new RefundResult()
                {
                    Success = false,
                    OukCode = OukCode.渠道不存在,
                    Msg = "渠道不存在",
                    Html = "渠道不存在"
                };
            }
            return face.Refund(arg);
        }

        /// <summary>
        /// 退款回调
        /// </summary>
        /// <param name="arg">退款回调参数</param>
        /// <returns>退款回调结果</returns>
        public static CallBackRefundResult CallBack(CallBacRefundkArg arg)
        {
            ICallBack face = null;
            switch (arg.ChannelNo)
            {
                case "Alipay": //支付宝
                    face = new AlipayCallBack();
                    break;
            }
            if (face == null)
            {
                throw new Exception("渠道不存在");
            }
            return face.CallBackRefund(arg);

        }

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="arg">支付回调参数</param>
        /// <returns>支付回调结果</returns>
        public static CallBackResult CallBack(CallBackArg arg)
        {
            ICallBack face = null; ;
            switch (arg.ChannelNo)
            {
                case "Alipay": //支付宝
                    face = new AlipayCallBack();
                    break;
                case "Wechat"://微信
                    face = new WechatCallBack();
                    break;
            }
            if (face == null)
            {
                throw new Exception("渠道不存在");
            }
            return face.CallBack(arg);
        }

        #region 微信支付获取信息
        /// <summary>
        /// 微信(只用于微信获取code返回url)
        /// </summary>
        /// <param name="redirect_uri">获取code后的跳转地址</param>
        /// <param name="appid">公主号id</param>
        /// <param name="state">给微信的备注</param>
        /// <returns>获取code的url</returns>
        public static string WechatJSAPIGetInfo(string redirect_uri, string appid, string state = "")
        {
            return JSAPISumbit.GetCodeUrl(redirect_uri, appid, state);
        }

        #endregion
    }
}
