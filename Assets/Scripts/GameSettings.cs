using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public Button SettingsButton;
    public Button MusicButton;
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
    

    public GameObject DropDown;
    
	void Start()
    {
        SettingsButton.onClick.AddListener(delegate () { DisplaySettings(); });
        SoundButton.onClick.AddListener(delegate () { ToggleSound (); });
        MusicButton.onClick.AddListener(delegate () { ToggleMusic(); });
        HomeButton.onClick.AddListener(delegate () { GoHome(); });
    }

    void DisplaySettings()
    {
        IsDroppedDown = !IsDroppedDown;
        if (IsInGameSession) InGameSettingsAnimation.SetBool("IsHidden", IsDroppedDown);
        SettingsAnimation.SetBool("IsHidden", IsDroppedDown);
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
    }

    void ToggleMusic()
    {
        if (IsMusicOn)
        {
            IsMusicOn = false;
            MusicButton.GetComponent<Image>().sprite = InactiveMusicImage;
        }
        else
        {
            IsMusicOn = true;
            MusicButton.GetComponent<Image>().sprite = ActiveMusicImage;
        }
    }

    void GoHome()
    {

    }
}
