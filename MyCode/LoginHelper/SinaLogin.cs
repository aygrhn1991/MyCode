using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoginHelper
{
   public class SinaLogin
    {
        Helper helper = new Helper();
        public string RequestCodeUrl(string unEncodedRediretUri, string state, Sina_AuthScopes[] scopes)
        {
            string scope = "";
            if (scopes.Length > 0)
            {
                string scopeStr = string.Join(",", scopes);
                scope = "&scope=" + scopeStr;
            }
            Sina_Parameters parameters = helper.GetSinaParameters();
            return "https://api.weibo.com/oauth2/authorize?client_id=" + parameters.AppKey + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&state=" + state + scope + "&response_type=code";
        }
        public Sina_AccessToken GetAccessToken(string unEncodedRediretUri)
        {
            Sina_Parameters parameters = helper.GetSinaParameters();
            string code = GetCode().code;
            string url = "https://api.weibo.com/oauth2/access_token?client_id=" + parameters.AppKey + "&client_secret=" + parameters.AppSecret + "&grant_type=authorization_code&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&code=" + code;
            string response = helper.HttpHelper(url, RequestMethod.POST);
            return helper.GetObject(new Sina_AccessToken(), response);
        }
        public string GetUId(string unEncodedRediretUri)
        {
            return GetAccessToken(unEncodedRediretUri).uid;
        }
        public Sina_UserInfo GetUserInfo(string uid, string accessToken)
        {
            string url = "https://api.weibo.com/2/users/show.json?access_token=" + accessToken + "&uid=" + uid;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new Sina_UserInfo(), response);
        }
        private Sina_CodeModel GetCode()
        {
            Sina_CodeModel codemodel = new Sina_CodeModel();
            codemodel.code = HttpContext.Current.Request["code"];
            codemodel.state = HttpContext.Current.Request["state"];
            return codemodel;
        }
    }
}
