using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Sounds
{
    PopUpOpen,
    PopUpClose
}

[System.Serializable]
public struct Sound
{
    public Sounds Name;
    public AudioClip Clip;
}

[System.Serializable]
public struct SongAudioData
{
    public SongsNames Name;
    public AudioClip StartClip;
    public AudioClip LoopingClip;
}

public class AudioManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private AudioSource fxAudio;
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource songAudio;
    [SerializeField] private List<SongAudioData> songs;
    [SerializeField] private List<Sound> sounds;
#pragma warning restore 0649

    private SongAudioData currentSong;

    private void Start()
    {
        GameManager.Instance.Player.SongGameplay.StartSong += PlaySong;
        GameManager.Instance.Player.SongGameplay.EndSong += StopCurrentSong;
    }

    private void OnDisable()
    {
        GameManager.Instance.Player.SongGameplay.StartSong -= PlaySong;
        GameManager.Instance.Player.SongGameplay.EndSong -= StopCurrentSong;
    }

    public void PlaySound(Sounds sound)
    {
        foreach(Sound ns in sounds)
        {
            if(ns.Name == sound)
            {
                fxAudio.PlayOneShot(ns.Clip);
                return;
            }
        }
    }

    private void PlaySong(object sender, StartSongArgs args)
    {
        foreach (SongAudioData s in songs)
        {
            if (s.Name == args.SongRing.SongName)
            {
                currentSong = s;
                StartCoroutine("SongCoroutine");
                return;
            }
        }
    }

    private IEnumerator SongCoroutine()
    {
        songAudio.PlayOneShot(currentSong.StartClip);
        float secureDelay = currentSong.StartClip.length / 10.0f;

        yield return new WaitForSeconds(currentSong.StartClip.length - secureDelay);

        songAudio.clip = currentSong.LoopingClip;
        songAudio.Play();
    }

    private void StopCurrentSong(object sender, EndSongArgs args)
    {
        StopCoroutine("SongCoroutine");
        songAudio.Stop();
        songAudio.clip = null;
    }
}
