using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private AudioSource audio;

    private void Awake()
    {
        this.audio = GetComponent<AudioSource>();
    }

    public IEnumerator PlaySE(AudioClip clip)
    {
        this.audio.clip = clip;
        this.audio.Play();
        yield return new WaitForSeconds(clip.length);
        Destroy(this.gameObject);
    }
}
