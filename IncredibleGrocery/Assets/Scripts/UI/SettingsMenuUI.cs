using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    public Text soundText;
    public Text musicText;
    public Image soundButtonImage;
    public Image musicButtonImage;
    Settings settings;
    private void Start()
    {
        settings = SaveSystem.LoadSettings();
        ChangeMusicButton(settings.musicMute);
        ChangeSoundButton(settings.soundMute);
    }
    public void ChangeSoundSettings()
    {
        settings.soundMute = !settings.soundMute;
        AudioManager.instance.soundsSource.mute = settings.soundMute;
        ChangeSoundButton(settings.soundMute);
    }
    public void ChangeMusicSettings()
    {
        settings.musicMute = !settings.musicMute;
        AudioManager.instance.musicSource.mute = settings.musicMute;
        ChangeMusicButton(settings.musicMute);
    }
    void ChangeSoundButton(bool isMute)
    {
        if (isMute)
        {
            soundText.text = "OFF";
            soundButtonImage.color = Color.red;
        }
        else
        {
            soundText.text = "ON";
            soundButtonImage.color = Color.green;
        }
    }
    void ChangeMusicButton(bool isMute)
    {
        if (isMute)
        {
            musicText.text = "OFF";
            musicButtonImage.color = Color.red;
        }
        else
        {
            musicText.text = "ON";
            musicButtonImage.color = Color.green;
        }
    }
    public void SaveSettings()
    {
        SaveSystem.SaveSettings(settings);
        GameManager.instance.SetPause(false);
        UIManager.instance.CloseSettings();
    }
}
