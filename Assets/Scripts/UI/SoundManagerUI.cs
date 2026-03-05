using UnityEngine;
using UnityEngine.UI;

public class SoundManagerUI : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    void Start()
    {
        if(!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            Load();
        }
        
        if(!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            Load();
        }
    }

    public void MusicVolumeChanged()
    {
        AudioListener.volume = musicVolumeSlider.value;
        Save();
    }

    public void SfxVolumeChanged()
    {
        AudioListener.volume = sfxVolumeSlider.value;
    }

    private void Load()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }
}
