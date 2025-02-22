using System;
using UnityEngine;
using UnityEngine.Events;

public class Data_Holder : MonoBehaviour
{
    // singleton instance
    public static Data_Holder Instance { get; private set; }

    // event for game status
    [HideInInspector] public UnityEvent<GameStatus> OnGameStatus = new UnityEvent<GameStatus>();
    private Data_GameData _gameData;

    [HideInInspector] public Data_GameData gameData
    {
        get { return _gameData ?? (_gameData = new Data_GameData()); }
        set { _gameData = value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Get Self player
    public PlayerData GetSelfPlayer() => Array.Find(gameData.GetData().players, p => p.id == 1);

    public PlayerData GetOponentPlayer() => Array.Find(gameData.GetData().players, p => p.id != 1);
}