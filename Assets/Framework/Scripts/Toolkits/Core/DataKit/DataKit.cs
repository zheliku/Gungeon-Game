// ------------------------------------------------------------
// @file       DataKit.cs
// @brief
// @author     zheliku
// @Modified   2024-12-06 23:12:44
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.DataKit
{
    using System.Collections.Generic;
    using FluentAPI;
    using UnityEngine;

    public class DataKit
    {
        public static string ProjectPath { get => Application.dataPath; }

        /// <summary>
        /// Json 存储路径
        /// </summary>
        public static string JsonSavePath { get; set; } = Application.persistentDataPath.CombinePath("Json");

        /// <summary>
        /// Txt 存储路径
        /// </summary>
        public static string TxtSavePath { get; set; } = Application.persistentDataPath.CombinePath("Txt");

        /// <summary>
        /// Binary 存储路径
        /// </summary>

        // public static string BinarySavePath { get; set; } = Application.persistentDataPath.CombinePath("Binary");
        public static string BinarySavePath { get; set; } = Application.persistentDataPath + "/Binary";

        /// <summary>
        /// Excel 存储路径
        /// </summary>
        public static string ExcelSavePath { get; set; } = Application.streamingAssetsPath.CombinePath("Excel");

        public static void SaveTxt(string filePath, string content, string extension = TxtHelper.EXTENSION)
        {
            TxtHelper.Save(filePath, content, extension);
        }

        public static void SaveTxtAbsolute(string filePath, string content, string extension = TxtHelper.EXTENSION)
        {
            TxtHelper.SaveAbsolute(filePath, content, extension);
        }

        public static string LoadTxt(string filePath, string extension = TxtHelper.EXTENSION)
        {
            return TxtHelper.Load(filePath, extension);
        }

        public static string LoadTxtAbsolute(string filePath, string extension = TxtHelper.EXTENSION)
        {
            return TxtHelper.LoadAbsolute(filePath, extension);
        }

        public static string ToJson<TData>(TData data, JsonType type = JsonType.LitJson)
        {
            return JsonHelper.ToJson(data, type);
        }

        public static void SaveJson<TData>(string filePath, TData data, string extension = JsonHelper.EXTENSION, JsonType type = JsonType.LitJson)
        {
            JsonHelper.Save<TData>(filePath, data, extension, type);
        }

        public static TData FromJson<TData>(string jsonStr, JsonType type = JsonType.LitJson)
        {
            return JsonHelper.FromJson<TData>(jsonStr, type);
        }

        public static TData LoadJson<TData>(string filePath, TData defaultValue = default(TData), string extension = JsonHelper.EXTENSION, JsonType type = JsonType.LitJson)
        {
            return JsonHelper.Load<TData>(filePath, defaultValue, extension, type);
        }

        public static ExcelSheet LoadExcel(string filePath, string sheetName = null, ExcelFormat format = ExcelFormat.Xlsx)
        {
            return new ExcelSheet(filePath, sheetName, format);
        }

        public static TData LoadBinary<TData>(string filePath, string extension = BinaryHelper.EXTENSION)
        {
            return BinaryHelper.Load<TData>(filePath, extension);
        }

        public static void SaveBinary<TData>(string filePath, TData data, string extension = BinaryHelper.EXTENSION)
        {
            BinaryHelper.Save<TData>(filePath, data, extension);
        }
    }
}