using Network;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class popMessage : MonoBehaviour
{
    public TMP_Text titleMessage;
    public Button choicePrefab;
    public Node Node { get; }
    // Start is called before the first frame update

    public void OnAnswerButtonClicked(int answerIndex)
    {
        Debug.Log("Réponse choisie : " + answerIndex);
        GameManager.Instance.Client.Node.Send(RequestType.Choice, answerIndex.ToString());

        var choix_object = GameObject.Find("Choix");

        foreach (Transform child in choix_object.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject canvas = GameObject.Find("Demande_Choix");
        canvas.SetActive(false);
    }

    public void SetMessage(Question question)
    {
        GameObject canvas = GameObject.Find("Demande_Choix");
        canvas.SetActive(true);

        titleMessage.text = question.Value;
    
        var choix_object = GameObject.Find("Choix");

        for (int i = 0; i < question.Answers.Count; ++i)
        {
            Button choice = Instantiate(choicePrefab);
            choice.transform.SetParent(choix_object.transform);
            choice.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            choice.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

            choice.GetComponentInChildren<TMP_Text>().text = question.Answers[i];

            // Ajout de l'événement OnClick
            choice.onClick.AddListener(() => OnAnswerButtonClicked(i));
        }
    }
}
