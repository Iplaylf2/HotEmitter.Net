using System;

namespace HotEmitter
{
    public class Emitter<T>
    {
        public void Emit<T>()
        {
        }

        public Line<T> Line { get; }
    }
}
