using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using System.Net.Sockets;
using Network;
using Game;
using TMPro;
using Unity.VisualScripting;

public class SC_HUD : MonoBehaviour, IObserver<PlayerData>, IObserver<BoardData> 
{

    public PlayerData PlayerData {get; set;}
    public BoardData BoardData {get; set;} 
    public GameObject PF_Guard;
    public GameObject PF_Associate;
    public GameObject PF_Inmate;
    public GameObject PF_Shovel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Subscribe(IObserver<BoardData>(this));
        GameManager.Instance.Subscribe(IObserver<PlayerData>(this));

        GameObject rolePlace = GameObject.Find("Role");

        if(PlayerData.Player.Role.ToString() == "Associate"){

            var associate = Instantiate(PF_Associate);
            associate.transform.SetParent(rolePlace.transform);

        }else{

            var inmate = Instantiate(PF_Inmate);
            inmate.transform.SetParent(rolePlace.transform);
        }

    }
 
    public void Notify (BoardData board){

        BoardData = board;
        UpdateHUD();
    }

    public void Notify (PlayerData playerData){

        PlayerData = playerData;
        UpdateHUD();

    }

    public void UpdateHUD() {

        GameObject shovelPlace= GameObject.Find("avancement");

        //Update de l'anvancement
        Transform[] shovelPrefabs = shovelPlace.GetComponentsInChildren<Transform>();
        foreach (Transform shovel in shovelPrefabs)
        {
            GameObject.Destroy(shovel.gameObject);
        }
        for (int i = 0; i < PlayerData.Player.Progression; ++i){
            var shovel = Instantiate(PF_Shovel);
            shovel.transform.SetParent(shovelPlace.transform);
            shovel.GetComponent<RectTransform>().localPosition = new Vector3(130 + (i+1)*50, 60, 0);
        }


        //Update des Jours
        var jours = GameObject.Find("jours");
        var nbJours = jours.GetComponentInChildren<TMP_Text>();
        nbJours.text = (BoardData.Day).ToString();

        //Update de la présence du gardien
        GameObject guardPlace = GameObject.Find("Guard");
        Transform guardIcon = guardPlace.transform.GetChild(0);
        if (guardIcon != null)
        {
            GameObject.Destroy(guardIcon.gameObject);
        }
        if(PlayerData.HasGuard){

            var guard = Instantiate(PF_Guard);
            guard.transform.SetParent(guardPlace.transform);
            guard.GetComponent<RectTransform>().localPosition = new Vector3(-500, 96, 0);

        }
    }
}
