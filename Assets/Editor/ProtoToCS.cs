using System;
using UnityEngine;
using UnityEditor;

public class ProtoToCS : MonoBehaviour
{
    [MenuItem("Tools/Proto2CS")]
    private static void ShowWindow()
    {
        //这个函数主要作用是执行编写好的一个批处理文件

        string currentPath = Environment.SystemDirectory;
        UnityEngine.Debug.LogError(currentPath);
        //string batPath = "E:/unity-2020/2020.3.8f1c1/Editor/project_0/Proto2CSFile/build_proto.bat";//批处理文件地址
        //Process pro = new Process();


        //FileInfo file = new FileInfo(batPath);
        //pro.StartInfo.WorkingDirectory = file.Directory.FullName;
        //pro.StartInfo.FileName = batPath;
        //pro.StartInfo.CreateNoWindow = false;
        //pro.Start();
        //pro.WaitForExit();
    }
}
