using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using TestPay.Models;
using TestPay.Banks.Alipay;

namespace TestPay.PayCallBackInterface
{
    // ******************************************************
    // 文件名（FileName）:               AlipayCallBack.cs  
    // 功能描述（Description）:          此文件用于支付宝网页版回调，实现了支付回调与退款回调。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class AlipayCallBack : ICallBack
    {
        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="Arg">支付回调参数</param>
        /// <returns>支付回调结果</returns>
        public override CallBackResult CallBack(CallBackArg Arg)
        {
            #region 参数获取

            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            string RequestType = Arg.Request.RequestType;
            if (RequestType == "POST")
            {
                coll = Arg.Request.Form;
            }
            else
                coll = Arg.Request.QueryString;
            String[] requestItem = coll.AllKeys;
            string Str = string.Empty;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Arg.Request[requestItem[i]]);

                Str += "Key:" + requestItem[i] + "Value:" + Arg.Request[requestItem[i]];
            }
            AlipayNotify aliNotify = new AlipayNotify(Arg.AccountNo, Arg.AccountKey);
            Str += "notify_id:" + Arg.Request["notify_id"] + ",sign:" + Arg.Request["sign"];

            #endregion

            //参数验证
            bool verifyResult = aliNotify.Verify(sArray, Arg.Request["notify_id"], Arg.Request["sign"]);
            if (!verifyResult)
                throw new Exception("消息验证时，失败");

            string trade_status = Arg.Request["trade_status"];

            return new CallBackResult
            {
                PayNumber = Arg.Request["out_trade_no"],
                TraceNo = Arg.Request["trade_no"],
                Success = (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS"),
                ChannelCode = trade_status
            };
        }

        /// <summary>
        /// 支付宝退款回调方法
        /// </summary>
        /// <param name="arg"> 支付宝退款回调方法的参数</param>
        /// <returns>退款回调结果</returns>
        public override CallBackRefundResult CallBackRefund(CallBacRefundkArg arg)
        {
            #region 参数获取

            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            string RequestType = arg.Request.RequestType;

            if (RequestType == "POST")
            {
                coll = arg.Request.Form;
            }
            else
                coll = arg.Request.QueryString;
            String[] requestItem = coll.AllKeys;
            string Str = string.Empty;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], arg.Request[requestItem[i]]);

                Str += "Key:" + requestItem[i] + "Value:" + arg.Request[requestItem[i]];
            }
            AlipayNotify aliNotify = new AlipayNotify(arg.AccountNo, arg.AccountKey);
            Str += "notify_id:" + arg.Request["notify_id"] + ",sign:" + arg.Request["sign"];

            #endregion

            //参数验证
            bool verifyResult = aliNotify.Verify(sArray, arg.Request["notify_id"], arg.Request["sign"]);
            if (!verifyResult)
            {
                throw new Exception("消息验证时，失败");
            }
            int success_num = 0;
            try
            {
                success_num = int.Parse(arg.Request["success_num"]);
            }
            catch
            {
                success_num = 0;
            }

            return new CallBackRefundResult
            {
                result_details = arg.Request["result_details"],
                Trade_status = success_num > 0 ? "success" : "fail",//用于回应支付宝是否成功
                Success = true//指当前业务流程有无错误
            };
        }
    }
}