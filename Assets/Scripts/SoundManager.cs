using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public AudioSource bgSound;
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
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
}
