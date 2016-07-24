using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoginHelper
{
   public class WeChatLogin
    {
        Helper helper = new Helper();

        #region 服务器配置
        public string CheckConfiguration()
        {
            string signature = HttpContext.Current.Request["signature"];
            string timestamp = HttpContext.Current.Request["timestamp"];
            string nonce = HttpContext.Current.Request["nonce"];
            string echostr = HttpContext.Current.Request["echostr"];
            string token = helper.GetWeChatParameters().Token;

            string[] tempArray = { token, timestamp, nonce };
            Array.Sort(tempArray);
            string tempString = string.Join("", tempArray);

            SHA1 sha1 = SHA1.Create();
            byte[] tempBytes = Encoding.UTF8.GetBytes(tempString);
            tempBytes = sha1.ComputeHash(tempBytes);
            sha1.Clear();
            string resultSring = BitConverter.ToString(tempBytes).Replace("-", "").ToLower();
            if (resultSring == signature)
                //HttpContext.Current.Response.Write(echostr);
                return echostr;
            return "error";
        }
        #endregion

        #region 基础accesstoken
        private string GetAccessToken()
        {
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/wc_access_token.json");
            if (File.Exists(path))
            {
                string tempString = helper.ReadFromFile(path);
                if (string.IsNullOrWhiteSpace(tempString))
                {
                    RequestAccessToken();
                }
                else
                {
                    WeChat_AccessToken accesstoken = helper.GetObject(new WeChat_AccessToken());
                    if ((DateTime.Now - accesstoken.get_time).TotalSeconds > accesstoken.expires_in)
                    {
                        RequestAccessToken();
                    }
                }
            }
            else
            {
                RequestAccessToken();
            }
            return helper.GetObject(new WeChat_AccessToken()).access_token;
        }
        private void RequestAccessToken()
        {
            WeChat_Parameters parameters = helper.GetWeChatParameters();
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + parameters.AppID + "&secret=" + parameters.AppSecret;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            response = response.Replace("}", ",\"get_time\":\"" + DateTime.Now.ToString() + "\"}");
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/wc_access_token.json");
            helper.WriteToFile(response, path);
        }
        #endregion

        #region 网页授权获取用户信息
        public string RequestCodeUrl(string unEncodedRediretUri, string state, WeChat_snsapi_scope scope)
        {
            WeChat_Parameters parameters = helper.GetWeChatParameters();
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + parameters.AppID + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&response_type=code&scope=" + scope.ToString() + "&state=" + state + "#wechat_redirect";
        }
        public string GetOpenIdBy_snsapi_base()
        {
            string code = GetCode().code;
            return RequestInfoAccessToken(code).openid;
        }
        public WeChat_snsapi_UserInfo GetUserInfoBy_snsapi_userinfo()
        {
            string code = GetCode().code;
            WeChat_AccessToken_UserInfo accesstoken_userinfo = RequestInfoAccessToken(code);
            string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + accesstoken_userinfo.access_token + "&openid=" + accesstoken_userinfo.openid + "&lang=zh_CN";
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new WeChat_snsapi_UserInfo(), response);
        }
        private WeChat_CodeModel GetCode()
        {
            WeChat_CodeModel codemodel = new WeChat_CodeModel();
            codemodel.code = HttpContext.Current.Request["code"];
            codemodel.state = HttpContext.Current.Request["state"];
            return codemodel;
        }
        private WeChat_AccessToken_UserInfo RequestInfoAccessToken(string code)
        {
            WeChat_Parameters parameters = helper.GetWeChatParameters();
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + parameters.AppID + "&secret=" + parameters.AppSecret + "&code=" + code + "&grant_type=authorization_code";
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new WeChat_AccessToken_UserInfo(), response);
        }
        #endregion

        #region accesstoken直接拉取用户信息
        public WeChat_UserInfo GetUserInfo(string openId)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + GetAccessToken() + "&openid=" + openId + "&lang=zh_CN";
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new WeChat_UserInfo(), response);
        }
        #endregion

        #region JS-SDK
        public WeChat_JSSDKConfig GetJSSDKConfig(string url)
        {
            WeChat_Parameters wechatparameters = helper.GetWeChatParameters();
            WeChat_JSSDKConfig jssdkconfig = new WeChat_JSSDKConfig();
            jssdkconfig.appId = wechatparameters.AppID;
            jssdkconfig.nonceStr = helper.GetNonceStr();
            jssdkconfig.timestamp = helper.GetTimeStamp(DateTime.Now).ToString();
            jssdkconfig.signature = GetJSSDKSignature(jssdkconfig.nonceStr, jssdkconfig.timestamp, url);
            return jssdkconfig;
        }
        private string GetJSSDKSignature(string nonceStr, string timeStamp, string url)
        {
            string ticket = GetJSApiTicket();
            string[] array = { "noncestr=" + nonceStr, "jsapi_ticket=" + ticket, "timestamp=" + timeStamp, "url=" + url };
            Array.Sort(array);
            string str = string.Join("&", array);
            SHA1 sha1 = SHA1.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            bytes = sha1.ComputeHash(bytes);
            sha1.Clear();
            string signature = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return signature;
        }
        private string GetJSApiTicket()
        {
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/wc_jsapi_ticket.json");
            if (File.Exists(path))
            {
                string tempString = helper.ReadFromFile(path);
                if (string.IsNullOrWhiteSpace(tempString))
                {
                    RequestJSApiTicket();
                }
                else
                {
                    WeChat_JSApiTicket ticket = helper.GetObject(new WeChat_JSApiTicket());
                    if ((DateTime.Now - ticket.get_time).TotalSeconds > ticket.expires_in)
                    {
                        RequestJSApiTicket();
                    }
                }
            }
            else
            {
                RequestJSApiTicket();
            }
            return helper.GetObject(new WeChat_JSApiTicket()).ticket;
        }
        private void RequestJSApiTicket()
        {
            string accesstoken = GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + accesstoken + "&type=jsapi";
            string response = helper.HttpHelper(url, RequestMethod.GET);
            response = response.Replace("}", ",\"get_time\":\"" + DateTime.Now.ToString() + "\"}");
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/wc_jsapi_ticket.json");
            helper.WriteToFile(response, path);
        }
        #endregion

        #region 支付
        public string GetPrePaySign(WeChat_UnifiedOrderRequest unifiedOrder)
        {
            WeChat_Parameters wechatparameters = helper.GetWeChatParameters();
            List<string> items = new List<string>();
            var properties = unifiedOrder.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name != "sign")
                {
                    var value = property.GetValue(unifiedOrder);
                    if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        items.Add(string.Format("{0}={1}", property.Name, value));
                }
            }
            items.Sort();
            string str1 = string.Join("&", items.ToArray());
            string str2 = str1 + "&key=" + wechatparameters.mch_key;
            string sign = helper.GetMD5(str2).ToUpper();
            return sign;
        }
        public WeChat_PayConfig GetPayConfig(WeChat_UnifiedOrderRequest unifiedOrder)
        {
            WeChat_Parameters wechatparameters = helper.GetWeChatParameters();
            WeChat_PayConfig payconfig = new WeChat_PayConfig();
            payconfig.appId = wechatparameters.AppID;
            payconfig.nonceStr = helper.GetNonceStr();
            payconfig.package = GetPrePayId(unifiedOrder);
            payconfig.signType = WeChat_SignType.MD5.ToString();
            payconfig.timeStamp = helper.GetTimeStamp(DateTime.Now).ToString();
            payconfig.paySign = GetPaySign(payconfig);
            return payconfig;
        }
        public WeChat_PayResult GetPayResult()
        {
            using (StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                string callbackstr = reader.ReadToEnd();
                WeChat_PayCallBack callback = helper.GetObjectByXmlString(new WeChat_PayCallBack(), callbackstr);
                if (callback.result_code == "SUCCESS")
                {
                    return WeChat_PayResult.SUCCESS;
                }
                else
                {
                    return WeChat_PayResult.FAIL;
                }
            }
        }
        private string GetPrePayId(WeChat_UnifiedOrderRequest unifiedOrder)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            string str = helper.ObjectToXmlSrting(unifiedOrder);
            string response = helper.HttpHelper(url, RequestMethod.POST, str);
            return helper.GetObjectByXmlString(new WeChat_UnifiedOrderResponse(), response).prepay_id;
        }
        private string GetPaySign(WeChat_PayConfig config)
        {
            WeChat_Parameters wechatparameters = helper.GetWeChatParameters();
            string[] array = { "appId=" + wechatparameters.AppID, "timeStamp=" + config.timeStamp, "nonceStr=" + config.nonceStr, "package=" + config.package, "signType=" + config.signType };
            Array.Sort(array);
            string str = string.Join("&", array) + "&key=" + wechatparameters.mch_key;
            string sign = helper.GetMD5(str);
            return sign;
        }
        #endregion

        #region 扫码一键关注
        public string GetTempSceneQRUrl(int sceneId, int expire_seconds = 2592000)
        {
            object sceneQR = new
            {
                expire_seconds = expire_seconds,
                action_name = WeChat_SceneQRType.QR_SCENE.ToString(),
                action_info = new
                {
                    scene = new
                    {
                        scene_id = sceneId,
                    },
                },
            };
            string ticket = RequestQRTicket(sceneQR);
            return RequestQRUrl(ticket);
        }
        public string GetPermanentSceneQRUrl(int sceneId)
        {
            object sceneQR = new
            {
                action_name = WeChat_SceneQRType.QR_LIMIT_SCENE.ToString(),
                action_info = new
                {
                    scene = new
                    {
                        scene_id = sceneId,
                    },
                },
            };
            string ticket = RequestQRTicket(sceneQR);
            return RequestQRUrl(ticket);
        }
        public string GetPermanentSceneQRUrl(string sceneStr)
        {
            object sceneQR = new
            {
                action_name = WeChat_SceneQRType.QR_SCENE.ToString(),
                action_info = new
                {
                    scene = new
                    {
                        scene_str = sceneStr,
                    },
                },
            };
            string ticket = RequestQRTicket(sceneQR);
            return RequestQRUrl(ticket);
        }
        public WeChat_SceneQRCallBack GetSceneQRCallBack()
        {
            using (StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                string callbackstr = reader.ReadToEnd();
                WeChat_SceneQRCallBack callback = helper.GetObjectByXmlString(new WeChat_SceneQRCallBack(), callbackstr);
                return callback;
            }
        }
        public string GetSceneQRKey(WeChat_SceneQRCallBack callback)
        {
            if (callback.Event == WeChat_SceneQREventType.SCAN.ToString())
            {
                return callback.EventKey;
            }
            else if (callback.Event == WeChat_SceneQREventType.subscribe.ToString())
            {
                return callback.EventKey.Substring(8);
            }
            else
            {
                return "error";
            }
        }
        private string RequestQRTicket(object qrObj)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + GetAccessToken();
            string requestStr = helper.ObjectToJsonSrting(qrObj);
            string response = helper.HttpHelper(url, RequestMethod.POST, requestStr);
            return helper.GetObject(new WeChat_SceneQRTicket(), response).ticket;
        }
        private string RequestQRUrl(string ticket)
        {
            string url = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + HttpUtility.UrlEncode(ticket);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = RequestMethod.GET.ToString();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.ResponseUri.AbsoluteUri;
        }
        #endregion

        #region 发送模板消息
        public void SendTemplateMessage(WeChat_TemplateMessage msg)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + GetAccessToken();
            string requestStr = helper.ObjectToJsonSrting(msg);
            helper.HttpHelper(url, RequestMethod.POST, requestStr);
        }
        #endregion
    }
}
