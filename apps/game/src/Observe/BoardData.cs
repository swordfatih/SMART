using System.Collections.Generic;

namespace Game
{
    public class BoardData
    {
        public readonly List<string> Names;
        public int Day;

        public BoardData(List<string> names, int day)
        {
            Names = names;
            Day = day;
        }
    }
}