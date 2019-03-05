using System;
using System.Collections.Generic;

namespace HotEmitter
{
    public class Emitter<T>
    {
        public Emitter()
        {
            Line = new Line<T>(AddReceiver);
        }

        public void Deconstruct(out Action<T> emit, out ILine<T> line)
        {
            emit = Emit;
            line = Line;
        }

        public void Emit(T value)
        {
            var exList = new List<Exception>();
            foreach (var receiver in ReceiverSet)
            {
                try
                {
                    receiver(value);
                }
                catch (Exception ex)
                {
                    exList.Add(ex);
                }
            }
            if (exList.Count != 0)
            {
                throw new AggregateException(exList.ToArray());
            }
        }

        public ILine<T> Line { get; }

        internal protected virtual Action AddReceiver(Action<T> receiver)
        {
            ReceiverSet.Add(receiver);
            return RemoveReceiver;

            void RemoveReceiver()
            {
                ReceiverSet.Remove(receiver);
            }
        }

        internal protected readonly HashSet<Action<T>> ReceiverSet = new HashSet<Action<T>>();
    }
}
