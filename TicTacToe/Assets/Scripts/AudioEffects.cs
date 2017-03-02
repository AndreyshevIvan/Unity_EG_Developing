using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour
{
    public AudioClip buttonTick;
    public AudioClip win;
    public AudioClip setGrid;
    public AudioClip playerHello;
    public AudioClip robotHello;
    public AudioClip loose;

    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void SetGrid()
    {
        PlaySound(setGrid);
    }
    public void ButtonTick()
    {
        PlaySound(buttonTick);
    }
    public void Winner()
    {
        PlaySound(win);
    }
    public void Looser()
    {
        PlaySound(loose);
    }
    public void ComputerHello()
    {
        PlaySound(robotHello);
    }
    public void PlayerHello()
    {
        PlaySound(playerHello);
    }
    private void PlaySound(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}