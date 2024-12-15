using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

     [Header("------  Audio Source ------")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;


     [Header("------  Audio Clip ------")]

    public AudioClip backgroundMusic;
    public AudioClip clickButtonSound;

     [Header("------ Volume Settings ------")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void PlayBackgroundMusic(AudioClip audioCLip)
    {
        if (audioCLip != null)
        {
            backgroundMusic = audioCLip;
        }

        if (backgroundMusic != null)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.volume = musicVolume * masterVolume;
            backgroundMusicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
    public void PlaySFX(AudioClip sfxClip, float volumeScale = 1f)
    {
        if (sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip, sfxVolume * masterVolume * volumeScale);
        }
    }
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        backgroundMusicSource.volume = musicVolume * masterVolume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }


    private void UpdateAllVolumes()
    {
        backgroundMusicSource.volume = musicVolume * masterVolume;
    }

    public void PauseAllSounds()
    {
        backgroundMusicSource.Pause();
        sfxSource.Pause();
   
    }




}
