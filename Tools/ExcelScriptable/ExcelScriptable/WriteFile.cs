using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExcelScriptable
{
    internal class WriteFile
    {
        private string path = null;
        private object[,] obj = null;
        private string[] subject = null;
        private string GUID = null;
        private string[] name = null;
        StreamWriter[] sw = null;

        public WriteFile(object[,] obj,string path, string GUID)
        {
            Console.WriteLine("Start Write Asset File..");

            this.obj = obj;
            this.path = path;
            this.GUID = GUID;

            sw = new StreamWriter[this.obj.GetLength(0) - 1];
            name = new string[this.obj.GetLength(0) - 1];
            subject = new string[this.obj.GetLength(1)];

            for(int i = 0; i < sw.Length; i++)
            {
                name[i] = "Crystal_" + i.ToString();
                sw[i] = new StreamWriter(path + "\\" + name[i] + ".asset");
            }

            for(int i = 1; i <= subject.Length; i++)
            {
                subject[i - 1] = this.obj[1, i].ToString();
            }
        }

        public bool Run()
        {
            int count = 1;
            try
            {
                for(int i = 0; i < sw.Length; i++)
                {
                    count = 1;
                    sw[i].WriteLine("%YAML 1.1");
                    sw[i].WriteLine("%TAG !u! tag:unity3d.com,2011:");
                    sw[i].WriteLine("--- !u!114 &11400000");
                    sw[i].WriteLine("MonoBehaviour:");
                    sw[i].WriteLine("  m_ObjectHideFlags: 0");
                    sw[i].WriteLine("  m_CorrespondingSourceObject: {fileID: 0}");
                    sw[i].WriteLine("  m_PrefabInstance: {fileID: 0}");
                    sw[i].WriteLine("  m_PrefabAsset: {fileID: 0}");
                    sw[i].WriteLine("  m_GameObject: {fileID: 0}");
                    sw[i].WriteLine("  m_Enabled: 1");
                    sw[i].WriteLine("  m_EditorHideFlags: 0");
                    sw[i].WriteLine("  m_Script: {fileID: 11500000, guid:" + GUID + ", type: 3}");
                    sw[i].WriteLine("  m_Name: " + name[i]);
                    sw[i].WriteLine("  m_EditorClassIdentifier:");

                    for(int j = 0; j < subject.Length; j++)
                    {
                        sw[i].Write("  " + subject[j] + ": ");
                        sw[i].WriteLine("  " + obj[i + 2, count]);
                        count++;
                    }

                    sw[i].Close();
                }
                Console.WriteLine("Complete Write...");

                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
