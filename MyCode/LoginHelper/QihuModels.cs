using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHelper
{
    public class Qihu_Parameters
    {
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string Note { get; set; }
    }
    public class Qihu_CodeModel
    {
        public string code { get; set; }
        public string state { get; set; }
    }
    public class Qihu_AccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
    public class Qihu_UserInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string sex { get; set; }
        public string area { get; set; }
    }
    public enum Qihu_AuthScopes
    {
        basic,
    }
    public enum Qihu_InfoFields
    {
        id,
        name,
        avatar,
        sex,
        area,
    }
}
