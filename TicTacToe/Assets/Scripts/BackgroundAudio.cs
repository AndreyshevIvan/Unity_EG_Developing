using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAudio : MonoBehaviour
{
    public int gameBagroundsCount;

    public AudioClip menuBackground;
    public AudioClip[] gameBackground;

    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayMenuMusic()
    {
        PlaySound(menuBackground);
    }
    public void PlayGameplayMusic()
    {
        int clip = Random.Range(0, gameBagroundsCount);
        PlaySound(gameBackground[clip]);
    }
    private void PlaySound(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
