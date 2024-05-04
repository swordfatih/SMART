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
    public int yValue;
    public int yValuePlayer;
    public int spacing = 38;
    public int playerSpacing = 32;
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
        yValue = 400;
        yValuePlayer= 400;


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
            
            //newPlayer.transform.localScale=new Vector3(5.134977f, 1.499191f, 0.9741842f);

            //TO DO : modifications dans le server : kick le joueur
            Button removePlayer = newPlayer.GetComponentInChildren<Button>();
            removePlayer.onClick.AddListener(() => removePlayerButton(newPlayer));

            NetworkClient? networkClient = client.Value;
            TMP_Text pseudo = newPlayer.GetComponentInChildren<TMP_Text>();
            pseudo.text = networkClient.Name;
            //Debug.Log(networkClient.Name);

        }


    }

    public void AddIAButton()
    {
        if (GameManager.Instance.Admin == true)
        {
            if (iaMembers.Count < 8)
            { //pour l'affichage
                iaNb = iaNb + 1;

                GameObject newIA = Instantiate(iaPrefab, new Vector3(860, yValue, 0), Quaternion.identity, iaPanel);
                Button removeButton = newIA.GetComponentInChildren<Button>();
                TMP_Text iaName = newIA.GetComponentInChildren<TMP_Text>();

                removeButton.onClick.AddListener(() => removeIAButton(newIA));
                var name= "IA " + (iaNb).ToString();
                iaName.text = name;

                yValue -= spacing;
                iaMembers.Add(newIA);

                GameManager.Instance.Bots.Add(new RandomClient(name));


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
            yValue -= spacing;

            for (int i = index; i < iaMembers.Count; i++)
            {
                GameObject iaMember = iaMembers[i];
                Vector3 newPosition = iaMember.transform.position;
                newPosition.y += spacing; 
                iaMember.transform.position = newPosition;

                /*
                TMP_Text iaName = iaMember.GetComponentInChildren<TMP_Text>();
                if (iaName != null)
                {
                    iaName.text = "IA " + (i + 1).ToString();
                }*/
            }
            yValue = 400 - spacing * iaMembers.Count;
            var name=iaToRemove.GetComponentInChildren<TMP_Text>();
            var bot=GameManager.Instance.Bots.Find(x=> x.Name==name.text);
            GameManager.Instance.Bots.Remove(bot);

            Destroy(iaToRemove);
        }
    }

    public void removePlayerButton(GameObject playerToRemove)
    {
        //GameManager.Instance.Server.Clients.Remove();
        Destroy(playerToRemove);
    }
}



