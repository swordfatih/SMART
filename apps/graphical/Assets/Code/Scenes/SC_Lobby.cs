using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Interface;
using System.Collections.Concurrent;
using System.Net.Sockets;
using Network;

public class SC_Lobby : MonoBehaviour
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
    void Start()
    {
        iaNb = 0;

    }

    // Update is called once per frame
    void Update()
    {
        ConcurrentDictionary<TcpClient, NetworkClient?>  clientList =GameManager.Instance.Server.Clients;


        GameObject[] playerPrefabs = GameObject.FindGameObjectsWithTag("Pseudo");

        // Destroy each player prefab found
        foreach (GameObject playerPrefab in playerPrefabs)
        {
            Destroy(playerPrefab);
        }

        var element_object = GameObject.Find("Element");

         foreach (var client in clientList)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            

            newPlayer.transform.SetParent(element_object.transform);

            //TO DO : modifications dans le server : kick le joueur
            Button removePlayer = newPlayer.GetComponentInChildren<Button>();
            removePlayer.onClick.AddListener(() => removePlayerButton(newPlayer));

            NetworkClient? networkClient = client.Value;
            TMP_Text pseudo = newPlayer.GetComponentInChildren<TMP_Text>();
            pseudo.text = networkClient.Name;

        }


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

                removeButton.onClick.AddListener(() => removeIAButton(newIA));
                var name= "IA " + (iaNb).ToString();
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
    public void removeIAButton(GameObject iaToRemove)
    {
        int index = iaMembers.IndexOf(iaToRemove);
        if (index != -1)
        {
            iaMembers.RemoveAt(index);

            var name=iaToRemove.GetComponentInChildren<TMP_Text>();
            var bot=GameManager.Instance.Server.Bots.Find(x=> x.Name==name.text);
            GameManager.Instance.Server.Bots.Remove(bot);

            Destroy(iaToRemove);
        }
    }

    public void removePlayerButton(GameObject playerToRemove)
    {
        //GameManager.Instance.Server.Clients.Remove();
        Destroy(playerToRemove);
    }
}



