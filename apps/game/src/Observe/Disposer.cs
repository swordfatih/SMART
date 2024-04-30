using System;
using System.Collections.Generic;

namespace Game
{
    public class Disposer<T> : IDisposable
    {
        public List<IObserver<T>> Observers { get; }
        public IObserver<T> Observer { get; }

        public Disposer(List<IObserver<T>> observers, IObserver<T> observer)
        {
            Observers = observers;
            Observer = observer;
        }

        public void Dispose()
        {
            Observers.Remove(Observer);
        }
    }
}