using Network;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_ChoiceBlock : MonoBehaviour, IObserver<Choice>
{
    public GameObject PF_Answer;
    public GameObject PF_Choice;
    public GameObject Canvas { get; set; } = null;
    public GameObject Block { get; set; } = null;
    public Choice LastChoice { get; set; } = null;

    public void Start()
    {
        GameManager.Instance.Subscribe(this);
    }

    public void Update()
    {
        if (Canvas == null)
        {
            Canvas = GameObject.Find("Canvas");

            if (LastChoice != null)
            {
                SetChoice(LastChoice);
            }
        }
    }

    public void OnDestroy()
    {
        GameManager.Instance.Unsubscribe(this);
    }

    public void OnAnswered(int answerIndex)
    {
        GameManager.Instance.Client.Node.Send(RequestType.Choice, answerIndex.ToString());
        GameObject.Destroy(Block);
        LastChoice = null;
        Block = null;
    }

    public void SetChoice(Choice choice)
    {
        LastChoice = choice;

        if (Block != null)
        {
            GameObject.Destroy(Block);
        }

        Block = Instantiate(PF_Choice, Canvas.transform);

        var titleMessage = Block.GetComponentInChildren<TMP_Text>();
        titleMessage.text = choice.Value;

        var choix_object = GameObject.Find("Choix");

        for (int i = 0; i < choice.Answers.Count; ++i)
        {
            var answer = Instantiate(PF_Answer);
            answer.transform.SetParent(choix_object.transform);
            answer.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            answer.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            answer.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            answer.GetComponentInChildren<TMP_Text>().text = choice.Answers[i];

            int index = i;
            answer.GetComponent<Button>().onClick.AddListener(() => OnAnswered(index));
        }
    }

    public void Notify(Choice choice)
    {
        SetChoice(choice);
    }
}
