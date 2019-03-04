using System;
using System.Collections.Generic;
using System.Text;

namespace HotEmitter
{
    public class Line<T>
    {
        public Line(Func<Action<T>, Action> addReceiver)
        {
            AddReceiver = addReceiver;
        }

        public virtual Action Connect(Action<T> action)
        {
            return AddReceiver(action);
        }

        private readonly Func<Action<T>, Action> AddReceiver;
    }
}
