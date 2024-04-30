using System.Text;

namespace Network
{
  public class Packet(RequestType request, string[] content)
  {
    public RequestType Request { get; } = request;
    public string[] Content { get; } = content;

    public readonly static string CS = "<|CS|>"; // Content separator
    public readonly static string EOP = "<|EOP|>"; // End of packet

    public override string ToString()
    {
      string packet = Request.ToString();

      foreach (var content in Content)
      {
        packet += CS + content;
      }

      packet += EOP;

      return packet;
    }

    public static Packet FromString(string packet)
    {
      var parts = packet.Split(CS);
      var request = parts[0];
      var content = parts.Length > 1 ? parts[1..] : [];

      return new Packet((RequestType)Enum.Parse(typeof(RequestType), request), content);
    }
  }
}