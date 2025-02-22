using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private HandType handType;
    [SerializeField] private bool isMe;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Listen on Game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus)
    {
        Debug.Log("Hand OnGameStatusChange: " + gameStatus);

        // countinue on Raise Hand or Raise Bot Hand
        if (gameStatus != GameStatus.RaiseHand || gameStatus != GameStatus.RaiseBotHand)
        {
            gameObject.SetActive(false);
            return;
        }

        // get player based on isMe
        PlayerData player = isMe ? Data_Holder.Instance.GetSelfPlayer() : Data_Holder.Instance.GetOponentPlayer();

        // check if the hand type of the player is the same as the hand type of this hand, then active this hand else deactive
        if (player.hand == handType)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}