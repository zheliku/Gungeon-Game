// ------------------------------------------------------------
// @file       PlayerPrefsWindow.cs
// @brief
// @author     zheliku
// @Modified   2025-06-10 01:36:18
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.BindableKit.Editor
{
    using System;
    using System.Diagnostics;
    using Microsoft.Win32;
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEngine;
    using Debug = UnityEngine.Debug;

    public class PlayerPrefsWindow : OdinEditorWindow
    {
        [MenuItem("Framework/BindableKit/Open PlayerPrefs Window")]
        private static void OpenWindow()
        {
            var window = GetWindow<PlayerPrefsWindow>();
            window.InitializeList();
            window.Show();
        }

        [MenuItem("Framework/BindableKit/Open Registry Window")]
        private static void OpenRegistry()
        {
            // 获取PlayerSettings中的公司名和产品名
            string companyName = PlayerSettings.companyName;
            string productName = PlayerSettings.productName;

            // 构造注册表路径（Windows平台）
            var registryLastKey  = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";
            var registryLocation = $"HKEY_CURRENT_USER\\Software\\Unity\\UnityEditor\\{companyName}\\{productName}";

            Registry.SetValue(registryLastKey, "LastKey", registryLocation); // Set LastKey value that regedit will go directly to

            // 调用regedit并定位到指定路径
            Process.Start("regedit.exe");
        }

        public BindableList<PlayerPrefPair> PlayerPrefPairs;

        private void InitializeList()
        {
            if (PlayerPrefPairs != null)
            {
                return;
            }

            string companyName = PlayerSettings.companyName;
            string productName = PlayerSettings.productName;

            var keyValues = GetAll(companyName, productName);
            PlayerPrefPairs = new BindableList<PlayerPrefPair>(keyValues);

            // PlayerPrefPairs.OnAdd.Register((i,     pair) => pair.WriteToPlayerPrefs());
            PlayerPrefPairs.OnRemove.Register((i, pair) => pair.DeleteFromPlayerPrefs());
            PlayerPrefPairs.OnReplace.Register((i, oldPair, newPair) =>
            {
                oldPair.DeleteFromPlayerPrefs();
                newPair.WriteToPlayerPrefs();
            });
            PlayerPrefPairs.OnClear.Register(() =>
            {
                foreach (var pair in keyValues)
                {
                    pair.DeleteFromPlayerPrefs();
                }
            });
        }

        public static PlayerPrefPair[] GetAll(string companyName, string productName)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // From Unity docs: On Windows, PlayerPrefs are stored in the registry under HKCU\Software\[company name]\[product name] key, where company and product names are the names set up in Project Settings.
#if UNITY_5_5_OR_NEWER

                // From Unity 5.5 editor player prefs moved to a specific location
                var registryKey =
                    Registry.CurrentUser.OpenSubKey("Software\\Unity\\UnityEditor\\" + companyName + "\\" + productName);
#else
                Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\" + companyName + "\\" + productName);
#endif

                // Parse the registry if the specified registryKey exists
                if (registryKey != null)
                {
                    // Get an array of what keys (registry value names) are stored
                    string[] valueNames = registryKey.GetValueNames();

                    // Create the array of the right size to take the saved player prefs
                    PlayerPrefPair[] tempPlayerPrefs = new PlayerPrefPair[valueNames.Length];

                    // Parse and convert the registry saved player prefs into our array
                    for (int i = 0; i < valueNames.Length; i++)
                    {
                        string key = valueNames[i];

                        // Remove the _h193410979 style suffix used on player pref keys in Windows registry
                        int index = key.LastIndexOf("_", StringComparison.Ordinal);
                        key = key.Remove(index, key.Length - index);

                        // Get the value from the registry
                        var ambiguousValue = registryKey.GetValue(valueNames[i]);
                        var valueKind      = registryKey.GetValueKind(valueNames[i]);

                        // Unfortunately floats will come back as an int (at least on 64 bit) because the float is stored as
                        // 64 bit but marked as 32 bit - which confuses the GetValue() method greatly! 
                        if (valueKind == RegistryValueKind.DWord)
                        {
                            // If the player pref is not actually an int then it must be a float, this will evaluate to true
                            // (impossible for it to be 0 and -1 at the same time)
                            if (PlayerPrefs.GetInt(key, -1) == -1 && PlayerPrefs.GetInt(key, 0) == 0)
                            {
                                // Fetch the float value from PlayerPrefs in memory
                                ambiguousValue = PlayerPrefs.GetFloat(key);
                            }
                        }
                        else if (valueKind == RegistryValueKind.Binary)
                        {
                            // On Unity 5 a string may be stored as binary, so convert it back to a string
                            ambiguousValue = System.Text.Encoding.Default.GetString((byte[]) ambiguousValue);
                        }

                        // Assign the key and value into the respective record in our output array
                        tempPlayerPrefs[i] = new PlayerPrefPair() { Key = key, Value = ambiguousValue };
                    }

                    // Return the results
                    return tempPlayerPrefs;
                }
                else
                {
                    // No existing player prefs saved (which is valid), so just return an empty array
                    return Array.Empty<PlayerPrefPair>();
                }
            }
            else
            {
                throw new NotSupportedException("PlayerPrefsEditor doesn't support this Unity Editor platform");
            }
        }
    }

    [Serializable]
    public struct PlayerPrefPair
    {
        [HorizontalGroup("PlayerPref", Gap = 50)]
        [LabelWidth(50)]
        public string Key;

        [HorizontalGroup("PlayerPref")]
        [ShowInInspector] [HideReferenceObjectPicker]
        [LabelWidth(50)]
        public object Value;

        public void WriteToPlayerPrefs()
        {
            if (Value is string str)
            {
                PlayerPrefs.SetString(Key, str);
            }
            else if (Value is int i)
            {
                PlayerPrefs.SetInt(Key, i);
            }
            else if (Value is float f)
            {
                PlayerPrefs.SetFloat(Key, f);
            }
        }

        public void DeleteFromPlayerPrefs()
        {
            PlayerPrefs.DeleteKey(Key);
        }
    }
}