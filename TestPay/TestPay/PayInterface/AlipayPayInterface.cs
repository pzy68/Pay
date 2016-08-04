using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestPay.PayInterface;
using TestPay.Models;
using TestPay.Banks.Alipay;

namespace TestPay.PayInterface
{
    // ******************************************************
    // 文件名（FileName）:               AlipayPayInterface.cs  
    // 功能描述（Description）:          此文件用于支付宝网页版支付，实现了支付与退款。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class AlipayPayInterface : ICreditInterface
    {
        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="Arg">支付参数</param>
        /// <returns>支付结果</returns>
        public override CreditResult Credit(CreditArg Arg)
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

            //建立请求
            string sHtmlText = AlipaySubmit.BuildRequest(sParaTemp, "post", "确认");

            return new CreditResult
            {
                Success = true,
                Html = sHtmlText,
                Charset = "UTF-8"
            };
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="arg">退款参数</param>
        /// <returns>退款结果</returns>
        public override RefundResult Refund(Models.RefundArg arg)
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

            //建立请求
            string sHtmlText = AlipaySubmit.BuildRequest(sParaTemp, "get", "确认");

            return new RefundResult
            {
                Success = true,
                Html = sHtmlText,
                Charset = "UTF-8"
            };
        }
    }
}
