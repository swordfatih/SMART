using Network;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_ChoiceBlock : MonoBehaviour, IObserver<Choice>
{
    public Button PF_Answer;
    public GameObject PF_Choice;
    public GameObject IT_Canvas;
    public GameObject Block { get; set; } = null;

    public void Start()
    {
        GameManager.Instance.Subscribe(this);
    }

    public void OnDestroy()
    {
        GameManager.Instance.Unsubscribe(this);
    }

    public void OnAnswerButtonClicked(int answerIndex)
    {
        GameManager.Instance.Client.Node.Send(RequestType.Choice, answerIndex.ToString());
        GameObject.Destroy(Block);
    }

    public void SetChoice(Choice choice)
    {
        if (Block != null)
        {
            GameObject.Destroy(Block);
        }

        Block = Instantiate(PF_Choice, IT_Canvas.transform);

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
            answer.onClick.AddListener(() => OnAnswerButtonClicked(index));
        }
    }

    public void Notify(Choice choice)
    {
        SetChoice(choice);
    }
}
