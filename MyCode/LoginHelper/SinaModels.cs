using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHelper
{
    public class Sina_Parameters
    {
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string Note { get; set; }
    }
    public class Sina_CodeModel
    {
        public string code { get; set; }
        public string state { get; set; }
    }
    public class Sina_AccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string uid { get; set; }
    }
    public class Sina_UserInfo
    {
        public string id { get; set; }
        public string idstr { get; set; }
        public string screen_name { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string profile_image_url { get; set; }
        public string profile_url { get; set; }
        public string domain { get; set; }
        public string weihao { get; set; }
        public string gender { get; set; }
        public string followers_count { get; set; }
        public string friends_count { get; set; }
        public string statuses_count { get; set; }
        public string favourites_count { get; set; }
        public string created_at { get; set; }
        public string following { get; set; }
        public string allow_all_act_msg { get; set; }
        public string geo_enabled { get; set; }
        public string verified { get; set; }
        public string verified_type { get; set; }
        public string remark { get; set; }
        public Sina_NewestMessage status { get; set; }
        public string allow_all_comment { get; set; }
        public string avatar_large { get; set; }
        public string avatar_hd { get; set; }
        public string verified_reason { get; set; }
        public string follow_me { get; set; }
        public string online_status { get; set; }
        public string bi_followers_count { get; set; }
        public string lang { get; set; }
    }
    public class Sina_NewestMessage
    {
        public string created_at { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public string favorited { get; set; }
        public string truncated { get; set; }
        public string in_reply_to_status_id { get; set; }
        public string in_reply_to_user_id { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public string geo { get; set; }
        public string mid { get; set; }
        public string[] annotations { get; set; }
        public string reposts_count { get; set; }
        public string comments_count { get; set; }
    }
    public enum Sina_AuthScopes
    {
        all,
        email,
        direct_messages_write,
        direct_messages_read,
        invitation_write,
        friendships_groups_read,
        friendships_groups_write,
        statuses_to_me_read,
        follow_app_official_microblog,
    }
}
