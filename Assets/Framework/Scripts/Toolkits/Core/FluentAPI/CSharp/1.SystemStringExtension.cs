// ------------------------------------------------------------
// @file       1.SystemStringExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-17 20:10:32
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 针对 System.String 提供的链式扩展，理论上任何集合都可以使用
    /// </summary>
    public static class SystemStringExtension
    {
        /// <summary>
        /// 检测是否为 Null 或 Empty
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(string.Empty.IsNullOrEmpty()); // true
        /// ]]>
        /// </code> </example>
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }
        
        /// <summary>
        /// 检测是否为 Not Null 且 Not Empty
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(""Hello"".IsNotNullAndNotEmpty()); // true
        /// ]]>
        /// </code> </example>
        public static bool IsNotNullAndNotEmpty(this string self)
        {
            return !string.IsNullOrEmpty(self);
        }
        
        /// <summary>
        /// 检测是否为 Null，或去掉两端空格后为 Empty
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(""   "".IsNullOrTrimEmpty()); // true
        /// ]]>
        /// </code> </example>
        public static bool IsNullOrTrimEmpty(this string self)
        {
            return self == null || self.Trim() == string.Empty;
        }
        
        /// <summary>
        /// 检测是否为 Not Null，且去掉两端空格后为 Not Empty
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(""  123  "".IsNotNullAndNotTrimEmpty()); // true
        /// ]]>
        /// </code> </example>
        public static bool IsNotNullAndNotTrimEmpty(this string self)
        {
            return self != null && self.Trim() != string.Empty;
        }
        
        /// <summary>
        /// 字符串分割
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ""1.2.3.4.5"".Split('.').ForEach(str => Debug.Log(str)); // 1 2 3 4 5
        /// ]]>
        /// </code> </example>
        public static string[] Split(this string self, char splitSymbol, StringSplitOptions options = StringSplitOptions.None)
        {
            return self.Split(splitSymbol, options);
        }
        
        /// <summary>
        /// 依据 delimiter 拆分字符串，并转换为 float 数组
        /// </summary>
        public static float[] SplitToFloat(this string self, char delimiter) {
            string[] strs = self.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            if (strs.Length == 0) return Array.Empty<float>(); // 判空

            return Array.ConvertAll<string, float>(strs, float.Parse); // 转换 float
        }
        
        /// <summary>
        /// 依据 delimiter 拆分字符串，并转换为 float 数组
        /// </summary>
        public static float[] SplitToFloat(this string self, string delimiter) {
            string[] strs = self.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            if (strs.Length == 0) return Array.Empty<float>(); // 判空

            return Array.ConvertAll<string, float>(strs, float.Parse); // 转换 float
        }
        
        /// <summary>
        /// 依据 delimiter 拆分字符串，并转换为 int 数组
        /// </summary>
        public static int[] SplitToInt(this string self, string delimiter) {
            string[] strs = self.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            if (strs.Length == 0) return Array.Empty<int>(); // 判空

            return Array.ConvertAll<string, int>(strs, int.Parse); // 转换 int
        }
        
        /// <summary>
        /// 格式化字符串填充参数
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var newStr = "{0}, {1}".Format(1, 2);
        /// Debug.Log(newStr); // 1, 2
        /// ]]>
        /// </code> </example>
        public static string Format(this string self, params object[] args)
        {
            return string.Format(self, args);
        }
        
        /// <summary>
        /// 返回包含此字符串的 StringBuilder
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var builder = "Hello".Builder();
        /// builder.Append(" QF");
        /// Debug.Log(builder.ToString()); // Hello QF
        /// ]]>
        /// </code> </example>
        public static StringBuilder Builder(this string self)
        {
            return new StringBuilder(self);
        }
        
        /// <summary>
        /// StringBuilder 添加前缀
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var builder = "World!".Builder().AddPrefix("Hello ") ;
        /// Debug.Log(builder.ToString()); // Hello World!
        /// ]]>
        /// </code> </example>
        public static StringBuilder AddPrefix(this StringBuilder self, string prefixString)
        {
            self.Insert(0, prefixString);
            return self;
        }
        
        /// <summary>
        /// 字符串解析成 int（不安全）
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var number = "123456".ToInt();
        /// Debug.Log(number); // 123456（Not Safe）
        /// ]]>
        /// </code> </example>
        public static int ToInt(this string self, int defaultValue = 0)
        {
            return int.TryParse(self, out var value) ? value : defaultValue;
        }
        
        /// <summary>
        /// 字符串解析成 float（不安全）
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var number = "123456f".ToInt();
        /// Debug.Log(number); // 123456（Not Safe）
        /// ]]>
        /// </code> </example>
        public static float ToFloat(this string self, float defaultValue = 0)
        {
            return float.TryParse(self, out var value) ? value : defaultValue;
        }
        
        /// <summary>
        /// 字符串解析成 DateTime（不安全）
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// DateTime.Now.ToString().ToDataTime();
        /// ]]>
        /// </code> </example>
        public static DateTime ToDateTime(this string self, DateTime defaultValue = default(DateTime))
        {
            return DateTime.TryParse(self, out var value) ? value : defaultValue;
        }
        
        /// <summary>
        /// 是否存在中文字符
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("你好".HasChinese()); // true
        /// ]]>
        /// </code> </example>
        public static bool HasChinese(this string self)
        {
            return Regex.IsMatch(self, @"[\u4e00-\u9fa5]");
        }
        
        /// <summary>
        /// 是否存在空格
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("你好 ".HasSpace()); // true
        /// ]]>
        /// </code> </example>
        public static bool HasSpace(this string self)
        {
            return self.Contains(' ');
        }
        
        /// <summary>
        /// 从 self 中依次移除 targets 内的字符串
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("Hello World!".RemoveString("Hello", "World")); //  !
        /// ]]>
        /// </code> </example>
        public static string Remove(this string self, params string[] targets)
        {
            return targets.Aggregate(self, (current, t) => current.Replace(t, string.Empty));
        }
        
        /// <summary>
        /// join string
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(new List<string>() { "1","2","3"}.Join("","")); // 1,2,3
        /// ]]>
        /// </code> </example>
        public static string Join(this IEnumerable<string> self, string separator)
        {
            return string.Join(separator, self);
        }
        
        public static string TrimStart(this string source, string value) {
            if (source.StartsWith(value)) {
                return source.Substring(value.Length);
            }
            return source;
        }

        public static string TrimEnd(this string source, string value) {
            if (source.EndsWith(value)) {
                return source.Substring(0, source.Length - value.Length);
            }
            return source;
        }
    }
}