using System;

namespace HotEmitter
{
    public interface ILine<T>
    {
        Action Connect(Action<T> reciver);
    }
}
