using Network;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_ChoiceBlock : MonoBehaviour, IObserver<Question>
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
        Debug.Log("Réponse choisie : " + answerIndex);
        GameManager.Instance.Client.Node.Send(RequestType.Choice, answerIndex.ToString());
        GameObject.Destroy(Block);
    }

    public void SetMessage(Question question)
    {
        if (Block != null)
        {
            GameObject.Destroy(Block);
        }

        Block = Instantiate(PF_Choice, IT_Canvas.transform);

        var titleMessage = Block.GetComponentInChildren<TMP_Text>();
        titleMessage.text = question.Value;

        var choix_object = GameObject.Find("Choix");

        for (int i = 0; i < question.Answers.Count; ++i)
        {
            var choice = Instantiate(PF_Answer);
            choice.transform.SetParent(choix_object.transform);
            choice.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            choice.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            choice.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            choice.GetComponentInChildren<TMP_Text>().text = question.Answers[i];

            // Ajout de l'événement OnClick
            int index = i;
            choice.onClick.AddListener(() => OnAnswerButtonClicked(index));
        }
    }

    public void Notify(Question question)
    {
        SetMessage(question);
    }
}
