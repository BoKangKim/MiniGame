using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildSetting
{
    [MenuItem("MyTools/Build")]
    public static void MyBuild()
    {
        string desktop = "C:/Users/User/Desktop";
        string buildPath = desktop + "/Build/";
        string[] scene = { "Assets/Scenes/SampleScene.unity" };

        FileInfo buildInfo = new FileInfo(buildPath);

        if(buildInfo.Exists == false)
        {
            Directory.CreateDirectory(buildPath);
        }


        BuildPipeline.BuildPlayer(scene, buildPath + "build.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}
