using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq; //Pour requête


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public Slider musicSlider; // Slider Music
    public Slider soundEffectSlider; // Slider Sound Effect

    public void Start()
    {

        // Récupérer les volumes des pistes audio
        // Music
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;
        
        // Sound Effect
        audioMixer.GetFloat("Sound Effect", out float soundValueForSlider);
        soundEffectSlider.value = soundValueForSlider;

        
        // Affiche toutes les résolutions du PC - Distinct = pas de duplication > ToArray = Resolution est une array
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //Plein Ecran par défaut
        Screen.fullScreen = true;
    }
    

    //Pour MUSIC
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }


    //Pour SOUND EFFECT
    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sound Effect", volume);
    }

    
    //Pour Plein Ecran
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //Pour apliquer la resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
