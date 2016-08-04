using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TestPay.Models;
using TestPay.Banks.Wechat;

namespace TestPay.PayCallBackInterface
{
    // ******************************************************
    // 文件名（FileName）:               WechatCallBack.cs  
    // 功能描述（Description）:          此文件用于微信回调，实现了支付回调。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class WechatCallBack : ICallBack
    {
        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="arg">支付回调参数</param>
        /// <returns>支付回调结果</returns>
        public override CallBackResult CallBack(CallBackArg arg)
        {
            #region 从返回的流中读取数据

            Stream inputstream = arg.Request.InputStream;// Request.InputStream;
            byte[] b = new byte[inputstream.Length];
            inputstream.Read(b, 0, (int)inputstream.Length);
            string inputstr = UTF8Encoding.UTF8.GetString(b);

            #endregion
    
            #region 参数获取

            string appid = string.Empty;// Request["appid"];// 	微信分配的公众账号ID
            string mch_id = string.Empty; //Request["mch_id"];//微信支付分配的商户号
            string device_info = string.Empty; //Request["device_info"];// 	微信支付分配的终端设备号
            string nonce_str = string.Empty; //Request["nonce_str"];//随机字符串
            string sign = string.Empty; //Request["sign"];//签名
            string result_code = string.Empty;// Request["result_code"];//SUCCESS/FAIL
            string err_code = string.Empty;// Request["err_code"];//错误返回的信息描述
            string err_code_des = string.Empty;// Request["err_code_des"];//错误返回的信息描述
            string openid = string.Empty; //Request["openid"];//用户在商户appid下的唯一标识
            string is_subscribe = string.Empty;// Request["is_subscribe"];//用户是否关注公众账号
            string trade_type = string.Empty; //Request["trade_type"];//JSAPI、NATIVE、APP
            string bank_type = string.Empty;// Request["bank_type"];//银行类型
            string total_fee = string.Empty;// Request["total_fee"];//订单总金额
            string fee_type = string.Empty; //Request["fee_type"];//货币类型
            string cash_fee = string.Empty;// Request["cash_fee"];//现金支付金额订单现金支付金额，详见支付金额
            string cash_fee_type = string.Empty; //Request["cash_fee_type"];//货币类型，符合ISO4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
            string coupon_fee = string.Empty; //Request["coupon_fee"];//代金券或立减优惠金额<=订单总金额，订单总金额-代金券或立减优惠金额=现金支付金额，详见支付金额
            string coupon_count = string.Empty;// Request["coupon_count"];//代金券或立减优惠使用数量
            string coupon_id_n = string.Empty; //Request["coupon_id_$n"];//代金券或立减优惠ID,$n为下标，从0开始编号
            string coupon_fee_n = string.Empty;// Request["coupon_fee_$n"];//单个代金券或立减优惠支付金额,$n为下标，从0开始编号
            string transaction_id = string.Empty;// Request["transaction_id"];//微信支付订单号
            string out_trade_no = string.Empty; //Request["out_trade_no"];//商户系统的订单号，与请求一致。
            string attach = string.Empty;// Request["attach"];//商家数据包，原样返回
            string time_end = string.Empty; //Request["time_end"];//支付完成时间
            string return_code = string.Empty;

            #endregion

            Dictionary<string, string> sp = new Dictionary<string, string>();
            sp.Add("appid", appid);
            sp.Add("mch_id", mch_id);
            sp.Add("device_info", device_info);
            sp.Add("nonce_str", nonce_str);
            sp.Add("result_code", result_code);
            sp.Add("err_code", err_code);
            sp.Add("err_code_des", err_code_des);
            sp.Add("openid", openid);
            sp.Add("is_subscribe", is_subscribe);
            sp.Add("trade_type", trade_type);
            sp.Add("bank_type", bank_type);
            sp.Add("total_fee", total_fee);
            sp.Add("fee_type", fee_type);
            sp.Add("cash_fee", cash_fee);
            sp.Add("cash_fee_type", cash_fee_type);
            sp.Add("coupon_fee", coupon_fee);
            sp.Add("coupon_count", coupon_count);
            sp.Add("coupon_id_$n", coupon_id_n);
            sp.Add("coupon_fee_$n", coupon_fee_n);
            sp.Add("transaction_id", transaction_id);
            sp.Add("out_trade_no", out_trade_no);
            sp.Add("attach", attach);
            sp.Add("time_end", time_end);
            sp.Add("sign", sign);
            sp.Add("return_code", return_code);
            JSAPISumbit.GetData(inputstr, sp);
            sign = sp["sign"];
            if (!sp.Remove("sign"))
            {
                return new CallBackResult()
                {
                    Success = false,
                    Content = "<xml><return_code><![CDATA[FAIL]]></return_code>  <return_msg><![CDATA[签名错误]]></return_msg></xml>"
                };
            }
            Dictionary<string, string> result = sp.Where(s => !string.IsNullOrEmpty(s.Value)).OrderBy(o => o.Key).ToDictionary(t => t.Key, t => t.Value);
            var str = "";
            foreach (var item in result)
            {
                str += item.Key + "=" + item.Value + "&";
            }
            str += "key=" + arg.AccountKey;
            var validate = AkHelper.EncryptHelper.MD5_32_UTF8(str).ToUpper();
            if (validate != sign)
            {
                return new CallBackResult()
                {
                    Success = false,
                    Content = "<xml><return_code><![CDATA[FAIL]]></return_code>  <return_msg><![CDATA[签名错误]]></return_msg></xml>"
                };
            }

            return new CallBackResult()
            {
                Success = true,
                PayNumber = sp["out_trade_no"],
                TraceNo = sp["transaction_id"],
                AttachInfo = sp["attach"],
                Content = "<xml><return_code><![CDATA[SUCCESS]]></return_code>  <return_msg><![CDATA[OK]]></return_msg></xml>"
            };
        }

        public override CallBackRefundResult CallBackRefund(CallBacRefundkArg arg)
        {
            throw new NotImplementedException();
        }
    }
}
