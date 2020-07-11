using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SongsNames
{
    Mi,
    Ka,
    Do,
    None
}

public class SongGameplay : MonoBehaviour
{
    public event EventHandler<StartSongArgs> StartSong;
    public event EventHandler<EndSongArgs> EndSong;
    public event EventHandler<StartDanceArgs> StartDance;
    public event EventHandler<EndDanceArgs> EndDance;

    public SongRing[] SongPrefabs;
    public DanceRing DanceRing;
    public float DamagePerTick;
    public double TickLength;
    public float MaxStamina;
    public float StaminaConsumption;
    public float StaminaRegen;
    public float DanceBonusRegen;

    [Header("ReadOnly")]
    public SongRing ActiveSongRing;

    private float stamina;

    public bool IsSinging { get { return ActiveSongRing != null; } }
    public bool IsDancing { get { return DanceRing.gameObject.activeSelf; } }
    public bool NoActiveSongOrDance {
        get
        {
            return IsSinging == false && IsDancing == false;
        }
    }

    public void Start()
    {
        stamina = MaxStamina;
    }

    private void Update()
    {
        if (IsSinging)
            ConsumeStamina();
        else if (IsDancing)
            RegenStamina(boost: DanceBonusRegen);
        else
            RegenStamina();
    }

    public void PlaySong(int index)
    {
        ActiveSongRing = Instantiate(SongPrefabs[index], transform.position, Quaternion.identity, transform);
        ActiveSongRing.transform.eulerAngles = new Vector3(90, 0, 0);
        ActiveSongRing.transform.position += new Vector3(0, 0.1f, 0);

        StartSong?.Invoke(this, new StartSongArgs(ActiveSongRing));
        Debug.Log("Start song " + ActiveSongRing.SongName);
    }

    public void TryEndSong(int index)
    {
        if(ActiveSongRing != null && ActiveSongRing.SongName == SongPrefabs[index].SongName)
        {
            StopCurrentSong();
        }
    }

    public void StopCurrentSong()
    {
        EndSong?.Invoke(this, new EndSongArgs(ActiveSongRing));
        Debug.Log("End song " + ActiveSongRing.SongName);
        Destroy(ActiveSongRing.gameObject);
    }

    public void StartDancing()
    {
        if (IsSinging)
            StopCurrentSong();

        DanceRing.gameObject.SetActive(true);
        DanceRing.transform.eulerAngles = new Vector3(90, 0, 0);
        DanceRing.transform.position += new Vector3(0, 0.1f, 0);

        StartDance?.Invoke(this, new StartDanceArgs(DanceRing));

        int rand = UnityEngine.Random.Range(0, 3);
        GameManager.Instance.Player.Animator.SetInteger("RandDance", rand);
        GameManager.Instance.Player.Animator.SetTrigger("StartDancing");
    }

    public void EndDancing()
    {
        EndDance?.Invoke(this, new EndDanceArgs(DanceRing));
        DanceRing.gameObject.SetActive(false);

        GameManager.Instance.Player.Animator.SetTrigger("StopDancing");
    }

    private void ConsumeStamina()
    {
        stamina -= StaminaConsumption * Time.deltaTime;

        if(stamina <= 0)
        {
            stamina = 0;
            StopCurrentSong();
        }

        GameManager.Instance.Player.UI.SetStaminaRatio(stamina/MaxStamina);
    }

    private void RegenStamina(float boost = 1)
    {
        if(stamina < MaxStamina)
        {
            stamina += StaminaRegen * Time.deltaTime * boost;

            if (stamina > MaxStamina)
                stamina = MaxStamina;

            GameManager.Instance.Player.UI.SetStaminaRatio(stamina / MaxStamina);
        }
    }
}

public class StartSongArgs : EventArgs
{
    public SongRing SongRing;

    public StartSongArgs(SongRing song)
    {
        SongRing = song;
    }
}

public class EndSongArgs : EventArgs
{
    public SongRing SongRing;

    public EndSongArgs(SongRing song)
    {
        SongRing = song;
    }
}

public class StartDanceArgs : EventArgs
{
    public DanceRing DanceRing;

    public StartDanceArgs(DanceRing ring)
    {
        DanceRing = ring;
    }
}

public class EndDanceArgs : EventArgs
{
    public DanceRing DanceRing;

    public EndDanceArgs(DanceRing ring)
    {
        DanceRing = ring;
    }
}