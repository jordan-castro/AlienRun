using System;

namespace Observers
{
    public interface IObserver
    {
        void Update(object data);
    }
}