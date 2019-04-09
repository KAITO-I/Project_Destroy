using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleManager : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField] AudioClip[] voices;
    [SerializeField] TextMeshProUGUI subtitle;
    [SerializeField] string[] subtitles;

    void Start()
    {
        this.audio = GetComponent<AudioSource>();
        StartCoroutine(voicePlay());
    }

    private IEnumerator voicePlay()
    {
        int num = 0;
        while (num < voices.Length)
        {
            audio.clip = voices[num];
            audio.Play();
            subtitle.text = subtitles[num];
            yield return new WaitForSeconds(voices[num].length);
            num++;
        }
    }
}
