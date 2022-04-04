using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    AudioSource source;

    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip gameLoopMusic;

    bool paused;

    private void Awake() 
    {
        source = GetComponent<AudioSource>();    
    }

    public void PlayMainMenuMusic()
    {
        source.clip = mainMenuMusic;
        source.Play();
    }

    public void PlayGameMusic()
    {
        StartCoroutine(PlayGameLoop());
    }
    
    IEnumerator PlayGameLoop()
    {
        source.clip = gameMusic;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.clip = gameLoopMusic;
        source.Play();
    }

    public void TogglePause()
    {
        if(paused)
        {
            source.UnPause();
            paused = false;
        }
        else
        {
            source.Pause();    
            paused = true;
        }
        
    }

    public void ResetCurrentTrack()
    {
        source.Stop();
    }
        
}
