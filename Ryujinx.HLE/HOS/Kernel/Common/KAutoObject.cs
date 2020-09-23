using System.Threading;

namespace Ryujinx.HLE.HOS.Kernel.Common
{
    class KAutoObject
    {
        protected KernelContext KernelContext;

        private int _referenceCount;

        public KAutoObject(KernelContext context)
        {
            KernelContext = context;

            _referenceCount = 1;
        }

        public virtual KernelResult SetName(string name)
        {
            if (!KernelContext.AutoObjectNames.TryAdd(name, this))
            {
                return KernelResult.InvalidState;
            }

            return KernelResult.Success;
        }

        public static KernelResult RemoveName(KernelContext context, string name)
        {
            if (!context.AutoObjectNames.TryRemove(name, out _))
            {
                return KernelResult.NotFound;
            }

            return KernelResult.Success;
        }

        public static KAutoObject FindNamedObject(KernelContext context, string name)
        {
            if (context.AutoObjectNames.TryGetValue(name, out KAutoObject obj))
            {
                return obj;
            }

            return null;
        }

        public void IncrementReferenceCount()
        {
            Interlocked.Increment(ref _referenceCount);
        }

        public void DecrementReferenceCount()
        {
            if (Interlocked.Decrement(ref _referenceCount) == 0)
            {
                Destroy();
            }
        }

        protected virtual void Destroy() { }
    }
}