namespace Game
{
    public class ChoiceCommunication : Communication
    {
        public readonly Choice Choice;

        public ChoiceCommunication(Player origin, Direction direction, Choice choice) : base(origin, direction)
        {
            Choice = choice;
        }
    }
}