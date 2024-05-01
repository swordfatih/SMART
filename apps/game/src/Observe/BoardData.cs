using System.Collections.Generic;

namespace Game
{
    public class BoardData
    {
        public List<string> Names { get; set; }
        public int Day { get; set; }

        public BoardData(List<string> names, int day)
        {
            Names = names;
            Day = day;
        }
    }
}