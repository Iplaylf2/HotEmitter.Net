using System;
using System.Collections.Generic;
using System.Text;

namespace HotEmitter
{
    public class Line<T>
    {
        public Line(Emitter<T> emitter)
        {
        }

        public Action Connect(Action<T> action)
        {
        }
    }
}
