using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Office.Interop.Excel;

namespace ExcelScriptable
{
    internal class ReadFile
    {
        private string path = null;
        private Application app = null;
        private Workbook wb = null;
        private Worksheet ws = null;

        private int rows = 0;
        private int cols = 0;

        private object[,] obj = null;
        public object[,] OBJ { get { return obj; } private set { } }
        public ReadFile(string path)
        {
            this.path = path;
            FileInfo excel = new FileInfo(path);
            if (excel.Exists == false)
            {
                Console.WriteLine(path + " is not exists");
                return;
            }
            else
            {
                Console.WriteLine("Checked Path : " + path + " is exists...");
            }

            app = new Application();
            wb = app.Workbooks.Open(Filename: @path);
            ws = wb.Worksheets.Item[2] as Worksheet;
        }

        public bool Run()
        {
            try
            {
                Range range = ws.UsedRange;

                rows = range.Rows.Count;
                cols = range.Columns.Count;
                Console.WriteLine(rows);
                Console.WriteLine(cols);

                Console.WriteLine("Copy Values ....");
                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= cols; j++)
                    {
                        obj = range.Value2;
                        Console.WriteLine(obj[i, j]);
                    }
                }
                Console.WriteLine("Copy Complete ....");

                app.Workbooks.Close();
                app.Quit();

                ReleaseExcelObject(wb);
                ReleaseExcelObject(app);

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                app.Workbooks.Close();
                app.Quit();

                ReleaseExcelObject(wb);
                ReleaseExcelObject(app);
                return false;
            }
            
        }

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
