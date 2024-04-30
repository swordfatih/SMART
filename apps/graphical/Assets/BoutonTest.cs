using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonTest : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testClick()
    {

        Debug.Log("Bouton cliqué!");
        gameManager.globalScore -= 3;
        Debug.Log(gameManager.globalScore);

    }
}
