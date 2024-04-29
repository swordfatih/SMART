namespace Game
{
    public interface IObservable<T>
    {
        public void Subscribe(IObserver<T> observer);
    }
}