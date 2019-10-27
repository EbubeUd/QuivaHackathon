using Assets.Models;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameSettings : MonoBehaviour {

    public Button SettingsButton;
    public Button MusicButton;
    public Button MusicButtonInGame;
    public Button SoundButtonInGame;
    public Button SoundButton;
    public Button HomeButton;

    public Sprite ActiveSoundImage;
    public Sprite InactiveSoundImage;
    public Sprite ActiveMusicImage;
    public Sprite InactiveMusicImage;

    bool IsSoundOn;
    bool IsMusicOn;
    bool IsDroppedDown = true;
    public bool IsInGameSession;  //Should be true while Game play is on
    public Animator SettingsAnimation;
    public Animator InGameSettingsAnimation;
    public AudioSource Music;
    
    public GameObject DropDown;
    
    
	void Start()
    {
        Music.Stop();
        SettingsButton.onClick.AddListener(delegate () { DisplaySettings(); });
        SoundButton.onClick.AddListener(delegate () { ToggleSound (); });
        MusicButton.onClick.AddListener(delegate () { ToggleMusic(); });
        HomeButton.onClick.AddListener(delegate () { GoHome(); });

        //Get the settings
        string settingsStr = PlayerPrefs.GetString("settings");
        Debug.Log(settingsStr);
        if (String.IsNullOrEmpty(settingsStr) || settingsStr == "{}")
        {
            PlayMusic(true);
          
            return;
        }
        SettingsModel settingsMod = JsonConvert.DeserializeObject<SettingsModel>(settingsStr);
        
        if (settingsMod.Music)
        {
            PlayMusic(true);
        }
        if (settingsMod.Sound)
        {
        }
    }

    void DisplaySettings()
    {
        IsDroppedDown = !IsDroppedDown;
        if (IsInGameSession) InGameSettingsAnimation.SetBool("IsHidden", IsDroppedDown);
        if(!IsInGameSession) SettingsAnimation.SetBool("IsHidden", IsDroppedDown);
    }

    void ToggleSound()
    {
        if (IsSoundOn)
        {
            IsSoundOn = false;
            SoundButton.GetComponent<Image>().sprite = InactiveSoundImage;
        }
        else
        {
            IsSoundOn = true;
            SoundButton.GetComponent<Image>().sprite = ActiveSoundImage;
        }
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        //Get the Existing Settings
        SettingsModel settingsMod = new SettingsModel()
        {
            Sound = IsSoundOn,
            Music = IsMusicOn
        };
        string str = JsonConvert.SerializeObject(settingsMod);
        PlayerPrefs.SetString("settings", str);
    }

    void ToggleMusic()
    {
        if (IsMusicOn)
        {
            PlayMusic(false);
        }
        else
        {
            PlayMusic(true);

        }
   
    }

    void GoHome()
    {

    }

    void PlayMusic(bool shouldplay = true)
    {
        switch (shouldplay)
        {
            case true:
         
                IsMusicOn = true;
                MusicButton.GetComponent<Image>().sprite = ActiveMusicImage;
                MusicButtonInGame.GetComponent<Image>().sprite = ActiveMusicImage;
                Music.Play();
                break;
            case false:
                IsMusicOn = false;
                MusicButton.GetComponent<Image>().sprite = InactiveMusicImage;
                MusicButtonInGame.GetComponent<Image>().sprite = InactiveMusicImage;
                Music.Stop();
                break;
        }
        UpdateSettings();
    }

    void PlaySound(bool shouldplay = true)
    {
        switch (shouldplay)
        {
            case true:
                IsSoundOn = true;
                SoundButton.GetComponent<Image>().sprite = ActiveMusicImage;
                SoundButtonInGame.GetComponent<Image>().sprite = ActiveMusicImage;
                break;
            case false:
                IsMusicOn = false;
                SoundButton.GetComponent<Image>().sprite = InactiveMusicImage;
                SoundButtonInGame.GetComponent<Image>().sprite = InactiveMusicImage;
                break;
        }
        UpdateSettings();
    }
}
