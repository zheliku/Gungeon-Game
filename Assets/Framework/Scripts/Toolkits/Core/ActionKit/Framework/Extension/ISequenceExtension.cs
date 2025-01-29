// ------------------------------------------------------------
// @file       SequenceExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-25 22:10:21
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using System.Collections;

    public static class ISequenceExtension
    {
        public static ISequence Sequence(this ISequence self, Action<ISequence> sequenceSetting)
        {
            var repeat = Toolkits.ActionKit.Sequence.Create();
            sequenceSetting(repeat);
            return self.Append(repeat);
        }

        public static ISequence DelayFrame(this ISequence self, int frameCount, Action onDelayFinish = null)
        {
            return self.Append(Toolkits.ActionKit.DelayFrame.Create(frameCount, onDelayFinish));
        }

        public static ISequence NextFrame(this ISequence self, Action onDelayFinish = null)
        {
            return self.Append(Toolkits.ActionKit.DelayFrame.Create(1, onDelayFinish));
        }

        public static ISequence Delay(this ISequence self, float seconds, Action onDelayFinish = null)
        {
            return self.Append(Toolkits.ActionKit.Delay.Create(seconds, onDelayFinish));
        }

        public static ISequence Delay(this ISequence self, Func<float> delayTimeFactory, Action onDelayFinish = null)
        {
            return self.Append(Toolkits.ActionKit.Delay.Create(delayTimeFactory, onDelayFinish));
        }

        public static ISequence Callback(this ISequence self, Action callback)
        {
            return self.Append(Toolkits.ActionKit.Callback.Create(callback));
        }

        public static ISequence Condition(this ISequence self, Func<bool> condition)
        {
            return self.Append(Toolkits.ActionKit.Condition.Create(condition));
        }

        public static ISequence Repeat(this ISequence self, Action<IRepeat> repeatSetting)
        {
            var repeat = Toolkits.ActionKit.Repeat.Create();
            repeatSetting(repeat);
            return self.Append(repeat);
        }

        public static ISequence Repeat(this ISequence self, int repeatCount, Action<IRepeat> repeatSetting)
        {
            var repeat = Toolkits.ActionKit.Repeat.Create(repeatCount);
            repeatSetting(repeat);
            return self.Append(repeat);
        }

        public static ISequence Parallel(this ISequence self, Action<ISequence> parallelSetting)
        {
            var parallel = Toolkits.ActionKit.Parallel.Create();
            parallelSetting(parallel);
            return self.Append(parallel);
        }

        public static ISequence Custom(this ISequence self, Action<ICustomAPI<object>> onCustomSetting)
        {
            var custom = ActionKit.Custom(onCustomSetting);
            return self.Append(custom);
        }


        public static ISequence Custom<TData>(this ISequence self, Action<ICustomAPI<TData>> onCustomSetting)
        {
            var custom = ActionKit.Custom(onCustomSetting);
            return self.Append(custom);
        }

        public static ISequence Coroutine(this ISequence self, Func<IEnumerator> coroutineGetter)
        {
            return self.Append(Toolkits.ActionKit.Coroutine.Create(coroutineGetter));
        }

        public static ISequence Lerp(this ISequence self, float a, float b, float duration, Action<float> onLerp = null, Action onLerpFinish = null)
        {
            return self.Append(Toolkits.ActionKit.Lerp.Create(a, b, duration, onLerp, onLerpFinish));
        }

        public static ISequence Lerp01(this ISequence self, float duration, Action<float> onLerp = null, Action onLerpFinish = null)
        {
            return self.Append(Toolkits.ActionKit.Lerp.Create(0, 1, duration, onLerp, onLerpFinish));
        }
        
        public static ISequence Task(this ISequence self, Func<System.Threading.Tasks.Task> taskGetter)
        {
            return self.Append(Toolkits.ActionKit.Task.Create(taskGetter));
        }

        public static IAction ToAction(this System.Threading.Tasks.Task self)
        {
            return Toolkits.ActionKit.Task.Create(() => self);
        }
    }
}