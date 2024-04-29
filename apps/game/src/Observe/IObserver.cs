namespace Game
{
    public interface IObserver<T>
    {
        public void Notify(T value);
    }
}