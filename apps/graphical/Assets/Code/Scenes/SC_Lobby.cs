using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Interface;
using System.Collections.Concurrent;
using System.Net.Sockets;
using Network;
using UnityEngine.SceneManagement;
using Game;

public class SC_Lobby : MonoBehaviour, IObserver<ServerData>
{
    public int iaNb;
    public Canvas canvas; // Le Canvas où ajouter le prefab
    public GameObject iaPrefab; // Le prefab à ajouter au Canvas
    public GameObject addButton;
    public Transform iaPanel;
    public Transform playerPanel;
    public GameObject playerPrefab;
    private List<GameObject> iaMembers = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {
        iaNb = 0;

        GameManager.Instance.Subscribe(this);
        GameManager.Instance.Client.Node.Send(RequestType.NotifyServer);
    }

    public void AddIAButton()
    {
        if (GameManager.Instance.Admin == true)
        {
            if (iaMembers.Count < 8)
            { //pour l'affichage
                iaNb = iaNb + 1;
                var IA_object = GameObject.Find("IAContainer");

                GameObject newIA = Instantiate(iaPrefab);
                newIA.transform.SetParent(IA_object.transform);

                Button removeButton = newIA.GetComponentInChildren<Button>();
                TMP_Text iaName = newIA.GetComponentInChildren<TMP_Text>();

                removeButton.onClick.AddListener(() => RemoveIAButton(newIA));
                var name = "IA " + iaNb.ToString();
                iaName.text = name;

                iaMembers.Add(newIA);

                GameManager.Instance.Server.Bots.Add(new RandomClient(name));
            }
            else
            {
                Debug.Log("You can't add more than 8 IA.");
            }
        }
    }
    public void RemoveIAButton(GameObject iaToRemove)
    {
        int index = iaMembers.IndexOf(iaToRemove);
        if (index != -1)
        {
            iaMembers.RemoveAt(index);

            var name = iaToRemove.GetComponentInChildren<TMP_Text>();
            var bot = GameManager.Instance.Server.Bots.Find(x => x.Name == name.text);
            GameManager.Instance.Server.Bots.Remove(bot);

            Destroy(iaToRemove);
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
        GameManager.Instance.Client.Node.Send(RequestType.Start);
        SceneManager.LoadScene("SC_Prison_Inside");
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

        if(element_object is null)
        {
            return;
        }

        foreach (var client in value.Clients)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.SetParent(element_object.transform);

            if (GameManager.Instance.Admin)
            {
                Button removePlayer = newPlayer.GetComponentInChildren<Button>();
                removePlayer.onClick.AddListener(() => RemovePlayerButton(newPlayer));
            }
            else
            {
                Button removePlayer = newPlayer.GetComponentInChildren<Button>();
                Destroy(removePlayer);
            }

            TMP_Text pseudo = newPlayer.GetComponentInChildren<TMP_Text>();
            pseudo.text = client;
        }
    }
}



