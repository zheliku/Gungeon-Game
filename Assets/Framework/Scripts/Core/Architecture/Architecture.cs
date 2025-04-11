// ------------------------------------------------------------
// @file       Architecture.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using Command;
    using global::System;
    using global::System.Linq;
    using Model;
    using Sirenix.OdinInspector;

    [HideReferenceObjectPicker]
    public abstract class AbstractArchitecture<TArchitecture> : IArchitecture where TArchitecture : AbstractArchitecture<TArchitecture>, new()
    {
    #region 常量

    #endregion

    #region Static

        public static Action<TArchitecture> OnRegisterPatch = architecture => { };

        protected static TArchitecture _Architecture;

        static void MakeSureArchitecture()
        {
            if (_Architecture == null)
            { 
                // 如果架构为空，则创建新的架构
                _Architecture = new TArchitecture();

                _Architecture.Init(); // 初始化架构

                OnRegisterPatch?.Invoke(_Architecture); // 如果有注册补丁的回调函数，则调用

                // 遍历架构中的所有模型，如果模型未初始化，则初始化
                foreach (var model in _Architecture._iocContainer.GetInstancesByType<IModel>().Where<IModel>(m => !m.Initialized))
                {
                    model.Init();
                    model.Initialized = true;
                }

                // 遍历架构中的所有系统，如果系统未初始化，则初始化
                foreach (var system in _Architecture._iocContainer.GetInstancesByType<ISystem>().Where<ISystem>(s => !s.Initialized))
                {
                    system.Init();
                    system.Initialized = true;
                }

                // 设置架构已经初始化
                _Architecture._inited = true;
            }
        }

        public static IArchitecture Architecture
        {
            get
            {
                MakeSureArchitecture(); // 如果架构为空，则创建新的架构
                return _Architecture;
            }
        }

    #endregion

    #region 字段

        [ShowInInspector]
        private bool _inited = false;

        [ShowInInspector]
        private IOCContainer _iocContainer = new IOCContainer();

        // 一个 Architecture 对应一个 TypeEventSystem
        [ShowInInspector]
        private TypeEventSystem _typeEventSystem = new TypeEventSystem();

    #endregion

    #region 属性

    #endregion

    #region 接口

        public void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem
        {
            system.SetArchitecture(this);
            _iocContainer.Register<TSystem>(system);

            if (_inited)
            { // 若 Architecture 已初始化，则新注册的 System 也需要立即初始化
                system.Init();
                system.Initialized = true;
            }
        }

        public void RegisterModel<TModel>(TModel model) where TModel : IModel
        {
            model.SetArchitecture(this);
            _iocContainer.Register<TModel>(model);

            if (_inited)
            { // 若 Architecture 已初始化，则新注册的 Model 也需要立即初始化
                model.Init();
                model.Initialized = true;
            }
        }

        public void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility
        {
            _iocContainer.Register<TUtility>(utility);
        }

        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem
        {
            return _iocContainer.Get<TSystem>();
        }

        public TModel GetModel<TModel>() where TModel : class, IModel
        {
            return _iocContainer.Get<TModel>();
        }

        public TUtility GetUtility<TUtility>() where TUtility : class, IUtility
        {
            return _iocContainer.Get<TUtility>();
        }

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            ExecuteCommand(command);
        }

        public TResult SendCommand<TResult>(ICommand<TResult> command)
        {
            return ExecuteCommand(command);
        }

        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            return DoQuery<TResult>(query);
        }

        public void SendEvent<TEvent>() where TEvent : new()
        {
            _typeEventSystem.Send<TEvent>();
        }

        public void SendEvent<TEvent>(TEvent e)
        {
            _typeEventSystem.Send<TEvent>(e);
        }

        public IUnRegister RegisterEvent<TEvent>(Action<TEvent> onEvent, int priority)
        {
            return _typeEventSystem.Register<TEvent>(onEvent, priority);
        }

        public void UnRegisterEvent<TEvent>(Action<TEvent> onEvent)
        {
            _typeEventSystem.UnRegister<TEvent>(onEvent);
        }

        public void Deinit()
        {
            OnDeinit(); // 调用反初始化事件

            // 遍历所有已初始化的系统，调用其反初始化方法
            foreach (var system in Enumerable.Where<ISystem>(_iocContainer.GetInstancesByType<ISystem>(), s => s.Initialized)) { system.Deinit(); }

            // 遍历所有已初始化的模型，调用其反初始化方法
            foreach (var model in Enumerable.Where<IModel>(_iocContainer.GetInstancesByType<IModel>(), m => m.Initialized)) { model.Deinit(); }

            _iocContainer.Clear(); // 清空 IOC 容器
            _inited = false;       // 设置初始化状态为 false
        }

    #endregion

    #region 方法

        protected abstract void Init();

        protected virtual void OnDeinit() { }

        protected virtual TResult ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            command.SetArchitecture(this);
            return command.Execute();
        }

        protected virtual void ExecuteCommand(ICommand command)
        {
            command.SetArchitecture(this);
            command.Execute();
        }

        protected virtual TResult DoQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }

    #endregion

    #region 回调

    #endregion

    #region 协程

    #endregion

    #region Unity 事件

    #endregion
    }
}