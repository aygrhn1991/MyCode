using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHelper
{
    public class WeChat_Parameters
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        public string Token { get; set; }
        public string mch_id { get; set; }
        public string mch_key { get; set; }
        public string Note { get; set; }
    }
    public class WeChat_AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public DateTime get_time { get; set; }
    }
    public class WeChat_CodeModel
    {
        public string code { get; set; }
        public string state { get; set; }
    }
    public class WeChat_AccessToken_UserInfo
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }
    }
    public class WeChat_snsapi_UserInfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string language { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string[] privilege { get; set; }
        public string unionid { get; set; }
    }
    public class WeChat_UserInfo
    {
        public int subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string language { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_time { get; set; }
        public string unionid { get; set; }
        public string remark { get; set; }
        public string groupid { get; set; }
    }
    public class WeChat_JSSDKConfig
    {
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
    }
    public class WeChat_JSApiTicket
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
        public DateTime get_time { get; set; }
    }
    public class WeChat_PayConfig
    {
        public string appId { get; set; }
        public string timeStamp { get; set; }
        public string nonceStr { get; set; }
        public string package { get; set; }
        public string signType { get; set; }
        public string paySign { get; set; }
    }
    public class WeChat_UnifiedOrderRequest
    {
        public string appid { get; set; }
        public string mch_id { get; set; }
        private string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string body { get; set; }
        private string detail { get; set; }
        private string attach { get; set; }
        public string out_trade_no { get; set; }
        private string fee_type { get; set; }
        public int total_fee { get; set; }
        public string spbill_create_ip { get; set; }
        private string time_start { get; set; }
        private string time_expire { get; set; }
        private string goods_tag { get; set; }
        public string notify_url { get; set; }
        public string trade_type { get; set; }
        private string product_id { get; set; }
        private string limit_pay { get; set; }
        public string openid { get; set; }
    }
    public class WeChat_UnifiedOrderResponse
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string trade_type { get; set; }
        public string prepay_id { get; set; }
        public string code_url { get; set; }
    }
    public class WeChat_PayCallBack
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string openid { get; set; }
        public string is_subscribe { get; set; }
        public string trade_type { get; set; }
        public string bank_type { get; set; }
        public string total_fee { get; set; }
        public string fee_type { get; set; }
        public string cash_fee { get; set; }
        public string cash_fee_type { get; set; }
        public string coupon_fee { get; set; }
        public string coupon_count { get; set; }
        public string coupon_id_0 { get; set; }
        public string coupon_fee_0 { get; set; }
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public string attach { get; set; }
        public string time_end { get; set; }
    }
    public class WeChat_PayCallBackResponse
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
    }
    public class WeChat_SceneQRTicket
    {
        public string ticket { get; set; }
        public int expire_seconds { get; set; }
        public string url { get; set; }
        public DateTime get_time { get; set; }
    }
    public class WeChat_SceneQRCallBack
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string CreateTime { get; set; }
        public string MsgType { get; set; }
        public string Event { get; set; }
        public string EventKey { get; set; }
        public string Ticket { get; set; }
    }
    public class WeChat_TemplateMessage
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public object data { get; set; }
    }    
    public enum WeChat_snsapi_scope
    {
        snsapi_base,
        snsapi_userinfo,
    }
    public enum WeChat_SignType
    {
        SHA1,
        MD5,
    }
    public enum WeChat_PayResult
    {
        SUCCESS,
        FAIL,
    }
    public enum WeChat_SceneQRType
    {
        QR_SCENE,
        QR_LIMIT_SCENE,
    }
    public enum WeChat_SceneQREventType
    {
        subscribe,
        SCAN,
    }
}
