using System;
using System.Collections.Generic;
using System.Text;

namespace HotEmitter
{
    public class ExtensionLine<T> : Line<T>
    {
        internal ExtensionLine(Emitter<T> emitter)
            : base(emitter)
        {
        }
    }
}
