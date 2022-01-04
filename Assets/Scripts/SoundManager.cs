using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgSound;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        bgSound = GetComponent<AudioSource>();
        BgSoundPlay(GameManager.Instance.LobbyMusic);

    }


     public void SFXPlay(string sfxSound, AudioClip clip)
    {
        GameObject go = new GameObject(sfxSound + "Sound");
        AudioSource audioSource =  go.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length); 
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.Stop();

        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.Play();
    }
}
