using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTest.WeChatQRLogin
{
    public static class WeChatQRLoginExtension
    {
        //可以模仿此处将微信授权写入中间件，创建UseWeChatLoginApp应用
        public static IAppBuilder UseWeChatQRLoginApp(this IAppBuilder builder)
        {
            return builder.Use<WeChatQRLoginMiddleware>();
            //UseXXX可以带多个参数，对应中间件构造函数中的第2、3、....参数;
        }

        //public static IAppBuilder UseWeChatLoginApp(this IAppBuilder builder)
        //{
        //    return builder.Use<WeChatLoginMiddleware>();
        //    //UseXXX可以带多个参数，对应中间件构造函数中的第2、3、....参数;
        //}
    }
}