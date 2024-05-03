using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class popMessage : MonoBehaviour
{
    public TMP_Text titleMessage;
    public Button choicePrefab;
    // Start is called before the first frame update
    public void SetMessage(Question question)
    {
        
        titleMessage.text = question.Value;
    
        var choix_object = GameObject.Find("Choix");

        for (int i = 0; i < question.Answers.Count; ++i)
        {
            Button choice = Instantiate(choicePrefab);
            choice.transform.SetParent(choix_object.transform);

            choice.GetComponentInChildren<TMP_Text>().text = question.Answers[i];
        }
    }
}
