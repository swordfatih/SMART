namespace Game
{
    public class ItemCommunication : Communication
    {
        public readonly Item Item;

        public ItemCommunication(Player origin, Direction direction, Item item) : base(origin, direction)
        {
            Item = item;
        }
    }
}