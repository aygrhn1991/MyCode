using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHelper
{
    public class Baidu_Parameters
    {
        public string APIKey { get; set; }
        public string SecretKey { get; set; }
        public string Note { get; set; }
    }
    public class Baidu_AccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string session_key { get; set; }
        public string session_secret { get; set; }
    }
    public class Baidu_UserInfo
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string realname { get; set; }
        private string _portrait;
        public string portrait
        {
            get
            {
                return _portrait;
            }
            set
            {
                _portrait = value;
            }
        }
        public string small_portrait
        {
            get
            {
                return "http://tb.himg.baidu.com/sys/portraitn/item/" + _portrait;
            }
            set
            {
                _portrait = value;
            }
        }
        public string large_portrait
        {
            get
            {
                return "http://tb.himg.baidu.com/sys/portrait/item/" + _portrait;
            }
            set
            {
                _portrait = value;
            }
        }
        public string userdetail { get; set; }
        public string birthday { get; set; }
        public string marriage { get; set; }
        public string sex { get; set; }
        public string blood { get; set; }
        public string figure { get; set; }
        public string constellation { get; set; }
        public string education { get; set; }
        public string trade { get; set; }
        public string job { get; set; }
    }
    public enum Baidu_AuthScopes
    {
        basic,
        super_msg,
        netdisk,
    }
}
