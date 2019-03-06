using System;
using System.Linq;

namespace HotEmitter
{
    public static class LineExtension
    {
        public static ILine<T> Filter<T>(this ILine<T> lineA, Func<T, Boolean> predicate)
        {
            return MakeExtension<T, T>(lineA, adapter);

            Action<T> adapter(Action<T> emitterB)
            {
                return filter;

                void filter(T value)
                {
                    if (predicate(value))
                    {
                        emitterB(value);
                    }
                }
            }
        }

        public static ILine<TResult> Map<T, TResult>(this ILine<T> lineA, Func<T, TResult> selector)
        {
            return MakeExtension<T, TResult>(lineA, adapter);

            Action<T> adapter(Action<TResult> emitterB)
            {
                return map;

                void map(T value)
                {
                    emitterB(selector(value));
                }
            }
        }

        public static ILine<TResult> Scan<T, TResult>(this ILine<T> lineA, Func<TResult, T, TResult> accumulator, TResult seed)
        {
            return MakeExtension<T, TResult>(lineA, adapter);

            Action<T> adapter(Action<TResult> emitterB)
            {
                var produce = seed;
                return scan;

                void scan(T value)
                {
                    produce = accumulator(produce, value);
                    emitterB(produce);
                }
            }
        }

        public static ILine<T> Merge<T>(params ILine<T>[] lineArray)
        {
            var emitterB = new LoadEmitter<T>();
            Action[] disconnectArray = Array.Empty<Action>();

            emitterB.BeLoad = OnBeLoad;
            emitterB.BeUnload = OnBeUnload;

            return emitterB.Line;

            void OnBeLoad()
            {
                disconnectArray = lineArray.Select(line => line.Connect(emitterB.Emit)).ToArray();
            }

            void OnBeUnload()
            {
                foreach (var disconnect in disconnectArray)
                {
                    disconnect();
                }
            }
        }

        public static ILine<TResult> MakeExtension<T, TResult>(ILine<T> lineA, Func<Action<TResult>, Action<T>> adapter)
        {
            var emitterB = new LoadEmitter<TResult>();
            var receiverAdapter = adapter(emitterB.Emit);
            Action disconnect = delegate { };

            emitterB.BeLoad = OnBeLoad;
            emitterB.BeUnload = OnBeUnload;

            return emitterB.Line;

            void OnBeLoad()
            {
                disconnect = lineA.Connect(receiverAdapter);
            }

            void OnBeUnload()
            {
                disconnect();
            }
        }
    }
}
