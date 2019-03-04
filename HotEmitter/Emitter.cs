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
        public void Emit(T value)
        {
            foreach (var action in ReceiverSet)
            {
                try
                {
                    action(value);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public Line<T> Line { get; }

        internal Action AddReceiver(Action<T> action, Action<Exception> catchAction = null)
        {
            ReceiverSet.Add(action);

            void RemoveReceiver()
            {
                ReceiverSet.Remove(action);
            }
            return RemoveReceiver;
        }

        private HashSet<Action<T>> ReceiverSet = new HashSet<Action<T>>();
    }
}
