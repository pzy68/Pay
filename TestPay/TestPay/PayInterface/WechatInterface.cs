using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestPay.Banks.Wechat;
using TestPay.Models;

namespace TestPay.PayInterface
{
    // ******************************************************
    // 文件名（FileName）:               WechatInterface.cs  
    // 功能描述（Description）:          此文件用于微信支付，实现了支付。
    // 作者（Author）:                   pzy
    // 日期（Create Date）:              2016-08-03
    // ******************************************************
    public class WechatInterface : ICreditInterface
    {
        public override CreditResult Credit(Models.CreditArg arg)
        {
            #region 下单（需要知道微信用户的code用来获取openid）

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

            #region 发起支付信息

            var remoteResult = JSAPISumbit.PostXml(JSAPIConfig.Pay_url, data);
            XmlUserPayInfo xmlmodel = JSAPISumbit.GetDataMode<XmlUserPayInfo>(remoteResult);

            #endregion

            #endregion

            #region 发起支付

            #region 配置参数

            LaunchPay model = new LaunchPay();
            model.appId = appid;
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

            #endregion

            return new CreditResult()
            {
                Success = true,
                Html = JSAPISumbit.Html(model),
            };
        }

        public override Models.RefundResult Refund(Models.RefundArg arg)
        {
            throw new NotImplementedException();
        }
    }
}
