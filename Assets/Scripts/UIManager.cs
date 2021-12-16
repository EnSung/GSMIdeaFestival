using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject placePanel;


    public void pop_UI(string name)
    {
        placePanel.GetComponentInChildren<Text>().text = name;
        placePanel.GetComponent<Animation>().Stop();
        placePanel.GetComponent<Animation>().Play();
    }
}
