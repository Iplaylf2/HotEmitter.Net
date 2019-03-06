using System;
using System.Collections.Generic;
using System.Text;

namespace HotEmitter
{
    internal class LoadEmitter<T> : Emitter<T>
    {
        public Action BeLoad { get; set; }
        public Action BeUnload { get; set; }

        protected internal override Action AddReceiver(Action<T> receiver)
        {
            var unloadBefore = ReceiverSet.Count == 0;
            var remove = base.AddReceiver(receiver);
            if (unloadBefore)
            {
                BeLoad();
            }

            return removeReceiver;

            void removeReceiver()
            {
                var loadBefore = ReceiverSet.Count != 0;
                remove();
                var unload = ReceiverSet.Count == 0;
                if (unload && loadBefore)
                {
                    BeUnload();
                }
            }
        }
    }
}
