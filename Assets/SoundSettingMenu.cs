using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider bgmSlider;

    public void SetMasterSound()
    {
        AudioManager.instance.masterSound = 100.0f;
    }
    public void SetSfxSound()
    {
        AudioManager.instance.masterSound = 100.0f;
    }
    public void SetBgmSound()
    {
        AudioManager.instance.masterSound = 100.0f;
    }
}
