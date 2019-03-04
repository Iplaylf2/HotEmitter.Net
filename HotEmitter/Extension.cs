using System;
using System.Collections.Generic;
using System.Text;

namespace HotEmitter
{
    public static class Extension
    {
        public static Line<T> Filter<T>(this Line<T> lineA, Func<T, Boolean> predicate)
        {
            throw new NotImplementedException();
        }

        public static Line<TResult> Map<T, TResult>(this Line<T> lineA, Func<T, TResult> selector)
        {
            throw new NotImplementedException();
        }

        public static Line<TResult> Scan<T, TResult>(this Line<T> lineA, Func<TResult, T, TResult> accumulator, TResult seed)
        {
            throw new NotImplementedException();
        }

        private static Line<TResult> MakeExtensionLine<T, TResult>(Line<T> lineA, Func<Action<TResult>, Action<T>> adapter)
        {
            var emitterB = new Emitter<TResult>();
            var receiverOfA = adapter(emitterB.Emit);
            lineA.Connect(receiverOfA);
            return emitterB.Line;
        }
    }
}
