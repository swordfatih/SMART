namespace Player
{
    public class ItemCommunication(Player origin, Direction direction, Request request, Item item) : Communication(origin, direction, request)
    {
        public readonly Item Item = item;
    }
}