using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public void btnSound()
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", GameManager.Instance.btnClickSFX);
    }
    public void toGameScene()
    {
        LoadingSceneController.LoadScene("GameScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }


    public void showUI(GameObject UI)
    {
        GameManager.Instance.UI_Q.Enqueue(UI);

        UI.SetActive(true);
    }

    public void closeUI()
    {
        GameManager.Instance.UI_Q.Dequeue().SetActive(false);

    }

    public void ReTry()
    {

        GameSceneManager.Instance.player.ownItemList.Remove(GameSceneManager.Instance.player.ownItemList[GameSceneManager.Instance.player.ownItemList.Count - 1]);
        GameSceneManager.Instance.boss.isDie = false;
        GameSceneManager.Instance.player.hp = 1;
        GameSceneManager.Instance.player.applySpeed = GameSceneManager.Instance.player.speed;
        GameSceneManager.Instance.ItemChoose = false;
        GameSceneManager.Instance.player.hungryGauge = GameSceneManager.Instance.player.maxHungryGauge;

        ChooseItem.choosingItem = null;
        GameSceneManager.Instance.mainCanvas.SetActive(true);
        GameSceneManager.Instance.isbossStart = false;

        GameSceneManager.Instance.boss.isDie = false;
        while (GameSceneManager.Instance.boss.bullets.Count != 0)
        {
            Destroy(GameSceneManager.Instance.boss.bullets.Dequeue().gameObject);
        }

        GameSceneManager.Instance.boss.Hp = GameSceneManager.Instance.boss.maxHp;
        GameObject[] tem = GameObject.FindGameObjectsWithTag("ChooseItem");
        foreach (var item in tem)
        {
            Destroy(item);
        }

        UIManager.Instance.playerOwnItems_UI_Update();
        try
        {
            Destroy(GameSceneManager.Instance.player.GetComponentInChildren<ChooseItem>().gameObject);
        }
        catch
        {

        }
        Instantiate(GameManager.Instance.ChooseItemPrefab);

        GameSceneManager.Instance.player.canMove_any = true;
        UIManager.Instance.diePanel.SetActive(false);

        GameSceneManager.Instance.sagamTeacher.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.sagamNormal;
        GameSceneManager.Instance.bossStartBang.SetActive(false);

        SoundManager.Instance.BgSoundPlay(GameManager.Instance.normalGameMusic);
        UIManager.Instance.cnt = 0;
    }

    public void ToTitle()
    {
        LoadingSceneController.LoadScene("TitleScene");
    }

    public void Cancel()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;

    }

}
