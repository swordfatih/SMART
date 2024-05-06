using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulleScript : MonoBehaviour
{
    public TextMeshPro Txt;
    public GameObject Bulle;

    public IEnumerator AfficheTxtBulle(string TxtDepart)
    {
        Bulle.SetActive(true);
        string temp = TxtDepart;
        int nbChar = TxtDepart.Length;
        
        float speed = 0.1f;
        for(int i=1;i<=nbChar;i++){
            yield return new WaitForSeconds(speed);
            Txt.text = temp.Substring(0,i);
        }

    }

    public void call_affichage(){

        string message = "Oussama va tenter de s'évader cette nuit. Va l'attraper!";
        StartCoroutine(AfficheTxtBulle(message));
    }
}
