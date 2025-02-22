using TMPro;
using UnityEngine;

public class player_scoreboard : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    [SerializeField] private bool isMe = false;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Get score text component
        scoreText = GetComponent<TextMeshProUGUI>();

        // Listen on game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus) {

        Debug.Log("player_scoreboard OnGameStatusChange: " + gameStatus);

        // continue on Round Finsihed
        if (gameStatus != GameStatus.RoundStarted) return;

        // get player 
        PlayerData player = isMe ? Data_Holder.Instance.GetSelfPlayer() : Data_Holder.Instance.GetOponentPlayer();

        // Update score text
        scoreText.text = player.score.ToString();

        // Check if this score is 5 or higher then invoke game finished
        if (CheckWinner()) Data_Holder.Instance.OnGameStatus.Invoke(GameStatus.GameFinished);
    }

    private bool CheckWinner(){
        // Check if the score is 5 or higher than 5
        if (isMe && Data_Holder.Instance.GetSelfPlayer().score >= 5 || !isMe && Data_Holder.Instance.GetOponentPlayer().score >= 5) return true;
        else return false;
    }
}