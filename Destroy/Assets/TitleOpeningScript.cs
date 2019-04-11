using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOpeningScript : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField] AudioClip se;
    [SerializeField] AudioClip bgm;

    public void Start()
    {
        this.audio = GetComponent<AudioSource>();
    }

    public void OnPlaySE()
    {
        this.audio.clip = se;
        this.audio.Play();
    }

    public void OnPlayBGM()
    {
        this.audio.clip = bgm;
        this.audio.loop = true;
        this.audio.Play();
    }
}
