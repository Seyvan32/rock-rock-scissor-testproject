using System.Collections.Generic;
using UnityEngine;

public class Data_GameData : MonoBehaviour
{
    //game data
    private GameData gameData = new GameData();

    public GameData GetData()
    {
        return gameData;
    }

    public void SetData(GameData data)
    {
        //var events = new List<GameStatus>();

        if (data.players != null)
        {
            gameData.players = data.players;
            //events.Add(GameStatus.PlayerUpdated);
        }

        if (data.turn != null)
        {
            gameData.turn = data.turn;
            //events.Add(GameStatus.TurnUpdated);
        }

        // if (events.Count > 0)
        // {
        //     // emit all events
        //     foreach (var e in events)
        //     {
        //         Data_Holder.Instance.OnGameStatus.Invoke(e);
        //     }
        // }

    }
}

public class GameData
{
    public PlayerData[] players;
    public int turn = 0;
    public int winnerId = 0;
}

public class PlayerData
{
    public int id;
    public HandType hand;
    public int score;
}

public enum HandType
{
    Rock,
    Paper,
    Scissors
}