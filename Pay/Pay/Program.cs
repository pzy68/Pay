using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pay.PayInterface;
using Pay.Models.Alipay;
using Pay.Bll;

namespace Pay
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 支付宝

            //创建支付宝接口对象
            BaseInterface alipayPay = new AlipayPayProcess();

            //支付
            alipayPay.Pay(new object());  //参数：AlipayCreditArg对象，结果:AlipayCreditResult对象

            //支付回调
            alipayPay.PayCallBack(new object());  //参数：AliPayCallBackArg对象，结果:AlipayCallBackResult对象

            //退款
            alipayPay.Refund(new object()); //参数：AlipayRefundArg对象，结果:AlipayRefundResult对象

            //退款回调
            alipayPay.RefundCallBack(new object());  //参数：AlipayCallBacRefundkArg对象，结果:AlipayCallBackRefundResult对象

            #endregion

            #region 微信

            //创建微信接口对象
            BaseInterface wechatPay = new WechatPayProcess();

            //支付
            wechatPay.Pay(new object()); //参数：WechatCreditArg对象，结果:WechatCreditResult对象

            //支付回调
            wechatPay.PayCallBack(new object());  //参数：WechatCallBackArg对象，结果:WechatCallBackResult对象

            //创建微信特殊接口对象
            WechatInterface wechatSpecialPay = new WechatPayProcess();

            //微信授权
            wechatSpecialPay.GetWechatCode(new object());   //参数：WechatCodeArg对象，结果:字符串

            #endregion
        }
    }
}
