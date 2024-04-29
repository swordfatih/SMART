namespace Game
{
    public class BoardData(List<string> names, int day)
    {
        public readonly List<string> Names = names;
        public int Day = day;
    }
}