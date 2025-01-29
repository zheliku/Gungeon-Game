// ------------------------------------------------------------
// @file       TxtHelper.cs
// @brief
// @author     zheliku
// @Modified   2024-12-06 23:12:37
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.DataKit
{
    using System.IO;
    using FluentAPI;
    using UnityEngine;
    
    // 1. 引用命名空间
#if UNITY_WEBGL && !UNITY_EDITOR
    using System.Runtime.InteropServices;
#endif

    /// <summary>
    /// WebGL 端数据存储参考：<see href="https://blog.csdn.net/xiaotaiyang_gege/article/details/135862196"/>
    /// </summary>
    public class TxtHelper
    {
        // 2. 加入外部函数声明
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SyncDB();
#endif
        
        public const string EXTENSION = ".txt";

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Framework/DataKit/Open Txt Folder")]
        public static void OpenTxtSavePath()
        {
            DataKit.TxtSavePath.EnsureDirectoryExist();
            System.Diagnostics.Process.Start("explorer.exe", DataKit.TxtSavePath.ConvertToCurrentEnvironmentPath());
        }
#endif

        /// <summary>
        /// 处理输入路径，使得路径符合 TxtHelper 下的路径
        /// </summary>
        private static string ProcessPath(string path)
        {
            return DataKit.TxtSavePath.CombinePath(path);
        }

        /// <summary>
        /// 存储数据为 txt 文件
        /// </summary>
        /// <param name="filePath">文件路径，可以不写后缀</param>
        /// <param name="content">内容</param>
        /// <param name="extension">文件扩展名</param>
        public static void Save(string filePath, string content, string extension = EXTENSION)
        {
            string fullPath = ProcessPath(filePath). // 处理输入路径
                ChangeExtension(extension)           // 确保文件路径扩展名为指定格式
               .EnsureDirectoryExist();              // 确保文件所在目录存在

            File.WriteAllText(fullPath, content);

    	    // 3. 调用外部函数
#if UNITY_WEBGL && !UNITY_EDITOR
    	    SyncDB();
#endif
            
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        /// <summary>
        /// 存储数据为 txt 文件
        /// </summary>
        /// <param name="filePath">文件绝对路径，可以不写后缀</param>
        /// <param name="content">内容</param>
        /// <param name="extension">文件扩展名</param>
        public static void SaveAbsolute(string filePath, string content, string extension = EXTENSION)
        {
            string fullPath = filePath.ChangeExtension(extension) // 确保文件路径扩展名为指定格式
               .EnsureDirectoryExist();                           // 确保文件所在目录存在

            File.WriteAllText(fullPath, content);

    	    // 3. 调用外部函数
#if UNITY_WEBGL && !UNITY_EDITOR
    	    SyncDB();
#endif
            
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        /// <summary>
        /// 读取 txt 文件中的数据
        /// </summary>
        /// <param name="filePath">文件路径，可以不写后缀</param>
        /// <param name="extension">文件扩展名</param>
        public static string Load(string filePath, string extension = EXTENSION)
        {
            string fullPath = ProcessPath(filePath). // 处理输入路径
                ChangeExtension(extension);          // 确保文件路径扩展名为指定格式

            if (!File.Exists(fullPath))
            { // 不存在文件，则警告，并返回默认值
                Debug.LogWarning($"TxtHelper: Can't find path \"{fullPath}\"");
                return null;
            }

            return File.ReadAllText(fullPath);
        }

        /// <summary>
        /// 读取 txt 文件中的数据
        /// </summary>
        /// <param name="filePath">文件绝对路径，可以不写后缀</param>
        /// <param name="extension">文件扩展名</param>
        public static string LoadAbsolute(string filePath, string extension = EXTENSION)
        {
            string fullPath = filePath.ChangeExtension(extension); // 确保文件路径扩展名为指定格式

            if (!File.Exists(fullPath))
            { // 不存在文件，则警告，并返回默认值
                Debug.LogWarning($"TxtHelper: Can't find path \"{fullPath}\"");
                return null;
            }

            return File.ReadAllText(fullPath);
        }
    }
}