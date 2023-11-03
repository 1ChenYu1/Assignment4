using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip introMusic;
    public AudioClip normalStateMusic;
    public AudioClip scaredStateMusic;
    public AudioClip deadStateMusic;
    public AudioClip pacStudentMoving;
    public AudioClip pelletEaten;
    public AudioClip collisionWithWall;
    public AudioClip pacStudentDeath;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayIntroMusicCoroutine());
    }
    IEnumerator PlayIntroMusicCoroutine()
    {
        PlayMusic(introMusic);
        yield return new WaitForSeconds(10);
        StopMusic();
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayNormalStateMusic()
    {
        PlayMusic(normalStateMusic);
    }

    public void PlayScaredStateMusic()
    {
        PlayMusic(scaredStateMusic);
    }

    public void PlayDeadStateMusic()
    {
        PlayMusic(deadStateMusic);
    }

    public void PlayPacStudentMoving()
    {
        PlaySound(pacStudentMoving);
    }

    public void PlayPelletEaten()
    {
        PlaySound(pelletEaten);
    }

    public void PlayCollisionWithWall()
    {
        PlaySound(collisionWithWall);
    }

    public void PlayPacStudentDeath()
    {
        PlaySound(pacStudentDeath);
    }

    private void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }

    private void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }





}
