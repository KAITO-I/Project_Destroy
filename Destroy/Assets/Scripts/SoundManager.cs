using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SoundManager) FindObjectOfType(typeof(SoundManager));
                if (instance == null) Debug.LogError(typeof(SoundManager) + "がシーン上にありません！");
            }
            return instance;
        }
    }

    private AudioSource audio;
    private GameObject se;

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        this.audio = GetComponent<AudioSource>();
        this.se = this.transform.Find("SE").gameObject;
    }

    public void PlayBGM(AudioClip clip)
    {
        this.audio.clip = clip;
        this.audio.Play();
    }

    public void StopBGM()
    {
        this.audio.Stop();
    }

    public void PlaySE(AudioClip clip)
    {
        StartCoroutine(Instantiate(this.se).GetComponent<SEManager>().PlaySE(clip));
    }
}
