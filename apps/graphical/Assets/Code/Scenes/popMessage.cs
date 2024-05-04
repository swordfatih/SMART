using Network;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class popMessage : MonoBehaviour, IObserver<Question>
{
    public Button answerPrefab;
    public GameObject choicePrefab;

    public void Start()
    {
        GameManager.Instance.Subscribe(this);
    }

    public void OnAnswerButtonClicked(int answerIndex)
    {
        Debug.Log("Réponse choisie : " + answerIndex);
        GameManager.Instance.Client.Node.Send(RequestType.Choice, answerIndex.ToString());
        var canvas = GameObject.Find("Demande_Choix");
        GameObject.Destroy(canvas);
    }

    public void SetMessage(Question question)
    {
        var canvas = GameObject.Find("Canvas");
        var message = Instantiate(choicePrefab, canvas.transform);
        var script = message.GetComponent<popMessage>();

        var titleMessage = message.GetComponentInChildren<TMP_Text>();
        titleMessage.text = question.Value;

        var choix_object = GameObject.Find("Choix");

        for (int i = 0; i < question.Answers.Count; ++i)
        {
            Button choice = Instantiate(answerPrefab);
            choice.transform.SetParent(choix_object.transform);
            choice.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            choice.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            choice.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            choice.GetComponentInChildren<TMP_Text>().text = question.Answers[i];

            // Ajout de l'événement OnClick
            choice.onClick.AddListener(() => OnAnswerButtonClicked(i));
        }
    }

    public void Notify(Question question)
    {
        SetMessage(question);
    }
}
