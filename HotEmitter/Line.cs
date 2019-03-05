using System;
using System.Collections.Generic;
using System.Text;

namespace HotEmitter
{
    class Line<T> : ILine<T>
    {
        public Line(Func<Action<T>, Action> addReceiver)
        {
            AddReceiver = addReceiver;
        }

        public virtual Action Connect(Action<T> receiver)
        {
            return AddReceiver(receiver);
        }

        private readonly Func<Action<T>, Action> AddReceiver;
    }
}
