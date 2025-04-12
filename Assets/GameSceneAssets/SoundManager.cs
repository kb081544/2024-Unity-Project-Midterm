using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] playaudio;

    // Start is called before the first frame update
    void Start()
    {
        playaudio = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayMusic2()
    {

        playaudio[0].Play();

    }
    public void PlayWhistle2()
    {
        playaudio[1].Play();
    }
    public void PlayPickSound()
    {
        playaudio[2].Play();
    }
    public void PlayGoldbarSound()
    {
        playaudio[3].Play();
    }
    public void PlayPoopSound() {
        playaudio[4].Play();
    }
}