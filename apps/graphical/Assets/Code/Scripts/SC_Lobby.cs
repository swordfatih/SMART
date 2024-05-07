using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Interface;
using System.Net.Sockets;
using Network;
using UnityEngine.SceneManagement;
using Game;

public class SC_Lobby : MonoBehaviour, IObserver<ServerData>
{
    public int iaNb;
    public int iaCount;
    public Canvas canvas; // Le Canvas où ajouter le prefab
    public GameObject iaPrefab; // Le prefab à ajouter au Canvas
    public GameObject addButton;
    public Transform iaPanel;
    public Transform playerPanel;
    public GameObject playerPrefab;
    public GameObject IT_BotSelect;

    // Start is called before the first frame update
    public void Start()
    {
        iaNb = 0;
        iaCount = 0;

        GameManager.Instance.Subscribe(this);
        GameManager.Instance.Client.Node.Send(RequestType.NotifyServer);

        if (GameManager.Instance.Admin == false)
        {
            var IA_add = GameObject.Find("Add");
            IA_add.SetActive(false);
        }
    }

    public void OnDestroy()
    {
        GameManager.Instance.Unsubscribe(this);
    }

    public GameObject AddIA(string name)
    {
        var IA_object = GameObject.Find("IAContainer");

        GameObject newIA = Instantiate(iaPrefab);
        newIA.transform.SetParent(IA_object.transform);
        newIA.transform.localScale = new Vector3(1, 1, 1);
        newIA.transform.localPosition = new Vector3(0, 0, 0);

        if (GameManager.Instance.Admin == true)
        {
            Button removeButton = newIA.GetComponentInChildren<Button>();
            removeButton.onClick.AddListener(() => RemoveIAButton(newIA));
        }
        else
        {
            Button removeButton = newIA.GetComponentInChildren<Button>();
            removeButton.gameObject.SetActive(false);
        }

        TMP_Text iaName = newIA.GetComponentInChildren<TMP_Text>();
        iaName.text = name;

        return newIA;
    }

    public void AddIAButton()
    {
        if (GameManager.Instance.Admin == true)
        {
            if (iaCount < 8)
            {
                iaNb++;
                iaCount++;

                var Label = IT_BotSelect.GetComponent<TMP_Text>();
                var name = Label.text + "_" + iaNb.ToString();

                GameManager.Instance.Server.Bots.Add(Label.text == "Random" ? new RandomClient(name) : new DecisionClient(name));
                GameManager.Instance.Server.Notify();
            }
            else
            {
                GameManager.Instance.Notify(new Message("Server", "You can't add more than 8 IA"));
                AudioManager.Instance.PlaySound("Error");
            }
        }
    }
    public void RemoveIAButton(GameObject iaToRemove)
    {
        if (GameManager.Instance.Admin == true)
        {
            var name = iaToRemove.GetComponentInChildren<TMP_Text>().text;
            iaCount--;
            GameManager.Instance.Server.Bots.RemoveAll(bot => bot.Name == name);
            GameManager.Instance.Server.Notify();
        }
    }

    public void RemovePlayerButton(GameObject playerToRemove)
    {
        TMP_Text pseudoText = playerToRemove.GetComponentInChildren<TMP_Text>();
        if (pseudoText == null)
        {
            Debug.LogError("TMP_Text component not found on playerToRemove");
            return;
        }

        string playerName = pseudoText.text;

        TcpClient keyToRemove = null;

        foreach (var entry in GameManager.Instance.Server.Clients)
        {
            NetworkClient networkClient = entry.Value;
            if (networkClient != null && networkClient.Name == playerName)
            {
                keyToRemove = entry.Key;
                break;
            }
        }

        if (keyToRemove != null)
        {
            GameManager.Instance.Server.Clients.TryRemove(keyToRemove, out _);
            Debug.Log($"Removed player {playerName}");
        }
        else
        {
            Debug.Log($"Player {playerName} not found");
        }

        Destroy(playerToRemove);
    }

    public void ClickStartButton()
    {
        AudioManager.Instance.PlaySound("Select");
        GameManager.Instance.Client.Node.Send(RequestType.Start);
    }

    public void Notify(ServerData value)
    {
        GameObject[] playerPrefabs = GameObject.FindGameObjectsWithTag("Pseudo");

        // Destroy each player prefab found
        foreach (GameObject playerPrefab in playerPrefabs)
        {
            Destroy(playerPrefab);
        }

        var element_object = GameObject.Find("Element");

        if (element_object is null)
        {
            return;
        }

        foreach (var client in value.Clients)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.SetParent(element_object.transform);
            newPlayer.transform.localScale = new Vector3(1, 1, 1);
            newPlayer.transform.localPosition = new Vector3(0, 0, 0);

            if (GameManager.Instance.Admin == true && client != GameManager.Instance.Client.Name)
            {
                Button removePlayer = newPlayer.GetComponentInChildren<Button>();
                removePlayer.onClick.AddListener(() => RemovePlayerButton(newPlayer));
            }
            else
            {
                Button removePlayer = newPlayer.GetComponentInChildren<Button>();
                removePlayer.gameObject.SetActive(false);
            }

            TMP_Text pseudo = newPlayer.GetComponentInChildren<TMP_Text>();
            pseudo.text = client;
        }

        var IA_object = GameObject.Find("IAContainer");

        foreach (var ia in IA_object.GetComponentsInChildren<Transform>())
        {
            if (ia != IA_object.transform)
            {
                Destroy(ia.gameObject);
            }
        }

        foreach (var bot in value.Bots)
        {
            AddIA(bot);
        }
    }
}



