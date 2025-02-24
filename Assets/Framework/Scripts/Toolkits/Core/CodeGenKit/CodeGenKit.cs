// ------------------------------------------------------------
// @file       CodeGen.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 22:02:17
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.CodeGenKit
{
    using System.Diagnostics;
    using UnityEngine;
    using Debug = UnityEngine.Debug;

    public class CodeGenKit
    {
        public static readonly string CODE_TEMPLATE_PATH = "Assets/Framework/Scripts/Toolkits/Core/CodeGenKit/CodeTemplate.cs";
        
        public static readonly string PIPE_LINE_PATH = "Assets/Framework/Scripts/Toolkits/Core/CodeGenKit/CodeGenKitPipeline.asset";

        public static void OpenFile(string path)
        {
            // 获取脚本文件的完整路径
            string fullPath = Application.dataPath.Replace("Assets", "") + path;

            // 检查文件是否存在
            if (!System.IO.File.Exists(fullPath))
            {
                Debug.LogError("Script file not found: " + fullPath);
                return;
            }

            // 启动外部编辑器打开文件
            try
            {
                Process.Start(fullPath);
                Debug.Log("Opened script file: " + fullPath);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to open script file: " + e.Message);
            }
        }
    }
}