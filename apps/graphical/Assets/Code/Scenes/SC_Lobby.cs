using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SC_Lobby : MonoBehaviour
{
    public int iaNb;
    public int yValue;
    public Canvas canvas; // Le Canvas où ajouter le prefab
    public GameObject iaPrefab; // Le prefab à ajouter au Canvas
    public GameObject addButton;
    public Transform iaPanel;

    // Start is called before the first frame update
    void Start()
    {
        iaNb=0;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void AddIAButton()
    {
        if(iaNb<7){ //juste pour de l'affichage, à corriger avec scrollview? ou vrm mettre limite?
            iaNb=iaNb+1;
            
            Vector3 position = new Vector3(860, 240-yValue, 0); 
            yValue+=38;
            
            Quaternion rotation = Quaternion.identity; // No rotation

            GameObject newIA = Instantiate(iaPrefab, position, rotation, iaPanel);
            Button removeButton = newIA.GetComponentInChildren<Button>();
            TMP_Text iaName=newIA.GetComponentInChildren<TMP_Text>();
  
            removeButton.onClick.AddListener(() => removeIAButton(newIA));
            if (removeButton != null)
            {
                Debug.Log("Button added.");
            }
            iaName.text= "IA "+ iaNb.ToString();


        
        }else{
            Debug.Log("You can't add more than 7 IA.");
        }
        //TO DO : du coup connexion d'une IA RANDOM


    }
    public void removeIAButton(GameObject iaToRemove)
    {
        iaNb--;
        Destroy(iaToRemove);
        Debug.Log("Remove IA clicked");
    }
}

