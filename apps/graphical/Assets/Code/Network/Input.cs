namespace Interface
{
  public class Input
  {
    public string Instruction { get; set; }

    public Input(string instruction)
    {
      Instruction = instruction;
    }
  }
}//remplacer message par input, enlever  originee
//set message devient set input, modifier le prefab pour afficher l'instruction, ajouter un bouton ou une entrée qui permet d'envoyer le message pour récupérer le input,
//ça va send() (exemple dans choiceBlock)