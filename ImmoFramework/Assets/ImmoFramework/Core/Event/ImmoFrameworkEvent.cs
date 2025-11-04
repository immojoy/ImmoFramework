
using System;



namespace Immo.Framework.Core.Event
{
    public abstract class ImmoFrameworkEvent
    {
        public bool IsCancelled { get; private set; }
        public DateTime Timestamp { get; private set; }
        public object Source { get; private set; }


        protected ImmoFrameworkEvent(object source)
        {
            Source = source;
            Timestamp = DateTime.Now;
            IsCancelled = false;
        }

        public void Cancel()
        {
            if (!IsCancellable())
            {
                throw new Exception("Trying to cancel a non-cancellable event.");
            }

            IsCancelled = true;
        }

        protected virtual bool IsCancellable()
        {
            return true;
        }
    }
}