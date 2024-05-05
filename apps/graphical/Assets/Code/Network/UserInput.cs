namespace Interface
{
  public class UserInput
  {
    public string Instruction { get; set; }

    public UserInput(string instruction)
    {
      Instruction = instruction;
    }
  }
}