// ------------------------------------------------------------
// @file       CodeGenWindow.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 22:02:38
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.CodeGenKit.Editor
{
    using System.IO;
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEngine;

    public class CodeGenWindow : OdinEditorWindow
    {
        [ShowInInspector]
        public string NameSpace
        {
            get => CodeGenKitPipeline.Default.NameSpace;
            set => CodeGenKitPipeline.Default.NameSpace = value;
        }

        [ShowInInspector] [FolderPath]
        public string FolderPath
        {
            get => CodeGenKitPipeline.Default.FolderPath;
            set => CodeGenKitPipeline.Default.FolderPath = value;
        }

        [ShowInInspector]
        public string FileName
        {
            get => CodeGenKitPipeline.Default.FileName;
            set => CodeGenKitPipeline.Default.FileName = value;
        }

        [ShowInInspector]
        public GameObject SelectedGameObject
        {
            get => CodeGenKitPipeline.Default.LastSelectedGameObject;
            set
            {
                CodeGenKitPipeline.Default.LastSelectedGameObject = value;

                if (value != null)
                {
                    FileName = value.name.Replace(" ", "");
                }
            }
        }

        [ShowInInspector]
        public string Architecture
        {
            get => CodeGenKitPipeline.Default.Architecture;
            set => CodeGenKitPipeline.Default.Architecture = value;
        }

        public bool IsGenerating
        {
            get => CodeGenKitPipeline.Default.IsGenerating;
            set => CodeGenKitPipeline.Default.IsGenerating = value;
        }

        [HorizontalGroup("Buttons")]
        [Button(ButtonSizes.Large)]
        public void Generate()
        {
            IsGenerating = true;

            var filePath = $"{FolderPath}/{FileName}.cs";

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            File.WriteAllText(filePath, GenerateContent);

            // 刷新 Unity 资源数据库，触发脚本编译
            AssetDatabase.Refresh();
        }

        [HorizontalGroup("Buttons")]
        [Button(ButtonSizes.Large)]
        public void GenerateAndOpen()
        {
            IsGenerating = true;

            // SaveCodeGenData();

            var filePath = $"{FolderPath}/{FileName}.cs";
            
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            
            File.WriteAllText(filePath, GenerateContent);
            
            CodeGenKit.OpenFile(filePath); // 打开文件

            // 刷新 Unity 资源数据库，触发脚本编译
            AssetDatabase.Refresh();
        }

        [ShowInInspector] [DisplayAsString(false)] [HideLabel]
        public string GenerateContent
        {
            get => CodeGenKitPipeline.Default.GenerateFileContent();
        }

        [MenuItem("Framework/CodeGen/Open CodeGen Window &V")]
        private static void OpenWindow()
        {
            var window = GetWindow<CodeGenWindow>();
            window.Show();

            window.LoadCodeGenData();

            window.SelectedGameObject = Selection.activeGameObject;
        }

        private void LoadCodeGenData()
        {
            NameSpace    = CodeGenKitPipeline.Default.GlobalNameSpace;
            FolderPath   = CodeGenKitPipeline.Default.GlobalFolderPath;
            FileName     = CodeGenKitPipeline.Default.GlobalFileName;
            Architecture = CodeGenKitPipeline.Default.GlobalArchitecture;
        }

        [Button(ButtonSizes.Large)] [PropertySpace(5)]
        private void SaveCodeGenData()
        {
            CodeGenKitPipeline.Default.GlobalNameSpace    = NameSpace;
            CodeGenKitPipeline.Default.GlobalFolderPath   = FolderPath;
            CodeGenKitPipeline.Default.GlobalFileName     = FileName;
            CodeGenKitPipeline.Default.GlobalArchitecture = Architecture;
        }
    }
}