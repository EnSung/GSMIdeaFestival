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

    #region ownItemsUI
    public GameObject ownItemPrefab;
    public GameObject grid;
    #endregion
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hungryGauge.maxValue = player.maxHungryGauge;

    }

    void Update()
    {
        set_hungryGauge();
        set_usingItem();
        questTextUpdate(player.curQuestFloor);
        questColorUpdate(player.curQuestFloor);
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

    public void questTextUpdate(int floor)
    {
        questText.text = GameManager.Instance.questDescription[floor];
    }

    public void questColorUpdate(int floor)
    {
        if (GameSceneManager.Instance.questClearDict[floor])
        {
            questText.color = Color.green;
        }
        else
        {
            questText.color = Color.black;

        }
    }

    

    public void playerOwnItems_UI_Update()
    {

        if(grid.transform.childCount != 0)
        {
            var child = grid.GetComponentsInChildren<Transform>();


            foreach (var iter in child)
            {
                if (iter != grid.transform)
                {
                    Destroy(iter.gameObject);
                }
            }
        }
        
        foreach (Item item in player.ownItemList)
         {
            var obj = Instantiate(ownItemPrefab);
            obj.transform.parent = grid.transform;
            //obj.transform.localPosition = Vector2.zero;
            obj.GetComponent<Image>().sprite = item.itemImage.sprite;
            obj.GetComponent<Image>().color = item.itemImage.color;
         }
    }

}
