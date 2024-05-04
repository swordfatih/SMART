using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_MessageBlock : MonoBehaviour, IObserver<Choice>
{
    public GameObject PF_Message;
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

    public void SetMessage(Choice choice)
    {
        if (Block != null)
        {
            GameObject.Destroy(Block);
        }

        Block = Instantiate(PF_Message, IT_Canvas.transform);

        var message = Block.GetComponentInChildren<TMP_Text>();
        message.text = choice.Value;

        var button = Block.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => GameObject.Destroy(Block));
    }

    public void Notify(Choice choice)
    {
        SetMessage(choice);
    }
}
