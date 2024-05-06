using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ActivationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bulle;


    void OnTriggerEnter()
    {
        Bulle.SetActive(true);
    }

    void Update()
    {
        
    }
}
