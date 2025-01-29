// ------------------------------------------------------------
// @file       Condition.cs
// @brief
// @author     zheliku
// @Modified   2024-10-26 12:10:38
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;

    /// <summary>
    /// 条件 Action
    /// </summary>
    public class Condition : AbstractAction<Condition>
    {
    #region Static

        public static Condition Create(Func<bool> func)
        {
            var condition = CreateInternal();
            condition._condition = func;
            return condition;
        }

    #endregion

    #region 字段

        private Func<bool> _condition;

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart() { }

        public override void OnExecute(float deltaTime)
        {
            // 满足条件才结束 Action
            if (_condition.Invoke())
            {
                this.Finish();
            }
        }

        public override void OnFinish() { }

        protected override void OnReset() { }

        protected override void OnDeinit()
        {
            _condition = null;
        }

    #endregion
    }
}