using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Pay.Models.Alipay;

//此命名空间下的方法和类，一般作为支付宝临时性方法和类，一般可根据实际需要去改变
namespace Pay.File.Alipay
{
    // ******************************************************
    // 文件名（FileName）:               AlipayProvisionalMethod.cs  
    // 功能描述（Description）:          此文件用于支付宝支付时的临时方法，可根据实际需要改变。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-05
    // ******************************************************
    internal class AlipayProvisionalMethod
    {
        /// <summary>
        /// 获取支付宝支付参数
        /// </summary>
        /// <param name="Arg">支付宝支付参数传输对象</param>
        /// <returns>支付宝支付参数(字典形式)</returns>
        internal static SortedDictionary<string, string> GetAlipayPayArgInfo(AlipayCreditArg Arg)
        {
            #region 初始化字段数据

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            string partner = Arg.AccountNo;

            //字符编码格式 目前支持 gbk 或 utf-8
            string input_charset = "utf-8";

            //支付类型 ,不能修改
            string payment_type = "1";

            //卖家支付宝用户号
            string seller_id = Arg.Account;

            //异步回调地址
            string notify_url = Arg.AsynCallBack;

            //同步回调地址
            string return_url = Arg.SynCallBack;

            //订单号
            string out_trade_no = Arg.OrderNo;

            //商品名称
            string subject = Arg.GoodsName;

            //商品描述
            string body = Arg.GoodsDesc;

            //金额
            string total_fee = Arg.Money.ToString();

            //用户IP
            string exter_invoke_ip = Arg.UserIP;

            //初始化
            AlipaySubmit.Init(Arg.AccountKey);

            #endregion

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("_input_charset", input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_id);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("anti_phishing_key", AlipaySubmit.Query_timestamp(partner, input_charset));
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            return sParaTemp;
        }

        /// <summary>
        ///  获取支付宝支付回调参数
        /// </summary>
        /// <param name="Arg">支付宝支付回调参数传输对象</param>
        /// <returns>支付宝支付回调参数(字典形式)</returns>
        internal static SortedDictionary<string, string> GetGetAlipayPayCallBackArgInfo(AliPayCallBackArg Arg)
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
            Str += "notify_id:" + Arg.Request["notify_id"] + ",sign:" + Arg.Request["sign"];

            #endregion

            return sArray;
        }

        /// <summary>
        /// 获取支付宝退款参数
        /// </summary>
        /// <param name="arg">支付宝退款参数传输对象</param>
        /// <returns>支付宝退款参数(字典形式)</returns>
        internal static SortedDictionary<string, string> GetAlipayRefundArgInfo(AlipayRefundArg arg)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //服务器异步通知页面路径
            string notify_url = arg.AsynCallBack;
            //需http://格式的完整路径，不允许加?id=123这类自定义参数

            //卖家支付宝帐户
            string seller_email = arg.Account;
            //必填

            //退款当天日期
            string refund_date = arg.CraeteDatetime;
            //必填，格式：年[4位]-月[2位]-日[2位] 小时[2位 24小时制]:分[2位]:秒[2位]，如：2007-10-01 13:13:13

            //批次号
            string batch_no = arg.BatchNumber;
            //必填，格式：当天日期[8位]+序列号[3至24位]，如：201008010000001

            //退款笔数
            string batch_num = arg.RefundNumber;
            //必填，参数detail_data的值中，“#”字符出现的数量加1，最大支持1000笔（即“#”字符出现的数量999个）

            //退款详细数据
            string detail_data = arg.RefundData;
            //必填，具体格式请参见接口技术文档

            AlipaySubmit.Init(arg.AccountKey);
            //公钥

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", arg.AccountNo);
            sParaTemp.Add("_input_charset", AlipayConfig.Input_charset.ToLower());
            sParaTemp.Add("service", "refund_fastpay_by_platform_pwd");
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("refund_date", refund_date);
            sParaTemp.Add("batch_no", batch_no);
            sParaTemp.Add("batch_num", batch_num);
            sParaTemp.Add("detail_data", detail_data);

            return sParaTemp;
        }

        /// <summary>
        /// 获取退款回调参数
        /// </summary>
        /// <param name="arg">退款回调参数对象</param>
        /// <returns>退款回调参数(字典形式)</returns>
        internal static SortedDictionary<string, string> GetAlipayRefundCallBackArgInfo(AlipayCallBacRefundkArg arg)
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

            return sArray;
        }
    }
}
