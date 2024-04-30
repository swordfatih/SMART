using System;
using System.Collections.Generic;

namespace Game
{
    public class Disposer<T>(List<IObserver<T>> observers, IObserver<T> observer) : IDisposable
    {
        public void Dispose()
        {
            observers.Remove(observer);
        }
    }
}