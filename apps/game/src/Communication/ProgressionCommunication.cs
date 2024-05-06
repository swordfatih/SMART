namespace Game
{
    public class ProgressionCommunication : Communication
    {
        public readonly Choice Choice;

        public ProgressionCommunication(Player origin, Direction direction, Choice choice) : base(origin, direction)
        {
            Choice = choice;
        }
    }
}