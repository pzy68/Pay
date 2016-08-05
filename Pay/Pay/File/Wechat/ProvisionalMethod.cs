using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pay.Models.Wechat;

//此命名空间下的方法和类，一般作为微信临时性方法和类，一般可根据实际需要去改变
namespace Pay.File.Wechat
{
    // ******************************************************
    // 文件名（FileName）:               WechatPayProvisionalMethod.cs  
    // 功能描述（Description）:          此文件用于微信支付时的临时方法，可根据实际需要改变。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-05
    // ******************************************************
    internal class WechatPayProvisionalMethod
    {
        /// <summary>
        /// 获取微信支付信息(用于页面显示)
        /// </summary>
        /// <param name="arg">微信支付参数</param>
        internal static string GetWechatPayInfo(WechatCreditArg arg)
        {
            #region 参数

            string appid = arg.PublicNo;//公众账号id
            string mch_id = arg.AccountNo;//微信支付分配的商户号
            string nonce_str = new Random().Next(1, 9999).ToString();//随机字符串，不长于32位
            string sign = "";//签名
            string body = arg.GoodsName;//商品或支付单简要描述
            string out_trade_no = arg.OrderNo;//System.DateTime.Now.ToString("yyyyMMddHHmmssfff");//System.DateTime.Now.ToString("yyyyMMddHHmmssfff");//商户订单号
            string total_fee = ((int)arg.Money).ToString(); ;//订单总金额
            string spbill_create_ip = AkHelper.HttpHelper.GetIP();//用户端ip
            string notify_url = arg.AsynCallBack;//异步通知地址
            string trade_type = JSAPIConfig.Trade_type;//交易类型
            string attach = arg.attach;//附加数据
            string openid = JSAPISumbit.GetUserId(arg.code, arg.PublicNo, arg.PublicKey);//用户标识

            #endregion

            #region 签名生成

            Dictionary<string, string> sp = new Dictionary<string, string>();
            sp.Add("appid", appid);
            sp.Add("mch_id", mch_id);
            sp.Add("nonce_str", nonce_str);
            sp.Add("body", body);
            sp.Add("out_trade_no", out_trade_no);
            sp.Add("total_fee", total_fee);
            sp.Add("spbill_create_ip", spbill_create_ip);
            sp.Add("notify_url", notify_url);
            sp.Add("trade_type", trade_type);
            sp.Add("attach", attach);
            sp.Add("openid", openid);

            Dictionary<string, string> result = new Dictionary<string, string>();
            result = sp.OrderBy(o => o.Key).ToDictionary(t => t.Key, t => t.Value);
            string con = "";
            foreach (var item in result)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    con += item.Key + "=" + item.Value + "&";
                }
            }
            con += "key=" + arg.AccountKey;

            sign = AkHelper.EncryptHelper.MD5_32_UTF8(con).ToUpper();

            #endregion

            #region 传递参数

            sp.Add("sign", sign);
            string data = JSAPISumbit.GetXmlData(sp);

            #endregion

            return data;

        }

        /// <summary>
        /// 解析微信支付信息(用于页面显示)
        /// </summary>
        /// <param name="arg">微信支付信息参数</param>
        /// <param name="model">获取解析微信支付信息对象</param>
        /// <param name="xmlmodel">解析微信支付信息</param>
        internal static void GetLaunchPayInfo(WechatCreditArg arg, LaunchPay model, XmlUserPayInfo xmlmodel)
        {
            #region 配置参数

            model.appId = arg.PublicNo;  //appid;  //公众账号id
            model.timeStamp = AkHelper.DateTimeHelper.ToUnix(System.DateTime.Now).ToString();
            model.nonceStr = new Random().Next(1, 99999).ToString();
            model.package = "prepay_id=" + xmlmodel.prepay_id;
            model.signType = "MD5";
            model.HtmlPayUrl = arg.HtmlPayUrl;

            #endregion

            #region 签名生成

            Dictionary<string, string> sb = new Dictionary<string, string>();
            sb.Add("appId", model.appId);
            sb.Add("timeStamp", model.timeStamp);
            sb.Add("nonceStr", model.nonceStr);
            sb.Add("signType", model.signType);
            sb.Add("package", model.package);
            Dictionary<string, string> resultPay = new Dictionary<string, string>();
            resultPay = sb.OrderBy(o => o.Key).ToDictionary(t => t.Key, t => t.Value);
            string constr = "";
            foreach (var item in resultPay)
            {
                constr += item.Key + "=" + item.Value + "&";
            }
            constr += "key=" + arg.AccountKey;
            model.paySign = AkHelper.EncryptHelper.MD5_32_UTF8(constr).ToUpper();

            #endregion
        }

        /// <summary>
        /// 验证微信支付回调是否存在由微信生成的签名
        /// </summary>
        /// <param name="arg">微信支付回调参数</param>
        /// <param name="data">输出由微信生成的签名</param>
        /// <param name="sp">输出微信支付回调参数生成的字典</param>
        /// <returns>true表示存在,false表示不存在</returns>
        internal static bool ValidatePayCallBackSign(WechatCallBackArg arg, out string data, out Dictionary<string, string> dic)
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

            #region 对输出参数赋值

            dic = new Dictionary<string, string>();
            dic = sp;
            data = sign;

            #endregion

            if (!sp.Remove("sign"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据微信回调参数生成签名
        /// </summary>
        /// <param name="sp">微信回调参数(字典形式)</param>
        /// <param name="arg">微信回调参数</param>
        /// <returns>签名</returns>
        internal static string GetPayCallBackSignByWechatInfo(Dictionary<string,string> sp,WechatCallBackArg arg)
        {
            Dictionary<string, string> result = sp.Where(s => !string.IsNullOrEmpty(s.Value)).OrderBy(o => o.Key).ToDictionary(t => t.Key, t => t.Value);
            var str = "";
            foreach (var item in result)
            {
                str += item.Key + "=" + item.Value + "&";
            }
            str += "key=" + arg.AccountKey;
            var validate = AkHelper.EncryptHelper.MD5_32_UTF8(str).ToUpper();
           
            return validate;
        }
    }
}
