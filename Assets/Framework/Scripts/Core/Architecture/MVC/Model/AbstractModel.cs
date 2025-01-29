// ------------------------------------------------------------
// @file       Model.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:49
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.Model
{
    using Sirenix.OdinInspector;

    /// <summary>
    /// Model 基类
    /// </summary>
    [HideReferenceObjectPicker]
    public abstract class AbstractModel : IModel
    {
        private IArchitecture _architecture;

        // 仅能通过 IBelongToArchitecture 接口访问 Architecture 属性
        IArchitecture IBelongToArchitecture.Architecture => _architecture;

        // 仅能通过 ICanSetArchitecture 接口设置 Architecture 属性
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) { _architecture = architecture; }

        [ShowInInspector]
        public bool Initialized { get; set; }

        // 仅能通过 ICanInit 接口使用 Init 方法
        void ICanInit.Init() { OnInit(); }

        public void Deinit() { OnDeinit(); }

        /// <summary>
        /// 初始化方法，需要由子类实现
        /// </summary>
        protected abstract void OnInit();

        /// <summary>
        /// 反初始化方法
        /// </summary>
        protected virtual void OnDeinit() { }
    }
}