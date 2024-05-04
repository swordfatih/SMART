namespace Interface
{
  public class Message
  {
    public string Origin { get; set; }
    public string Value { get; set; }

    public Message(string origin, string value)
    {
      Origin = origin;
      Value = value;
    }
  }
}