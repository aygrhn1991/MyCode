using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTools.Excel;

namespace WebTools.Controllers
{
    public class ExcelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExportExcel()
        {
            List<ExcelExample> list = new List<ExcelExample>();
            for (int i = 0; i < 20; i++)
            {
                ExcelExample item = new ExcelExample();
                item.Id = i;
                item.姓名 = "姓名" + i;
                item.性别 = i < 10 ? "0" : i >= 10 && i < 15 ? "1" : "2";
                list.Add(item);
            }
            string path = HttpContext.Server.MapPath("~/Attachments/aaa.xls");

            var data = ExcelMethod.IListOut(list);
            ExcelHelper.DataSetToExcel(data, path);

            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            System.IO.File.Delete(path);

            Response.ContentType = "application/ms-excel";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("aaa.xls"));
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.Flush();
            Response.End();
            return null;
        }
    }
    public class ExcelExample
    {
        public int Id { get; set; }
        public string 姓名 { get; set; }
        private string _性别;
        public string 性别
        {
            get
            {
                return _性别;
            }
            set
            {
                if (value == "0")
                {
                    _性别 = "男";
                }
                else if (value == "1")
                {
                    _性别 = "女";
                }
                else
                {
                    _性别 = "未填写";
                }
            }
        }
    }

}