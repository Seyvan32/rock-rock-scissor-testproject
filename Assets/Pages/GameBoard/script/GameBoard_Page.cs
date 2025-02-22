using UnityEngine;

public class GameBoard_Page : UI_Element
{

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        
    }

    public override void init(){
        // Listen on game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus){
        
        Debug.Log("GameBoard_Page OnGameStatusChange: " + gameStatus);

        // Open page if gameStatus is GameBoard
        if (gameStatus == GameStatus.GameBoard) UI_Manager.instance.openPage(this);

    }
}