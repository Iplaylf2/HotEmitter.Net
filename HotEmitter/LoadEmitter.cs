using System;
using System.Collections.Generic;
using System.Text;

namespace HotEmitter
{
    internal class LoadEmitter<T> : Emitter<T>
    {
        public ILine<Int32> BeLoadLine => BeLoadEmitter.Line;
        public ILine<Int32> BeUnloadLine => BeUnloadEmitter.Line;

        protected internal override Action AddReceiver(Action<T> receiver)
        {
            var unload = ReceiverSet.Count == 0;
            var remove = base.AddReceiver(receiver);
            if (unload)
            {
                BeLoadEmitter.Emit(0);
            }

            return removeReceiver;

            void removeReceiver()
            {
                var load = ReceiverSet.Count != 0;
                remove();
                if (ReceiverSet.Count == 0 && load)
                {
                    BeUnloadEmitter.Emit(0);
                }
            }
        }

        private readonly Emitter<Int32> BeLoadEmitter = new Emitter<int>();
        private readonly Emitter<Int32> BeUnloadEmitter = new Emitter<int>();
    }
}
