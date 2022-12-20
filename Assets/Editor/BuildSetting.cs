using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class BuildSetting
{
    [MenuItem("MyTools/Build")]
    public static void MyBuild()
    {
        string desktop = "C:/MetaTrend_1";
        string buildPath = desktop + "/MiniGame_1/";
        string[] scene = { "Assets/Scenes/SampleScene.unity" };
        string folderName = "";

        FileInfo buildInfo = new FileInfo(buildPath);

        if(buildInfo.Exists == false)
        {
            Directory.CreateDirectory(buildInfo.FullName);
        }

        folderName = buildPath + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "/";
        FileInfo folder = new FileInfo(folderName);

        if (folder.Exists == false)
        {
            Directory.CreateDirectory(folder.FullName);
        }


        BuildPipeline.BuildPlayer(scene, folderName + "build.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}
