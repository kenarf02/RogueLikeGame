using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer Sounds;
    public AudioMixer Music;
    Resolution[] resolutions;
    public Dropdown resolutiondropdown;
    public GameObject usure;
    public Slider volume;
    public GameObject optionsmenu;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Brightness") != true)
        {
            PlayerPrefs.SetFloat("Brightness", 0.4f);
        }
        //get avaliable screen resolutions to an array
       resolutions = Screen.resolutions;
        //reference the dropdown and clear its placeholdert options
        resolutiondropdown.ClearOptions();

        //addoptions method require a list of strings as an argument, so we create one from our resolutions array
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
            //add every possible resolution to the list
        for(int i= 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height ;
            options.Add(option);
            //check for resolution the game is currently using (misc purposes)
            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //add our list of strings as a set of options;
        resolutiondropdown.AddOptions(options);
        //Set Current resolution shown by a dropdown as the one our game uses
        resolutiondropdown.value = currentResolutionIndex;
        resolutiondropdown.RefreshShownValue();
    }
    public void SetResoultion(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetBrightness(float brightness)
    {
            PlayerPrefs.SetFloat("Brightness", brightness);
    }
    public void SetVolume(float volume)
    {
        Sounds.SetFloat("volume",volume);
    }
    public void SetMusicVolume(float volume)
    {
        Music.SetFloat("volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void AskIfSure()
    {
        usure.SetActive(true);
        optionsmenu.SetActive(false);
    }
    public void GoBack()
    {
        usure.SetActive(false);
        optionsmenu.SetActive(true);
    }
    public void ClearPlayerprefs()
    {
        usure.SetActive(false);
        optionsmenu.SetActive(true);
        PlayerPrefs.DeleteAll();
    }
}
