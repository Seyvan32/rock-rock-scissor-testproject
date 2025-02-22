using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameFlow : MonoBehaviour
{
    private int turn = 0;

    private int selfId = 1;
    private int oponentId = 2;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // listen on game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus)
    {
        Debug.Log("GameFlow OnGameStatusChange: " + gameStatus);

        // return if game status is not the desired one
        if (gameStatus != GameStatus.GameBoard &&
            gameStatus != GameStatus.RaiseHand &&
            gameStatus != GameStatus.RoundStarted) return;

        // if game status is GameBoard, start round
        if (gameStatus == GameStatus.GameBoard)
        {
            // Init players data for the first time
            InitPlayers();

            StartRound();
        }

        // if game status is RaiseHand, raise hand for bot
        if (gameStatus == GameStatus.RaiseHand) StartCoroutine(RaiseHandForBot());

    }

    private void InitPlayers()
    {
        // Create two players
        PlayerData selfPlayer = new PlayerData
        {
            id = selfId,
            score = 0
        };
        PlayerData oponentPlayer = new PlayerData
        {
            id = oponentId,
            score = 0
        };
        // Set them to gameData
        Data_Holder.Instance.gameData.GetData().players = new PlayerData[] { selfPlayer, oponentPlayer };
    }

    private void StartRound()
    {
        // Set turn to self turn
        turn = Data_Holder.Instance.GetSelfPlayer().id;
        Data_Holder.Instance.gameData.GetData().turn = turn;

        // emit start round event
        Data_Holder.Instance.OnGameStatus.Invoke(GameStatus.RoundStarted);
    }

    // Enumerator method for raise hand for bot
    private IEnumerator RaiseHandForBot()
    {
        // wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Set turn to opponent turn
        turn = Data_Holder.Instance.GetOponentPlayer().id;
        Data_Holder.Instance.gameData.GetData().turn = turn;

        // choose a handtpye randomly for oponent player
        Data_Holder.Instance.GetOponentPlayer().hand = (HandType)Random.Range(0, 3);

        //emit a Raise Bot Hand event
        Data_Holder.Instance.OnGameStatus.Invoke(GameStatus.RaiseBotHand);

        // wait for 1 seconds
        yield return new WaitForSeconds(1f);

        // compare player hands and add score based on winner
        PlayerData winner = CompareHands();
        if (winner != null)
        {
            // add score to winner player
            winner.score++;

            // Set winner to gameData
            Data_Holder.Instance.gameData.GetData().winnerId = winner.id;

            // emit round finished event
            Data_Holder.Instance.OnGameStatus.Invoke(GameStatus.RoundFinished);
        }
        
        // wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        StartRound();
    }

    private PlayerData CompareHands()
    {
        // get self and oponent player
        PlayerData selfPlayer = Data_Holder.Instance.GetSelfPlayer();
        PlayerData oponentPlayer = Data_Holder.Instance.GetOponentPlayer();

        // compare their hands
        if (selfPlayer.hand == oponentPlayer.hand)
        {
            return null; // Draw
        }

        if ((selfPlayer.hand == HandType.Rock && oponentPlayer.hand == HandType.Scissors) ||
            (selfPlayer.hand == HandType.Paper && oponentPlayer.hand == HandType.Rock) ||
            (selfPlayer.hand == HandType.Scissors && oponentPlayer.hand == HandType.Paper))
        {
            return selfPlayer; // Player 1 wins
        }

        return oponentPlayer; // Player 2 wins
    }
}