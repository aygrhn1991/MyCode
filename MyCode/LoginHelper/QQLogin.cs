using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoginHelper
{
   public class QQLogin
    {
        Helper helper = new Helper();
        public string RequestCodeUrl(string unEncodedRediretUri, string state, QQ_AuthScopes[] scopes)
        {
            string scope = "";
            if (scopes.Length > 0)
            {
                string scopeStr = string.Join(",", scopes);
                scope = "&scope=" + scopeStr;
            }
            QQ_Parameters parameters = helper.GetQQParameters();
            return "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=" + parameters.APPID + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&state=" + state + scope;
        }
        public string GetAccessToken(string unEncodedRediretUri)
        {
            string code = GetCode().code;
            QQ_Parameters parameters = helper.GetQQParameters();
            string url = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id=" + parameters.APPID + "&code=" + code + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&client_secret=" + parameters.APPKEY;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            NameValueCollection keyValues = new NameValueCollection();
            string[] pairs = response.Split('&');
            int tempIndex;
            foreach (string pair in pairs)
            {
                tempIndex = pair.IndexOf('=');
                keyValues.Add(pair.Substring(0, tempIndex), pair.Substring(tempIndex + 1));
            }
            QQ_AccessToken accessToken = new QQ_AccessToken();
            accessToken.access_token = keyValues["access_token"];
            accessToken.expires_in = keyValues["expires_in"];
            accessToken.refresh_token = keyValues["refresh_token"];
            return accessToken.access_token;
        }
        public string GetOpenId(string accessToken)
        {
            string url = "https://graph.qq.com/oauth2.0/me?access_token=" + accessToken;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            response = response.Replace("callback(", "").Replace(");", "");
            string openid = helper.GetObject(new QQ_OpenIdModel(), response).openid;
            return openid;
        }
        public QQ_UserInfo GetUserInfo(string openId, string accessToken)
        {
            QQ_Parameters parameters = helper.GetQQParameters();
            string url = "https://graph.qq.com/user/get_user_info?access_token=" + accessToken + "&oauth_consumer_key=" + parameters.APPID + "&openid=" + openId;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new QQ_UserInfo(), response);
        }
        private QQ_CodeModel GetCode()
        {
            QQ_CodeModel codemodel = new QQ_CodeModel();
            codemodel.code = HttpContext.Current.Request["code"];
            codemodel.state = HttpContext.Current.Request["state"];
            return codemodel;
        }
    }
}
