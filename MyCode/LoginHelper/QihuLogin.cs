using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoginHelper
{
   public class QihuLogin
    {
        Helper helper = new Helper();
        public string RequestCodeUrl(string unEncodedRediretUri, string state, Qihu_AuthScopes scope)
        {
            Qihu_Parameters parameters = helper.GetQihuParameters();
            return "https://openapi.360.cn/oauth2/authorize?client_id=" + parameters.AppKey + "&response_type=code&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&scope=" + scope.ToString() + "&display=default";
        }
        public Qihu_UserInfo GetUserInfo(string unEncodedRediretUri)
        {
            string accessToken = GetAccessToken(unEncodedRediretUri);
            string url = "https://openapi.360.cn/user/me?access_token=" + accessToken;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new Qihu_UserInfo(), response);
        }
        public Qihu_UserInfo GetUserInfo(string unEncodedRediretUri, Qihu_InfoFields[] fields)
        {
            string field = "";
            if (fields.Length > 0)
            {
                string fieldStr = string.Join(",", fields);
                field = "&fields=" + fieldStr;
            }
            string accessToken = GetAccessToken(unEncodedRediretUri);
            string url = "https://openapi.360.cn/user/me?access_token=" + accessToken + field;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new Qihu_UserInfo(), response);
        }
        private Qihu_CodeModel GetCode()
        {
            Qihu_CodeModel codemodel = new Qihu_CodeModel();
            codemodel.code = HttpContext.Current.Request["code"];
            codemodel.state = HttpContext.Current.Request["state"];
            return codemodel;
        }
        private string GetAccessToken(string unEncodedRediretUri)
        {
            Qihu_Parameters parameters = helper.GetQihuParameters();
            string code = GetCode().code;
            string url = "https://openapi.360.cn/oauth2/access_token?grant_type=authorization_code&code=" + code + "&client_id=" + parameters.AppKey + "&client_secret=" + parameters.AppSecret + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri);
            string response = helper.HttpHelper(url, RequestMethod.POST);
            return helper.GetObject(new Qihu_AccessToken(), response).access_token;
        }
    }
}
