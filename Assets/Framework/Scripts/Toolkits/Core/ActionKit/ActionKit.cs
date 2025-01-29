// ------------------------------------------------------------
// @file       ActionKit.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 19:10:26
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using System.Collections;
    using Framework.Core;

    public partial class ActionKit : Architecture<ActionKit>
    {
        public static ulong IDGenerator = 0;

        protected override void Init() { }

        /// <summary>
        /// 回调事件
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("Sequence Start: " + Time.time);
        /// ActionKit.Sequence()
        /// .Callback(() => Debug.Log("Delay Start: " + Time.time))
        /// .Delay(1.0f)
        ///     .Callback(() => Debug.Log("Delay Finish: " + Time.time))
        /// .Start(this, _ => { Debug.Log("Sequence Finish: " + Time.time); });
        ///  
        /// // Sequence Start: 0
        /// // Delay Start: 0
        /// ------ 1 秒后 ------
        /// // Delay Finish: 0.984723
        /// // Sequence Finish: 0.984723
        /// ]]>
        /// </code> </example>
        public static IAction Callback(global::System.Action callback)
        {
            return Toolkits.ActionKit.Callback.Create(callback);
        }

        /// <summary>
        /// 延时回调
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("Start Time: " + Time.time);
        /// ActionKit.Delay(2, () =>
        /// {
        ///      Debug.Log("End Time: " + Time.time);
        /// }).Start(this); // update driven
        ///  
        /// // Start Time: 0.000000
        /// ------ 2 秒后 ------
        /// // End Time: 1.986583
        /// ]]>
        /// </code> </example>
        public static IAction Delay(float seconds, global::System.Action callback)
        {
            return Toolkits.ActionKit.Delay.Create(seconds, callback);
        }

        /// <summary>
        /// 动作序列
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("Sequence Start: " + Time.time);
        /// ActionKit.Sequence()
        ///          .Callback(() => Debug.Log("Delay Start: " + Time.time))
        ///          .Delay(1.0f)
        ///          .Callback(() => Debug.Log("Delay Finish: " + Time.time))
        ///          .Start(this, _ => { Debug.Log("Sequence Finish: " + Time.time); });
        ///  
        /// // Sequence Start: 0
        /// // Delay Start: 0
        /// ------ 1 秒后 ------
        /// // Delay Finish: 0.984723
        /// // Sequence Finish: 0.984723
        /// ]]>
        /// </code> </example>
        public static ISequence Sequence()
        {
            return Toolkits.ActionKit.Sequence.Create();
        }

        /// <summary>
        /// 延时帧
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("Delay Frame Start FrameCount: " + Time.frameCount);
        /// ActionKit.DelayFrame(1, () => { Debug.Log("Delay Frame Finish FrameCount: " + Time.frameCount); })
        ///          .Start(this);
        ///  
        /// ActionKit.Sequence()
        ///          .DelayFrame(10)
        ///          .Callback(() => Debug.Log("Sequence Delay FrameCount: " + Time.frameCount))
        ///          .Start(this);
        ///  
        /// // Delay Frame Start FrameCount: 1
        /// // Delay Frame Finish FrameCount: 2
        /// // Sequence Delay FrameCount: 11
        /// ]]>
        /// </code> </example>
        public static IAction DelayFrame(int frameCount, global::System.Action onDelayFinish = null)
        {
            return Toolkits.ActionKit.DelayFrame.Create(frameCount, onDelayFinish);
        }

        /// <summary>
        /// 下一帧
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.Sequence()
        ///          .NextFrame()
        ///          .Start(this);
        ///  
        /// ActionKit.NextFrame(() => { })
        ///          .Start(this);
        /// ]]>
        /// </code> </example>
        public static IAction NextFrame(global::System.Action onDelayFinish = null)
        {
            return Toolkits.ActionKit.DelayFrame.Create(1, onDelayFinish);
        }

        /// <summary>
        /// 重复动作
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.Repeat()
        ///          .Condition(() => Input.GetMouseButtonDown(0))
        ///          .Callback(() => Debug.Log("Mouse Clicked"))
        ///          .Start(this);
        ///  
        /// // always Log Mouse Clicked when click left mouse
        /// // 鼠标左键点击时，每次都会输出 Mouse Clicked
        ///  
        /// ActionKit.Repeat(5) // -1、0 means forever 1 means once  2 means twice
        ///          .Condition(() => Input.GetMouseButtonDown(1))
        ///          .Callback(() => Debug.Log("Mouse right clicked"))
        ///          .Start(this, () =>
        ///          {
        ///              Debug.Log("Right click finished");
        ///          });
        ///  
        /// // Mouse right clicked
        /// // Mouse right clicked
        /// // Mouse right clicked
        /// // Mouse right clicked
        /// // Mouse right clicked
        /// // Right click finished
        /// ]]>
        /// </code> </example>
        public static IRepeat Repeat(int repeatCount = -1)
        {
            return Toolkits.ActionKit.Repeat.Create(repeatCount);
        }

        /// <summary>
        /// 并行动作
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log("Parallel Start: " + Time.time);
        ///  
        /// ActionKit.Parallel()
        /// .Delay(1.0f, () => { Debug.Log(Time.time); })
        /// .Delay(2.0f, () => { Debug.Log(Time.time); })
        /// .Delay(3.0f, () => { Debug.Log(Time.time); })
        /// .Start(this, () =>
        /// {
        ///     Debug.Log("Parallel Finish: " + Time.time);
        /// });
        ///  
        /// // Parallel Start: 0
        /// // 1.01
        /// // 2.01
        /// // 3.02
        /// // Parallel Finish: 3.02
        /// ]]>
        /// </code> </example>
        public static IParallel Parallel()
        {
            return Toolkits.ActionKit.Parallel.Create();
        }

        /// <summary>
        /// 自定义动作
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.Custom(a =>
        /// {
        ///     a.OnStart(() => { Debug.Log("OnStart"); })
        ///      .OnExecute(dt =>
        ///       {
        ///           Debug.Log("OnExecute")
        ///           a.Finish();
        ///       })
        ///      .OnFinish(() => { Debug.Log("OnFinish"); });
        /// }).Start(this);
        ///  
        /// // OnStart
        /// // OnExecute
        /// // OnFinish
        /// ]]>
        /// </code> </example>
        public static IAction Custom(Action<ICustomAPI<object>> customSetting)
        {
            var action = Toolkits.ActionKit.Custom.Create();
            customSetting(action);
            return action;
        }

        /// <summary>
        /// 自定义动作
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// class SomeData
        /// {
        ///     public int ExecuteCount = 0;
        /// }
        ///  
        /// ActionKit.Custom<SomeData>(a =>
        /// {
        ///     a.OnStart(() =>
        ///       {
        ///           a.Data = new SomeData()
        ///           {
        ///               ExecuteCount = 0
        ///           };
        ///       })
        ///      .OnExecute(dt =>
        ///       {
        ///           Debug.Log(a.Data.ExecuteCount);
        ///           a.Data.ExecuteCount++;
        ///
        ///           if (a.Data.ExecuteCount >= 5)
        ///           {
        ///               a.Finish();
        ///           }
        ///       }).OnFinish(() => { Debug.Log("Finished"); });
        /// }).Start(this);
        ///  
        /// // 0
        /// // 1
        /// // 2
        /// // 3
        /// // 4
        /// // Finished
        /// ]]>
        /// </code> </example>
        public static IAction Custom<TData>(Action<ICustomAPI<TData>> customSetting)
        {
            var action = Toolkits.ActionKit.Custom<TData>.Create();
            customSetting(action);
            return action;
        }

        /// <summary>
        /// 协程支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// IEnumerator SomeCoroutine()
        /// {
        ///     yield return new WaitForSeconds(1.0f);
        ///     Debug.Log("Hello: " + Time.time);
        /// }
        ///  
        /// ActionKit.Coroutine(SomeCoroutine).Start(this);
        ///  
        /// // Hello: 1.0039
        ///  
        /// SomeCoroutine().ToAction().Start(this);
        ///  
        /// // Hello: 1.0039
        ///  
        /// ActionKit.Sequence()
        ///          .Coroutine(SomeCoroutine)
        ///          .Start(this);
        ///  
        /// // Hello: 1.0039
        /// ]]>
        /// </code> </example>
        public static IAction Coroutine(Func<IEnumerator> coroutineGetter)
        {
            return Toolkits.ActionKit.Coroutine.Create(coroutineGetter);
        }

        public static IAction Lerp(float a, float b, float duration, global::System.Action<float> onLerp, global::System.Action onLerpFinish = null)
        {
            return Toolkits.ActionKit.Lerp.Create(a, b, duration, onLerp, onLerpFinish);
        }

        public static IAction Lerp01(float duration, global::System.Action<float> onLerp, global::System.Action onLerpFinish = null)
        {
            return Toolkits.ActionKit.Lerp.Create(0, 1, duration, onLerp, onLerpFinish);
        }

        /// <summary>
        /// Task 支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// async Task SomeTask()
        /// {
        ///     await Task.Delay(TimeSpan.FromSeconds(1.0f));
        ///     Debug.Log("Hello: " + Time.time);
        /// }
        ///  
        /// ActionKit.Task(SomeTask).Start(this);
        ///  
        /// SomeTask().ToAction().Start(this);
        ///  
        /// ActionKit.Sequence()
        ///          .Task(SomeTask)
        ///          .Start(this);
        ///  
        /// // Hello: 1.0039
        /// ]]>
        /// </code> </example>
        public static IAction Task(Func<global::System.Threading.Tasks.Task> taskGetter)
        {
            return Toolkits.ActionKit.Task.Create(taskGetter);
        }

        /// <summary>
        /// Update 生命周期支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.OnUpdate.Register(() =>
        /// {
        ///     if (Time.frameCount % 30 == 0)
        ///     {
        ///         Debug.Log("Update");
        ///     }
        /// }).UnRegisterWhenGameObjectDestroyed(gameObject);
        /// ]]>
        /// </code> </example>
        public static EasyEvent OnUpdate { get => ActionKitMonoBehaviourEvent.Instance.OnUpdate; }

        /// <summary>
        /// OnFixedUpdate 生命周期支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.OnFixedUpdate.Register(() =>
        /// {
        ///     // fixed update code here
        ///     // 这里写 fixed update 相关代码
        /// }).UnRegisterWhenGameObjectDestroyed(gameObject);
        /// ]]>
        /// </code> </example>
        public static EasyEvent OnFixedUpdate { get => ActionKitMonoBehaviourEvent.Instance.OnFixedUpdate; }

        /// <summary>
        /// OnLateUpdate 生命周期支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.OnLateUpdate.Register(() =>
        /// {
        ///     // late update code here
        ///     // 这里写 late update 相关代码
        /// }).UnRegisterWhenGameObjectDestroyed(gameObject);
        /// ]]>
        /// </code> </example>
        public static EasyEvent OnLateUpdate { get => ActionKitMonoBehaviourEvent.Instance.OnLateUpdate; }

        /// <summary>
        /// OnGUI 生命周期支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.OnGUI.Register(() =>
        /// {
        ///     GUILayout.Label("See Example Code");
        ///     GUILayout.Label("请查看示例代码");
        /// }).UnRegisterWhenGameObjectDestroyed(gameObject);
        /// ]]>
        /// </code> </example>
        public static EasyEvent OnGUI { get => ActionKitMonoBehaviourEvent.Instance.OnGUIEvent; }

        /// <summary>
        /// OnApplicationQuit 生命周期支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.OnApplicationQuit.Register(() =>
        /// {
        ///     Debug.Log("quit");
        /// }).UnRegisterWhenGameObjectDestroyed(gameObject);
        /// ]]>
        /// </code> </example>
        public static EasyEvent OnApplicationQuit { get => ActionKitMonoBehaviourEvent.Instance.OnApplicationQuitEvent; }

        /// <summary>
        /// OnApplicationPause 生命周期支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.OnApplicationPause.Register(pause =>
        /// {
        ///     Debug.Log("pause: " + pause);
        /// }).UnRegisterWhenGameObjectDestroyed(gameObject);
        /// ]]>
        /// </code> </example>
        public static EasyEvent<bool> OnApplicationPause { get => ActionKitMonoBehaviourEvent.Instance.OnApplicationPauseEvent; }

        /// <summary>
        /// OnApplicationFocus 生命周期支持
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// ActionKit.OnApplicationFocus.Register(focus =>
        /// {
        ///     Debug.Log("focus: " + focus);
        /// }).UnRegisterWhenGameObjectDestroyed(gameObject);
        /// ]]>
        /// </code> </example>
        public static EasyEvent<bool> OnApplicationFocus { get => ActionKitMonoBehaviourEvent.Instance.OnApplicationFocusEvent; }

        public static void ClearGlobal()
        {
            var executor = ActionKitMonoBehaviourEvent.Instance.GetComponent<ActionExecutor>();
            executor?.Clear();
        }
        
        public static void ClearCurrentScene()
        {
            var executor = ActionKitCurrentScene.SceneComponent.GetComponent<ActionExecutor>();
            executor?.Clear();
        }
    }
}