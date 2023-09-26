using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //Keep this object even when we go to a new scene
        if(Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //destroy duplicate gameobjects
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }

    public void ChangeSoundVolume(float change)
    {
        float baseVolume = 1;

        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat("soundVolume");
        currentVolume += change;

        //Check if we've reached max or min value of volume
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        //Assign the final value
        float finalVolume = currentVolume * baseVolume;
        soundSource.volume = finalVolume;
        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }

    public void ChangeMusicVolume(float change)
    {
        float baseVolume = 0.3f;

        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat("musicVolume");
        currentVolume += change;

        //Check if we've reached max or min value of volume
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        //Assign the final value
        float finalVolume = currentVolume * baseVolume;
        musicSource.volume = finalVolume;
        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
}
