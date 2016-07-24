using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace LoginHelper
{
    public class Helper
    {
        public Baidu_Parameters GetBaiduParameters()
        {
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/baidu_parameters.json");
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(Baidu_Parameters));
                //QQParameters result = (QQParameters)obj;
                Baidu_Parameters result = obj as Baidu_Parameters;
                return result;
            }
        }
        public Qihu_Parameters GetQihuParameters()
        {
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/qihu_parameters.json");
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(Qihu_Parameters));
                //QQParameters result = (QQParameters)obj;
                Qihu_Parameters result = obj as Qihu_Parameters;
                return result;
            }
        }
        public QQ_Parameters GetQQParameters()
        {
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/qq_parameters.json");
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(QQ_Parameters));
                //QQParameters result = (QQParameters)obj;
                QQ_Parameters result = obj as QQ_Parameters;
                return result;
            }
        }
        public Sina_Parameters GetSinaParameters()
        {
            string path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/sina_parameters.json");
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(Sina_Parameters));
                //QQParameters result = (QQParameters)obj;
                Sina_Parameters result = obj as Sina_Parameters;
                return result;
            }
        }
        public WeChat_Parameters GetWeChatParameters()
        {
            return GetObject(new WeChat_Parameters());
        }
        public string ReadFromFile(string absolutePath)
        {
            using (StreamReader reader = new StreamReader(absolutePath))
            {
                string str = reader.ReadToEnd();
                return str;
            }
        }
        public void WriteToFile(string str, string absolutePath)
        {
            using (StreamWriter writer = new StreamWriter(absolutePath, false))//不存在会自动创建
            {
                writer.Write(str);
                writer.Flush();
            }
        }
        public T GetObject<T>(T model)
        {
            string path;
            if (model is WeChat_AccessToken)
            {
                path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/wc_access_token.json");
            }
            else if (model is WeChat_JSApiTicket)
            {
                path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/wc_jsapi_ticket.json");
            }
            else
            {
                path = HttpContext.Current.Server.MapPath("~/LoginConfigFile/wc_parameters.json");
            }
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(T));
                //T result = obj as T;
                T result = (T)obj;
                return result;
            }
        }
        public T GetObject<T>(T model, string str)
        {
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(T));
                //T result = obj as T;
                T result = (T)obj;
                return result;
            }
        }
        public string HttpHelper(string url, RequestMethod requestMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestMethod.ToString();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    string responseString = streamReader.ReadToEnd();
                    return responseString;
                }
            }
        }
        public string HttpHelper(string url, RequestMethod requestMethod, string requestString)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestMethod.ToString();

            Stream requestStream = request.GetRequestStream();
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestString);
            requestStream.Write(requestBytes, 0, requestBytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    string responseString = streamReader.ReadToEnd();
                    return responseString;
                }
            }
        }
        public string GetMD5(string str)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] inputByte;
            byte[] outputByte;
            inputByte = Encoding.UTF8.GetBytes(str);
            outputByte = MD5.ComputeHash(inputByte);
            return BitConverter.ToString(outputByte).Replace("-", "").ToUpper();
        }
        public string GetNonceStr(int length = 30)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string nonceStr = "";
            for (int i = 0; i < length; i++)
            {
                nonceStr += chars.Substring(random.Next(0, chars.Length - 1), 1);
            }
            return nonceStr;
        }
        public int GetTimeStamp(DateTime time)
        {
            return (int)((time - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        }
        public string ObjectToXmlSrting<T>(T obj)
        {
            List<string> items = new List<string>();
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    items.Add(string.Format("<{0}>{1}</{0}>", property.Name, value));
            }
            items.Sort();
            return "<xml>" + string.Join("", items) + "</xml>";
        }
        public T GetObjectByXmlString<T>(T model, string str)
        {
            string newstr = str.Replace("xml", model.GetType().Name);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(newstr)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    T obj = (T)xmlSerializer.Deserialize(xmlReader);
                    return obj;
                }
            }
        }
        public string ObjectToJsonSrting<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
    public enum RequestMethod
    {
        GET,
        POST,
    }
}
