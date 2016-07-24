using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHelper
{
    public class QQ_Parameters
    {
        public string APPID { get; set; }
        public string APPKEY { get; set; }
        public string Note { get; set; }
    }
    public class QQ_CodeModel
    {
        public string code { get; set; }
        public string state { get; set; }
    }
    public class QQ_AccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }
    public class QQ_OpenIdModel
    {
        public string client_id { get; set; }
        public string openid { get; set; }
    }
    public class QQ_UserInfo
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string nickname { get; set; }
        public string figureurl { get; set; }
        public string figureurl_1 { get; set; }
        public string figureurl_2 { get; set; }
        public string figureurl_qq_1 { get; set; }
        public string figureurl_qq_2 { get; set; }
        public string gender { get; set; }
        public string is_yellow_vip { get; set; }
        public string vip { get; set; }
        public string yellow_vip_level { get; set; }
        public string level { get; set; }
        public string is_yellow_year_vip { get; set; }
    }
    public enum QQ_AuthScopes
    {
        get_user_info,
        get_info,
        add_t,
        del_t,
        add_pic_t,
        get_repost_list,
        get_other_info,
        get_fanslist,
        get_idollist,
        add_idol,
        del_idol,
        //以下需要申请
        get_vip_info,
        get_vip_rich_info,
        list_album,
        upload_pic,
        add_album,
        list_photo,
        get_tenpay_addr,
    }
}
