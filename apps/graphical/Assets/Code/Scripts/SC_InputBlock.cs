using System.Collections;
using Game;
using Interface;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_InputBlock : MonoBehaviour, IObserver<UserInput>
{
    public GameObject PF_Input;
    public GameObject Canvas { get; set; } = null;
    public GameObject Block { get; set; } = null;
    public UserInput LastInput { get; set; } = null;
    public string Message { get; set; } = null ;

    public void Start()
    {
        GameManager.Instance.Subscribe(this);
    }

    public void Update()
    {
        if (Canvas == null)
        {
            Canvas = GameObject.Find("Canvas");

            if (LastInput != null)
            {
                SetInput(LastInput);
            }
        }
    }

    public void OnDestroy()
    {
        GameManager.Instance.Unsubscribe(this);
    }

    public void SetInput(UserInput Input)
    {
        LastInput = Input;

        if (Block != null)
        {
            Destroy(Block);
        }

        Block = Instantiate(PF_Input, Canvas.transform);
        Block.transform.localPosition = new Vector3(0, 0, 0);

        var input = Block.GetComponentInChildren<TMP_InputField>();
        input.onValueChanged.AddListener((message) =>
        {
            Message = message;
        });

        var button = Block.GetComponentInChildren<Button>();
        button.onClick.AddListener(() =>
        {
            Send(Message);
        }); 
    }

    public void Notify(UserInput Input)
    {
        SetInput(Input);
    }

    public void Send(string message)
    {
        GameManager.Instance.Client.Node.Send(RequestType.Input, message);
        GameObject.Destroy(Block);
        LastInput = null;
        Block = null;
    }
}
