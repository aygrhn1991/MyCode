using LoginHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        WeChatLogin wechat = new WeChatLogin();
        QQLogin qq = new QQLogin();
        SinaLogin sina = new SinaLogin();
        BaiduLogin baidu = new BaiduLogin();
        QihuLogin qihu = new QihuLogin();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Baidu()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/BaiduCallBack";
            string red_url = baidu.RequestCodeUrl(url, Baidu_AuthScopes.basic);
            return Redirect(red_url);
        }
        public ActionResult BaiduCallBack()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/BaiduCallBack";
            Baidu_UserInfo info = baidu.GetUserInfo(url);
            ViewBag.img = info.small_portrait;
            ViewBag.name = info.username;
            return View();
        }
        public ActionResult Qihu()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/QihuCallBack";
            string red_url = qihu.RequestCodeUrl(url, "aaa", Qihu_AuthScopes.basic);
            return Redirect(red_url);
        }
        public ActionResult QihuCallBack()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/QihuCallBack";
            Qihu_UserInfo info = qihu.GetUserInfo(url);
            ViewBag.img = info.avatar;
            ViewBag.name = info.name;
            return View();
        }
        public ActionResult QQ()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/QQCallBack";
            string red_url = qq.RequestCodeUrl(url, "aaa", new QQ_AuthScopes[] { });
            return Redirect(red_url);
        }
        public ActionResult QQCallBack()
        {
            string url = HttpContext.Request.Url.AbsoluteUri;
            string token = qq.GetAccessToken(url);
            string openid = qq.GetOpenId(token);
            var sss = qq.GetUserInfo(openid, token);
            ViewBag.img = sss.figureurl_1;
            ViewBag.name = sss.nickname;
            return View();
        }
        public ActionResult Sina()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/SinaCallBack";
            string red_url = sina.RequestCodeUrl(url, "aaa", new Sina_AuthScopes[] { });
            return Redirect(red_url);
        }
        public ActionResult SinaCallBack()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/SinaCallBack";
            Sina_AccessToken token = sina.GetAccessToken(url);
            string uid = token.uid;
            var sss = sina.GetUserInfo(uid, token.access_token);
            ViewBag.img = sss.avatar_large;
            ViewBag.name = sss.screen_name;
            return View();
        }
        public ActionResult WeChat()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/WeChatCallBack";
            string red_url = wechat.RequestCodeUrl(url, "aaa", WeChat_snsapi_scope.snsapi_userinfo);
            return Redirect(red_url);
        }
        public ActionResult WeChatCallBack()
        {
            ViewBag.code = HttpContext.Request["code"];
            return View();
        }
        public ActionResult WeCahtQR()
        {
            return View();
        }
        public ActionResult CheckConfiguration()
        {
            string str = wechat.CheckConfiguration();
            if (str == "error")
            {
                return null;
            }
            else
            {
                HttpContext.Response.Write(str);
                return null;
            }
        }
    }
}