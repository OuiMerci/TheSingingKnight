using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameManager.Instance.State)
        {
            case GameState.Playing:
                GetSongInput();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Playing:
                GetPlayerMovement();
                break;
        }
    }

    void GetPlayerMovement()
    {
        Vector2 mv = Vector2.zero;
        mv.x = Input.GetAxis("Horizontal");
        mv.y = Input.GetAxis("Vertical");

        GameManager.Instance.Player.Movement.ApplyMovement(mv);
    }

    void GetSongInput()
    {
        if(Input.GetButtonDown("Dance"))
        {
            GameManager.Instance.Player.SongGameplay.StartDancing();
        }
        else if(Input.GetButtonUp("Dance"))
        {
            GameManager.Instance.Player.SongGameplay.EndDancing();
        }

        if(GameManager.Instance.Player.SongGameplay.NoActiveSongOrDance)
        {
            if (Input.GetButtonDown("Song1"))
            {
                GameManager.Instance.Player.SongGameplay.PlaySong(0);
            }
            else if (Input.GetButtonDown("Song2"))
            {
                GameManager.Instance.Player.SongGameplay.PlaySong(1);
            }
            else if (Input.GetButtonDown("Song3"))
            {
                GameManager.Instance.Player.SongGameplay.PlaySong(2);
            }
        }
        else
        {
            if (Input.GetButtonUp("Song1"))
            {
                GameManager.Instance.Player.SongGameplay.TryEndSong(0);
            }
            else if (Input.GetButtonUp("Song2"))
            {
                GameManager.Instance.Player.SongGameplay.TryEndSong(1);
            }
            else if (Input.GetButtonUp("Song3"))
            {
                GameManager.Instance.Player.SongGameplay.TryEndSong(2);
            }
        }
    }
}
