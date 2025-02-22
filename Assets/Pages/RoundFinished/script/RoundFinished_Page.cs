using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoundFinished_Page : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerName;
    private Animation anim;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // get animation
        anim = GetComponent<Animation>();
        // Listen on Game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus) {

        Debug.Log("RoundFinished_Page OnGameStatusChange: " + gameStatus);

        // Continue if gameStatus is RoundFinished
        if (gameStatus != GameStatus.RoundFinished) return;

        // Init data
        initData();
    }

    private void initData(){
        // Check if the winner was me or oponent
        winnerName.text = Data_Holder.Instance.gameData.GetData().winnerId == Data_Holder.Instance.GetSelfPlayer().id ? "You" : "opponent";

        // Play animation
        anim.Play();
    }
}