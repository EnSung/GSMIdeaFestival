using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject placePanel;
    public Slider hungryGauge;

    public PlayerController player;

    public Image usingItemImage;
    public Text usingItemName;

    public Text questText;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hungryGauge.maxValue = player.maxHungryGauge;

    }

    private void Update()
    {
        set_hungryGauge();
        set_usingItem();
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

    public void set_usingItem()
    {
        if(player.usingItem != null)
        {
            usingItemName.gameObject.SetActive(true);
            usingItemImage.sprite = player.usingItem.itemImage.sprite;
            usingItemImage.color = player.usingItem.itemImage.color;
            usingItemName.text = player.usingItem.itemName;
        }
        else
        {
            usingItemName.gameObject.SetActive(false);
            usingItemImage.color = new Color(159, 159, 159);
        }

    }
}
