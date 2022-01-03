using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.SFXPlay("게임오버", GameManager.Instance.gameoverSFX);
    }

}
