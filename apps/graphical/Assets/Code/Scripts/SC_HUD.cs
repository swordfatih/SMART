using UnityEngine;
using Game;
using TMPro;
using Network;

public class SC_HUD : MonoBehaviour, IObserver<PlayerData>, IObserver<BoardData>
{
    public PlayerData PlayerData { get; set; }
    public BoardData BoardData { get; set; }
    public GameObject PF_Guard;
    public GameObject PF_Associate;
    public GameObject PF_Inmate;
    public GameObject PF_Shovel;
    public GameObject PF_Soap;
    public GameObject PF_Poison;
    public bool Initialized { get; set; }

    void Start()
    {
        GameManager.Instance.Subscribe((IObserver<BoardData>)this);
        GameManager.Instance.Subscribe((IObserver<PlayerData>)this);
        Initialized = false;

        GameManager.Instance.Client.Node.Send(RequestType.NotifyPlayer);
        GameManager.Instance.Client.Node.Send(RequestType.NotifyBoard);
    }

    public void Notify(BoardData boardData)
    {
        BoardData = boardData;
        UpdateHUD();
    }

    public void Notify(PlayerData playerData)
    {
        PlayerData = playerData;
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        var HUD = GameObject.Find("HUD");

        if (PlayerData is null || BoardData is null || HUD is null)
        {
            return;
        }

        var Role = HUD.transform.Find("Role");
        var Progression = HUD.transform.Find("Progression");
        var Day = HUD.transform.Find("Day");
        var Guard = HUD.transform.Find("Guard");
        var Items = HUD.transform.Find("Items");

        if (!Initialized)
        {
            if (PlayerData.Player.Role.ToString() == "Associate")
            {
                var associate = Instantiate(PF_Associate);
                associate.transform.SetParent(Role.transform);
                associate.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                associate.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else
            {
                var inmate = Instantiate(PF_Inmate);
                inmate.transform.SetParent(Role.transform);
                inmate.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                inmate.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }

            Initialized = true;
        }

        // delete all shovels
        foreach (Transform child in Progression.GetComponentInChildren<Transform>())
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < PlayerData.Player.Progression; ++i)
        {
            var shovel = Instantiate(PF_Shovel);
            shovel.transform.SetParent(Progression.transform);
            shovel.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            shovel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            shovel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }

        // Update des Jours
        var count = Day.GetComponentInChildren<TMP_Text>();
        count.text = BoardData.Day.ToString();

        // delete all guard children
        foreach (Transform child in Guard.GetComponentInChildren<Transform>())
        {
            GameObject.Destroy(child.gameObject);
        }

        if (PlayerData.HasGuard)
        {
            var guard = Instantiate(PF_Guard);
            guard.transform.SetParent(Guard.transform);
            guard.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            guard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        // update items
        foreach (Transform child in Items.GetComponentInChildren<Transform>())
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var item in PlayerData.Player.Items)
        { 
            var GO_Item = item.Name switch {
                "Soap" => Instantiate(PF_Soap),
                "Poison" => Instantiate(PF_Poison),
                _ => null
            };

            if(GO_Item is null)
            {
                continue;
            }
        
            GO_Item.transform.SetParent(Items.transform);
            GO_Item.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            GO_Item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            GO_Item.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }
}
