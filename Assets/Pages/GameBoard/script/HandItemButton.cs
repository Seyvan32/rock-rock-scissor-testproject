using UnityEngine;
using UnityEngine.UI;

public class HandItemButton : MonoBehaviour
{
    // button
    private Button btn;
    [SerializeField] HandType handType;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Get button
        btn = GetComponent<Button>();

        //Listen on game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus)
    {
        // continue on turn updated
        if (gameStatus != GameStatus.RoundStarted)
        {
            // Disable the button
            btn.interactable = false;
            return;
        }

        // Enable the button
        btn.interactable = true;

        // Get self player
        PlayerData player = Data_Holder.Instance.GetSelfPlayer();

        // Update button state depending on turn
        btn.interactable = Data_Holder.Instance.gameData.GetData().turn == player.id ? true : false;
    }

    public void OnHandButtonClicked()
    {
        // set the hand tpye player to selected hand type
        Data_Holder.Instance.GetSelfPlayer().hand = handType;

        // emit a raise hand event
        Data_Holder.Instance.OnGameStatus.Invoke(GameStatus.RaiseHand);
    }
}