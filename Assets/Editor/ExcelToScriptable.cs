using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class ExcelToScriptable : EditorWindow
{
    string cvs = "";
    string folder = "";
    string __GUID = "";
    string __name = "";
    string defaultPath = System.IO.Directory.GetCurrentDirectory() + "/Tools/CVS/";
    string __process = "D:/MiniGame/MiniGame/Tools/ExcelScriptable/ExcelScriptable/bin/Release/ExcelScriptable.exe";

    [MenuItem("MyTools/ExcelToScriptable")]   
    public static void ShowWindow()
    {
        ExcelToScriptable ets = (ExcelToScriptable)EditorWindow.GetWindow(typeof(ExcelToScriptable));
        ets.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.Label("Input SCV Name");
        cvs = GUILayout.TextField(cvs, 1000);

        GUILayout.Label("Input Save Folder");
        folder = GUILayout.TextField(folder, 1000);

        GUILayout.Label("Input GUID");
        __GUID = GUILayout.TextField(__GUID, 1000);

        GUILayout.Label("Input File Name");
        __name = GUILayout.TextField(__name, 1000);

        if (GUILayout.Button("START CONVERT"))
        {
            Process.Start(__process,defaultPath + cvs + " " + folder + " " + __GUID);
        }

        GUILayout.EndVertical();
    }
}
