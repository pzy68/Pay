using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pay.Enums;
using Pay.PayInterface;
using Pay.Bll;

namespace Pay
{
    // ******************************************************
    // 文件名（FileName）:               Interface.cs  
    // 功能描述（Description）:          此文件用于支付时对外的接口(支付一般流程:支付--->支付回调-->退款-->退款回调)。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class Interface
    {
        /// <summary>
        ///  支付对外接口
        /// </summary>
        /// <param name="backType">支付机构(由枚举BrankType组成)</param>
        /// <param name="backAction">支付行为(由枚举BrankAction组成)</param>
        /// <param name="objects">支付参数传输对象</param>
        /// <returns>支付参数输出对象(特殊时:也可以字符串和值类型)</returns>
        public static object GetResult(int backType, int backAction, object objects)
        {
            //接口对象
            BaseInterface face = null;
            //输出对象
            object result = null;

            switch (backType)
            {

                //支付宝支付流程： 支付--->支付回调-->退款-->退款回调
                case (int)Enums.BrankType.支付宝:

                    //支付宝
                    face = new AlipayPayProcess();
                    switch (backAction)
                    {
                        case (int)Enums.BrankAction.支付:
                            result = face.Pay(objects);
                            break;
                        case (int)Enums.BrankAction.支付回调:
                            result = face.PayCallBack(objects);
                            break;
                        case (int)Enums.BrankAction.退款:
                            result = face.Refund(objects);
                            break;
                        case (int)Enums.BrankAction.退款回调:
                            result = face.RefundCallBack(objects);
                            break;
                    }

                    break;

                //微信支付流程: 授权-->支付--->支付回调
                case (int)Enums.BrankType.微信:

                    //微信
                    face = new WechatPayProcess();
                    switch (backAction)
                    {
                        case (int)Enums.BrankAction.授权获取code:
                            result = face.Pay(objects);
                            break;
                        case (int)Enums.BrankAction.支付:
                            result = face.Pay(objects);
                            break;
                        case (int)Enums.BrankAction.支付回调:
                            result = face.PayCallBack(objects);
                            break;
                    }

                    break;
            }

            return result;
        }
    }
}
