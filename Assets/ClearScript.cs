using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearScript : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.SFXPlay("Å¬¸®¾î",GameManager.Instance.gameClearSFX);
        SoundManager.Instance.BgSoundPlay(GameManager.Instance.ClearMusic);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadingSceneController.LoadScene("TitleScene"); 
        }
    }
}
