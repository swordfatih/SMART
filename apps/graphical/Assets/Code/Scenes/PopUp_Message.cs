using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopUp_Message : MonoBehaviour
{
    public TMP_Text message_containt;
    public void AfficheMessage(String msg)
    {
        GameObject canvas = GameObject.Find("Message");
        canvas.SetActive(true);
        message_containt.text = msg;

    }
}
