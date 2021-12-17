using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject placePanel;
    public Slider hungryGauge;

    public PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hungryGauge.maxValue = player.maxHungryGauge;

    }

    private void Update()
    {
        set_hungryGauge();
    }
    public void pop_UI(string name)
    {
        placePanel.GetComponentInChildren<Text>().text = name;
        placePanel.GetComponent<Animation>().Stop();
        placePanel.GetComponent<Animation>().Play();
    }


    public void set_hungryGauge()
    {
        hungryGauge.value = player.hungryGauge;
    }
}
