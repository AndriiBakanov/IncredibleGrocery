using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicSource;
    public AudioSource soundsSource;
    protected override void Initialise()
    {
        LoadAudioSettings();
    }
    void LoadAudioSettings() 
    {
        Settings settings = SaveSystem.LoadSettings();
        soundsSource.mute = settings.soundMute;
        musicSource.mute = settings.musicMute;
    }
    public void PlaySound(AudioClip clip) 
    {
        soundsSource.clip = clip;
        soundsSource.Play();
    }
}
