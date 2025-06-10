// ------------------------------------------------------------
// @file       NetKit.API.cs
// @brief
// @author     zheliku
// @Modified   2025-05-14 11:58:01
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.NetKit
{
    using System;
    using UnityEngine;
    using UnityEngine.Networking;

    public partial class NetKit
    {
        public static string HttpServerUrl = "http://192.168.186.1:8080/HTTP Server";

        public static void DownloadBytes(
            string                  filePath,
            Action<byte[]>          callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadBytesCoroutine(HttpServerUrl, filePath, callback, setting));
        }

        public static void DownloadBytes(
            string                  httpServerUrl,
            string                  filePath,
            Action<byte[]>          callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadBytesCoroutine(httpServerUrl, filePath, callback, setting));
        }

        public static void DownloadTexture(
            string                  filePath,
            Action<Texture2D>       callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadTextureCoroutine(HttpServerUrl, filePath, callback, setting));
        }

        public static void DownloadTexture(
            string                  httpServerUrl,
            string                  filePath,
            Action<Texture2D>       callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadTextureCoroutine(httpServerUrl, filePath, callback, setting));
        }

        public static void DownloadAssetBundle(
            string                  filePath,
            Action<AssetBundle>     callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadAssetBundleCoroutine(HttpServerUrl, filePath, callback, setting));
        }

        public static void DownloadAssetBundle(
            string                  httpServerUrl,
            string                  filePath,
            Action<AssetBundle>     callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadAssetBundleCoroutine(httpServerUrl, filePath, callback, setting));
        }

        public static void DownloadTextAsset(
            string                  filePath,
            Action<TextAsset>       callback,
            string                  savePath = null,
            Action<UnityWebRequest> setting  = null)
        {
            _Instance.StartCoroutine(DownloadTextAssetCoroutine(HttpServerUrl, filePath, callback, savePath, setting));
        }

        public static void DownloadTextAsset(
            string                  httpServerUrl,
            string                  filePath,
            Action<TextAsset>       callback,
            string                  savePath = null,
            Action<UnityWebRequest> setting  = null)
        {
            _Instance.StartCoroutine(DownloadTextAssetCoroutine(httpServerUrl, filePath, callback, savePath, setting));
        }

        public static void DownloadAudioClip(
            string                  filePath,
            AudioType               audioType,
            Action<AudioClip>       callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadAudioClipCoroutine(HttpServerUrl, filePath, audioType, callback, setting));
        }

        public static void DownloadAudioClip(
            string                  httpServerUrl,
            string                  filePath,
            AudioType               audioType,
            Action<AudioClip>       callback,
            Action<UnityWebRequest> setting = null)
        {
            _Instance.StartCoroutine(DownloadAudioClipCoroutine(httpServerUrl, filePath, audioType, callback, setting));
        }


        public static void DownloadFile(
            string                         filePath,
            string                         savePath,
            Action<UnityWebRequest.Result> callback = null,
            Action<UnityWebRequest>        setting  = null)
        {
            _Instance.StartCoroutine(DownloadFileCoroutine(HttpServerUrl, filePath, savePath, callback, setting));
        }

        public static void DownloadFile(
            string                         httpServerUrl,
            string                         filePath,
            string                         savePath,
            Action<UnityWebRequest.Result> callback = null,
            Action<UnityWebRequest>        setting  = null)
        {
            _Instance.StartCoroutine(DownloadFileCoroutine(httpServerUrl, filePath, savePath, callback, setting));
        }

        public static void UploadFile(
            string                         filePath,
            string                         localPath,
            Action<UnityWebRequest.Result> callback = null,
            Action<UnityWebRequest>        setting  = null)
        {
            _Instance.StartCoroutine(UploadFileCoroutine(HttpServerUrl, filePath, localPath, callback, setting));
        }

        public static void UploadFile(
            string                         httpServerUrl,
            string                         filePath,
            string                         localPath,
            Action<UnityWebRequest.Result> callback = null,
            Action<UnityWebRequest>        setting  = null)
        {
            _Instance.StartCoroutine(UploadFileCoroutine(httpServerUrl, filePath, localPath, callback, setting));
        }

        public static void UploadFile(
            string                         filePath,
            byte[]                         fileBytes,
            Action<UnityWebRequest.Result> callback = null,
            Action<UnityWebRequest>        setting  = null)
        {
            _Instance.StartCoroutine(UploadFileCoroutine(HttpServerUrl, filePath, fileBytes, callback, setting));
        }

        public static void UploadFile(
            string                         httpServerUrl,
            string                         filePath,
            byte[]                         fileBytes,
            Action<UnityWebRequest.Result> callback = null,
            Action<UnityWebRequest>        setting  = null)
        {
            _Instance.StartCoroutine(UploadFileCoroutine(httpServerUrl, filePath, fileBytes, callback, setting));
        }
    }
}