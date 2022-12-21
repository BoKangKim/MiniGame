using System;
using System.IO;
using UnityEngine;

namespace SaveLoad 
{
    [System.Serializable]
    public struct SaveData
    {
        public int level;
        public int gold;
        public int pick;
    }

    public class Save
    {
        private StreamWriter sw = null;
        string path = Directory.GetCurrentDirectory() + "/SaveData/";
        string sData = null;
        SaveData data;

        public Save()
        {
            FileInfo pathInfo = new FileInfo(path);

            if(pathInfo.Exists == false)
            {
                Directory.CreateDirectory(path);
            }

        }

        public void Start(int level, int gold, int pick)
        {
            data.level = level;
            data.gold = gold;
            data.pick = pick;

            sData = path + "Data.json";

            sw = new StreamWriter(sData);

            string json = JsonUtility.ToJson(data);

            Debug.Log(json);

            sw.Write(json);

            sw.Close();
        }
    }

    public class Load 
    { 
        private StreamReader sr = null;
        string path = Directory.GetCurrentDirectory() + "/SaveData/Data.json";
        SaveData load;
        FileInfo loadInfo = null;

        public Load()
        {
            loadInfo = new FileInfo(path);
            if(loadInfo.Exists == false)
            {
                Debug.Log("Not Found SaveData");
                return;
            }

            sr = new StreamReader(path);
        }

        public SaveData Start()
        {
            if (loadInfo.Exists == false)
            {
                Debug.Log("Not Found SaveData");
                return load;
            }

            string json = sr.ReadToEnd();

            Debug.Log(json);

            load = JsonUtility.FromJson<SaveData>(json);


            sr.Close();
            return load;
        }
    }

}

