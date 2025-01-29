// ------------------------------------------------------------
// @file       TxtHelper.cs
// @brief
// @author     zheliku
// @Modified   2024-12-06 23:12:37
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.DataKit
{
    using System;
    using System.IO;
    using FluentAPI;
    using LitJson;
    using UnityEngine;
    
    // 1. 引用命名空间
#if UNITY_WEBGL && !UNITY_EDITOR
    using System.Runtime.InteropServices;
#endif

    public enum JsonType
    {
        /// <summary>
        /// 1. <c>float</c> 序列化时看起来会有一些误差 <br/>
        /// 2. 自定义类需要加上序列化特性 <c>[System.Serializable]</c> <br/>
        /// 3. 想要序列化私有变量，需要加上特性 <c>[SerializeField]</c> <br/>
        /// 4. 不支持字典；序列化数组时需要包裹一层 <br/>
        /// 5. 存储 <c>null</c> 对象不会是 <c>null</c>，而是默认值的数据
        /// </summary>
        JsonUtility,

        /// <summary>
        /// 1. 无需加特性 <c>[System.Serializable]</c> <br/>
        /// 2. 不支持私有变量、但支持字典序列化反序列化 <br/>
        /// 3. 可以直接将数据反序列化为数据集合 <br/>
        /// 4. 反序列化时，自定义类型需要无参构造 <br/>
        /// 5. Json 文档编码格式必须是 UTF-8
        /// </summary>
        LitJson
    }

    /// <summary>
    /// WebGL 端数据存储参考：<see href="https://blog.csdn.net/xiaotaiyang_gege/article/details/135862196"/>
    /// </summary>
    public partial class JsonHelper
    {
        // 2. 加入外部函数声明
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SyncDB();
#endif
        
        public const string EXTENSION = ".json";

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Framework/DataKit/Open Json Folder")]
        public static void OpenJsonSavePath()
        {
            DataKit.JsonSavePath.EnsureDirectoryExist();
            System.Diagnostics.Process.Start("explorer.exe", DataKit.JsonSavePath.ConvertToCurrentEnvironmentPath());
        }
#endif

        /// <summary>
        /// 处理输入路径，使得路径符合 TxtHelper 下的路径
        /// </summary>
        private static string ProcessPath(string path)
        {
            return DataKit.JsonSavePath.CombinePath(path);
        }

        /// <summary>
        /// 将数据转换为 json 文件
        /// </summary>
        /// <param name="data">存储数据，如果是 JsonUtility，则自定义结构需要添加 [Serializable] 特性</param>
        /// <param name="type">序列化方式</param>
        public static string ToJson<TData>(TData data, JsonType type = JsonType.LitJson)
        {
            if (type == JsonType.LitJson)
            {
                RegisterLitJsonExporter();
            }

            var jsonStr = type switch
            {
                JsonType.JsonUtility => JsonUtility.ToJson(data),
                JsonType.LitJson     => JsonMapper.ToJson(data),
                _                    => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            if (type == JsonType.LitJson)
            {
                JsonMapper.UnregisterExporters();
            }

            return jsonStr;
        }

        /// <summary>
        /// 存储数据为 json 文件
        /// </summary>
        /// <param name="filePath">文件路径，可以不写后缀</param>
        /// <param name="data">存储数据，如果是 JsonUtility，则自定义结构需要添加 [Serializable] 特性</param>
        /// <param name="extension">文件扩展名</param>
        /// <param name="type">序列化方式</param>
        public static void Save<TData>(string filePath, TData data, string extension = EXTENSION, JsonType type = JsonType.LitJson)
        {
            string fullPath = ProcessPath(filePath). // 处理输入路径
                ChangeExtension(extension)           // 确保文件路径扩展名为指定格式
               .EnsureDirectoryExist();              // 确保文件所在目录存在

            var jsonStr = ToJson(data, type);

            File.WriteAllText(fullPath, jsonStr);

    	    // 3. 调用外部函数
#if UNITY_WEBGL && !UNITY_EDITOR
    	    SyncDB();
#endif
            
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        /// <summary>
        /// 将 json 格式字符串转换为对应数据
        /// </summary>
        /// <param name="jsonStr">json 字符串</param>
        /// <param name="type">序列化方式</param>
        public static TData FromJson<TData>(string jsonStr, JsonType type = JsonType.LitJson)
        {
            if (type == JsonType.LitJson)
            {
                RegisterLitJsonImporter();
            }

            var obj = type switch
            {
                JsonType.JsonUtility => JsonUtility.FromJson<TData>(jsonStr),
                JsonType.LitJson     => JsonMapper.ToObject<TData>(jsonStr),
                _                    => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            if (type == JsonType.LitJson)
            {
                JsonMapper.UnregisterImporters();
            }

            return obj;
        }

        /// <summary>
        /// 读取 json 文件中的数据
        /// </summary>
        /// <param name="filePath">文件路径，可以不写后缀</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="extension">文件扩展名</param>
        /// <param name="type">序列化方式</param>
        public static TData Load<TData>(string filePath, TData defaultValue = default(TData), string extension = EXTENSION, JsonType type = JsonType.LitJson)
        {
            string fullPath = ProcessPath(filePath). // 处理输入路径
                ChangeExtension(extension);          // 确保文件路径扩展名为指定格式

            if (!File.Exists(fullPath))
            { // 不存在文件，则警告，并返回默认值
                Debug.LogWarning($"JsonHelper: Can't find path \"{fullPath}\"");
                return defaultValue;
            }

            var jsonStr = File.ReadAllText(fullPath);

            return FromJson<TData>(jsonStr, type);
        }
    }

    public static partial class JsonHelper
    {
        /// <summary>
        /// 注册 LitJson 导出器
        /// </summary>
        private static void RegisterLitJsonExporter()
        {
            // 注册 Vector2
            JsonMapper.RegisterExporter<Vector2>((obj, writer) =>
            {
                writer.WriteObjectStart();
                writer.WritePropertyName("x");
                writer.Write(obj.x);
                writer.WritePropertyName("y");
                writer.Write(obj.y);
                writer.WriteObjectEnd();
            });

            // 注册 Vector3
            JsonMapper.RegisterExporter<Vector3>((obj, writer) =>
            {
                writer.WriteObjectStart();
                writer.WritePropertyName("x");
                writer.Write(obj.x);
                writer.WritePropertyName("y");
                writer.Write(obj.y);
                writer.WritePropertyName("z");
                writer.Write(obj.z);
                writer.WriteObjectEnd();
            });

            // 注册 Vector4
            JsonMapper.RegisterExporter<Vector4>((obj, writer) =>
            {
                writer.WriteObjectStart();
                writer.WritePropertyName("x");
                writer.Write(obj.x);
                writer.WritePropertyName("y");
                writer.Write(obj.y);
                writer.WritePropertyName("z");
                writer.Write(obj.z);
                writer.WritePropertyName("w");
                writer.Write(obj.w);
                writer.WriteObjectEnd();
            });
        }

        /// <summary>
        /// 注册 LitJson 导入器
        /// </summary>
        private static void RegisterLitJsonImporter()
        {
            // 注册 Vector2
            JsonMapper.RegisterImporter<JsonData, Vector2>(jsonObj =>
            {
                return new Vector2((float) jsonObj["x"], (float) jsonObj["y"]);
            });

            // 注册 Vector3
            JsonMapper.RegisterImporter<JsonData, Vector3>(jsonObj =>
            {
                return new Vector3((float) jsonObj["x"], (float) jsonObj["y"], (float) jsonObj["z"]);
            });

            // 注册 Vector4
            JsonMapper.RegisterImporter<JsonData, Vector4>(jsonObj =>
            {
                return new Vector4((float) jsonObj["x"], (float) jsonObj["y"], (float) jsonObj["z"], (float) jsonObj["w"]);
            });
        }
    }
}