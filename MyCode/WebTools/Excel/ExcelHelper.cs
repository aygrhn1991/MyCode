using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace WebTools.Excel
{
    public class ExcelHelper
    {
        public static void DataSetToExcel(DataTable data, string absolutePath)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel._Workbook workBook;
            Microsoft.Office.Interop.Excel._Worksheet workSheet;
            object misValue = System.Reflection.Missing.Value;
            excel = new Microsoft.Office.Interop.Excel.Application();
            workBook = excel.Workbooks.Add(misValue);
            workSheet = (Microsoft.Office.Interop.Excel._Worksheet)workBook.ActiveSheet;
            int rowIndex = 1;
            int colIndex = 0;
            //取得标题
            foreach (DataColumn col in data.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;

                workSheet.Range[excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            }
            //取得表格中的数据
            foreach (DataRow row in data.Rows)
            {
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in data.Columns)
                {
                    colIndex++;
                    excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString().Trim();
                    //设置表格内容居中对齐
                    //workSheet.get_Range(excel.Cells[rowIndex, colIndex], 
                    workSheet.Range[excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                }
            }
            excel.Visible = false;
            workBook.SaveAs(absolutePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            data = null;
            workBook.Close(true, misValue, misValue);
            excel.Quit();
            //调用kill当前excel进程
            ExcelMethod.Kill(excel);
            releaseObject(workSheet);
            releaseObject(workBook);
            releaseObject(excel);
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
    public class ExcelMethod
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
        {
            try
            {
                IntPtr t = new IntPtr(excel.Hwnd);
                int k = 0;
                GetWindowThreadProcessId(t, out k);
                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
                p.Kill();
            }
            catch { }
        }
        public static DataTable IListOut<T>(IList<T> list)
        {
            DataTable dt = new DataTable();
            //此处遍历IList的结构并建立同样的DataTable
            System.Reflection.PropertyInfo[] p = list[0].GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in p)
            {
                dt.Columns.Add(pi.Name, System.Type.GetType(pi.PropertyType.ToString()));
            }
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new ArrayList();
                //将IList中的一条记录写入ArrayList
                foreach (System.Reflection.PropertyInfo pi in p)
                {
                    object oo = pi.GetValue(list[i], null);
                    tempList.Add(oo);
                }
                object[] items = new object[p.Length];
                //遍历ArrayList向object[]里放数据
                for (int j = 0; j < tempList.Count; j++)
                {
                    items.SetValue(tempList[j], j);
                }
                //将object[]的内容放入DataTable
                dt.LoadDataRow(items, true);
            }
            //返回DataTable
            return dt;
        }
    }
}