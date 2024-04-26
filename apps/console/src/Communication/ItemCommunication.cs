namespace Board
{
    public class ItemCommunication(Player origin, Direction direction, Item item) : Communication(origin, direction)
    {
        public readonly Item Item = item;
    }
}