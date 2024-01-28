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
        AudioManager.instance.masterSound = masterSlider.value;
        AudioManager.instance.SetVolume();
        Debug.Log("Master sound: " + AudioManager.instance.masterSound);
    }
    public void SetSfxSound()
    {
        AudioManager.instance.sfxSound = sfxSlider.value;
        AudioManager.instance.SetVolume();
    }
    public void SetBgmSound()
    {
        AudioManager.instance.bgmSound = bgmSlider.value;
        AudioManager.instance.SetVolume();
    }
}
