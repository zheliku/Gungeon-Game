// ------------------------------------------------------------
// @file       NetKit.Impl.cs
// @brief
// @author     zheliku
// @Modified   2025-05-18 15:20:17
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.NetKit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using UnityEngine.Networking;

    public partial class NetKit
    {
        private static NetMgr _Instance
        {
            get => NetMgr.Instance;
        }

        private static IEnumerator DownloadBytesCoroutine(
            string                  httpServerUrl,
            string                  filePath,
            Action<byte[]>          callback,
            Action<UnityWebRequest> setting)
        {
            using var req = new UnityWebRequest(httpServerUrl + "/" + filePath, UnityWebRequest.kHttpVerbGET);
            req.downloadHandler = new DownloadHandlerBuffer();

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(req.downloadHandler.data);
                Debug.Log("【NetKit】下载成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.downloadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("【NetKit】下载出现问题！\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }

        private static IEnumerator DownloadTextureCoroutine(
            string                  httpServerUrl,
            string                  filePath,
            Action<Texture2D>       callback,
            Action<UnityWebRequest> setting)
        {
            using var req = new UnityWebRequest(httpServerUrl + "/" + filePath, UnityWebRequest.kHttpVerbGET);
            req.downloadHandler = new DownloadHandlerTexture();

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(DownloadHandlerTexture.GetContent(req));
                Debug.Log("【NetKit】下载成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.downloadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("【NetKit】下载出现问题！\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }

        private static IEnumerator DownloadAssetBundleCoroutine(
            string                  httpServerUrl,
            string                  filePath,
            Action<AssetBundle>     callback,
            Action<UnityWebRequest> setting)
        {
            using var req = new UnityWebRequest(httpServerUrl + "/" + filePath, UnityWebRequest.kHttpVerbGET);
            req.downloadHandler = new DownloadHandlerAssetBundle(req.url, 0);

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke((req.downloadHandler as DownloadHandlerAssetBundle)?.assetBundle);
                Debug.Log("【NetKit】下载成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.downloadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("【NetKit】下载出现问题！\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }

        private static IEnumerator DownloadTextAssetCoroutine(
            string                  httpServerUrl,
            string                  filePath,
            Action<TextAsset>       callback,
            string                  savePath,
            Action<UnityWebRequest> setting)
        {
            using var req = new UnityWebRequest(httpServerUrl + "/" + filePath, UnityWebRequest.kHttpVerbGET);

            if (savePath != null)
            {
                req.downloadHandler = new DownloadHandlerFile(savePath);
            }
            else
            {
                req.downloadHandler = new DownloadHandlerBuffer();
            }

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                var asset = new TextAsset(req.downloadHandler.text);
                callback?.Invoke(asset);
                Debug.Log("【NetKit】下载成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.downloadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("【NetKit】下载出现问题！\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }

        private static IEnumerator DownloadAudioClipCoroutine(
            string                  httpServerUrl,
            string                  filePath,
            AudioType               audioType,
            Action<AudioClip>       callback,
            Action<UnityWebRequest> setting)
        {
            using var req = new UnityWebRequest(httpServerUrl + "/" + filePath, UnityWebRequest.kHttpVerbGET);

            req.downloadHandler = new DownloadHandlerAudioClip(req.url, audioType);

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(DownloadHandlerAudioClip.GetContent(req));
                Debug.Log("【NetKit】下载成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.downloadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("【NetKit】下载出现问题！\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }

        private static IEnumerator DownloadFileCoroutine(
            string                         httpServerUrl,
            string                         filePath,
            string                         savePath,
            Action<UnityWebRequest.Result> callback,
            Action<UnityWebRequest>        setting)
        {
            using var req = new UnityWebRequest(httpServerUrl + "/" + filePath, UnityWebRequest.kHttpVerbGET);
            req.downloadHandler = new DownloadHandlerFile(savePath);

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            callback?.Invoke(req.result);

            if (req.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("【NetKit】下载成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.downloadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("【NetKit】下载出现问题！\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }

        private static IEnumerator UploadFileCoroutine(
            string                         httpServerUrl,
            string                         filePath,
            string                         localPath,
            Action<UnityWebRequest.Result> callback,
            Action<UnityWebRequest>        setting)
        {
            var data = new List<IMultipartFormSection>
            {
                new MultipartFormFileSection(filePath, File.ReadAllBytes(localPath))
            };

            using var req = UnityWebRequest.Post(httpServerUrl, data);

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            callback?.Invoke(req.result);

            if (req.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("【NetKit】上传成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.uploadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("上传出现问题！\n" +
                                 $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }

        private static IEnumerator UploadFileCoroutine(
            string                         httpServerUrl,
            string                         filePath,
            byte[]                         fileBytes,
            Action<UnityWebRequest.Result> callback,
            Action<UnityWebRequest>        setting)
        {
            var data = new List<IMultipartFormSection>
            {
                new MultipartFormFileSection(filePath, fileBytes)
            };

            using var req = UnityWebRequest.Post(httpServerUrl, data);

            setting?.Invoke(req);

            yield return req.SendWebRequest();

            callback?.Invoke(req.result);

            if (req.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("【NetKit】上传成功！\n" +
                          $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                          $"data: {req.uploadedBytes} bytes");
            }
            else
            {
                Debug.LogWarning("上传出现问题！\n" +
                                 $"url: \"{httpServerUrl + "/" + filePath}\"\n" +
                                 $"error: {req.error}\n" +
                                 $"responseCode: {req.responseCode}");
            }
        }
    }
}