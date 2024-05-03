using UnityEngine;

public class SC_Interface : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("Interface started.");
    }

    public void Update()
    {
        if (GameManager.Instance.Client != null)
        {
            GameManager.Instance.Client.Handle();
        }
    }
}