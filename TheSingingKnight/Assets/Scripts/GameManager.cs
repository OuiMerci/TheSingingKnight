using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Menu, Playing, GameOver}

public class GameManager : MonoBehaviour
{
    static private GameManager _instance;
    public GameState State;
    public Player Player; 
    public AudioManager Audio;

    static public GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        State = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
