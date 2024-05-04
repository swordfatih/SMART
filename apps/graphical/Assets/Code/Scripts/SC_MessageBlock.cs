using System.Collections;
using Game;
using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_MessageBlock : MonoBehaviour, IObserver<Message>
{
    public GameObject PF_Message;
    public GameObject Canvas { get; set; } = null;
    public GameObject Block { get; set; } = null;
    public Message LastMessage { get; set; } = null;

    public void Start()
    {
        GameManager.Instance.Subscribe(this);
    }

    public void Update()
    {
        if (Canvas == null)
        {
            Canvas = GameObject.Find("Canvas");

            if (LastMessage != null)
            {
                SetMessage(LastMessage);
            }
        }
    }

    public void OnDestroy()
    {
        GameManager.Instance.Unsubscribe(this);
    }

    public IEnumerator Close(float time)
    {
        yield return new WaitForSeconds(time);

        if (Block != null)
        {
            Destroy(Block);
            Block = null;
        }

        LastMessage = null;
    }

    public void SetMessage(Message message)
    {
        LastMessage = message;

        if (Block != null)
        {
            Destroy(Block);
        }

        Block = Instantiate(PF_Message, Canvas.transform);

        var text = Block.GetComponentInChildren<TMP_Text>();
        text.text = $"You received a message from {message.Origin}:\n{message.Value}";

        var button = Block.GetComponentInChildren<Button>();
        button.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            StartCoroutine(Close(0f));
        });
    }

    public void Notify(Message message)
    {
        SetMessage(message);

        StopAllCoroutines();
        StartCoroutine(Close(5.0f));
    }
}
