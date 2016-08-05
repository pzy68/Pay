using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Pay.PayInterface;
using Pay.Models.Alipay;
using Pay.File.Alipay;

namespace Pay.Bll
{
    // ******************************************************
    // 文件名（FileName）:               AlipayPayProcess.cs  
    // 功能描述（Description）:          此文件用于支付宝网页版支付，实现了支付与退款流程。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    internal class AlipayPayProcess : BaseInterface
    {
        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="objects">支付回调参数对象</param>
        /// <returns>支付回调结果</returns>
        public object Pay(object objects)
        {
            // 支付宝支付参数传输对象
            AlipayCreditArg Arg = (AlipayCreditArg)objects;
            //支付宝支付参数(字典形式)
            SortedDictionary<string, string> sParaTemp = AlipayProvisionalMethod.GetAlipayPayArgInfo(Arg);

            //建立请求
            string sHtmlText = AlipaySubmit.BuildRequest(sParaTemp, "post", "确认");

            return new AlipayCreditResult
            {
                Success = true,
                Html = sHtmlText,
                Charset = "UTF-8"
            };
        }

        /// <summary>
        ///  支付回调
        /// </summary>
        /// <param name="objects">支付回调参数</param>
        /// <returns>支付回调结果</returns>
        public object PayCallBack(object objects)
        {
            // 支付宝支付参数传输对象
            AliPayCallBackArg Arg = (AliPayCallBackArg)objects;
            //支付宝支付参数(字典形式)
            SortedDictionary<string, string> sArray = AlipayProvisionalMethod.GetGetAlipayPayCallBackArgInfo(Arg);
            //初始化参数信息
            AlipayNotify aliNotify = new AlipayNotify(Arg.AccountNo, Arg.AccountKey);

            //参数验证
            bool verifyResult = aliNotify.Verify(sArray, Arg.Request["notify_id"], Arg.Request["sign"]);
            if (!verifyResult)
                throw new Exception("消息验证时，失败");

            string trade_status = Arg.Request["trade_status"];

            return new AlipayCallBackResult
            {
                PayNumber = Arg.Request["out_trade_no"],
                TraceNo = Arg.Request["trade_no"],
                Success = (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS"),
                ChannelCode = trade_status
            };
        }

        /// <summary>
        ///  退款
        /// </summary>
        /// <param name="objects">退款传输对象</param>
        /// <returns>退款结果</returns>
        public object Refund(object objects)
        {
            //退款传输对象
            AlipayRefundArg arg = (AlipayRefundArg)objects;
            //退款传输对象(字典形式)
            SortedDictionary<string, string> sParaTemp = AlipayProvisionalMethod.GetAlipayRefundArgInfo(arg);

            //建立请求
            string sHtmlText = AlipaySubmit.BuildRequest(sParaTemp, "get", "确认");

            return new AlipayRefundResult
            {
                Success = true,
                Html = sHtmlText,
                Charset = "UTF-8"
            };
        }

        /// <summary>
        /// 退款回调
        /// </summary>
        /// <param name="objects">退款传输对象</param>
        /// <returns>退款回调结果</returns>
        public object RefundCallBack(object objects)
        {
            //退款传输对象
            AlipayCallBacRefundkArg arg = (AlipayCallBacRefundkArg)objects;
            //初始化数据
            AlipayNotify aliNotify = new AlipayNotify(arg.AccountNo, arg.AccountKey);
            //退款参数(字典形式)
            SortedDictionary<string, string> sArray = AlipayProvisionalMethod.GetAlipayRefundCallBackArgInfo(arg);

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

            return new AlipayCallBackRefundResult
            {
                result_details = arg.Request["result_details"],
                Trade_status = success_num > 0 ? "success" : "fail",//用于回应支付宝是否成功
                Success = true//指当前业务流程有无错误
            };
        }
    }
}
