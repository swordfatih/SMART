using System;
using System.Linq;

namespace Network
{
  public class Packet
  {
    public RequestType Request { get; }
    public string[] Content { get; }

    public readonly static string CS = "<|CS|>"; // Content separator
    public readonly static string EOP = "<|EOP|>"; // End of packet

    public Packet(RequestType request, string[] content)
    {
      Request = request;
      Content = content;
    }

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
      var parts = packet.Split(new string[] { CS }, StringSplitOptions.None);
      var request = parts[0];
      var content = parts.Length > 1 ? parts.Skip(1).ToArray() : new string[0];

      return new Packet((RequestType)Enum.Parse(typeof(RequestType), request), content);
    }
  }
}